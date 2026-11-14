using FiyiStack.Blazor.Components;
using FiyiStack.Blazor.ExternalServices.APIWithSOAP.Client.Interfaces;
using FiyiStack.Blazor.ExternalServices.APIWithSOAP.Client.Services;
using FiyiStack.Blazor.ExternalServices.APIWithSOAP.Product.Interfaces;
using FiyiStack.Blazor.ExternalServices.APIWithSOAP.Product.Services;
using Microsoft.AspNetCore.DataProtection;
using FiyiStack.Blazor.ExternalServices.APIWithREST.User.Interfaces;
using FiyiStack.Blazor.ExternalServices.APIWithREST.User.Services;
using FiyiStack.Blazor.ExternalServices.APIWithREST.Movie.Services;
using FiyiStack.Blazor.ExternalServices.APIWithREST.Movie.Interfaces;
using Polly;
using Polly.Timeout;
using Polly.Retry;
using Polly.CircuitBreaker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDataProtection()
    .SetApplicationName("FiyiStack.Blazor")
    .PersistKeysToFileSystem(new DirectoryInfo("/app/keys"));   

//CLIENT
builder.Services.AddScoped<IClientSOAPService, ClientSOAPService>();

//PRODUCT
builder.Services.AddScoped<IProductSOAPService, ProductSOAPService>();

//USER
builder.Services.AddHttpClient<IUserRESTService, UserRESTService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["REST:UserServiceEndpoint"]
        ?? throw new InvalidOperationException("Falta configurar REST:UserServiceEndpoint."));
    client.Timeout = TimeSpan.FromSeconds(15);
});

//MOVIE
builder.Services.AddHttpClient<IMovieRESTService, MovieRESTService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["REST:MovieServiceEndpoint"]
        ?? throw new InvalidOperationException("Falta configurar REST:MovieServiceEndpoint."));
}).AddResilienceHandler("Pipeline1", pipelineBuilder =>
{
    // ESTRATEGIA 1: Timeout de intento (frena peticiones individuales colgadas)
    pipelineBuilder.AddTimeout(new TimeoutStrategyOptions
    {
        Timeout = TimeSpan.FromSeconds(15)
    });

    // ESTRATEGIA 2: Reintentos con Backoff Exponencial y Jitter (evita saturar el servidor remoto)
    pipelineBuilder.AddRetry(new RetryStrategyOptions<HttpResponseMessage>
    {
        ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
            .Handle<HttpRequestException>() // Falla la red
            .HandleResult(response => (int)response.StatusCode >= 500), // Error del servidor remoto

        MaxRetryAttempts = 3,
        BackoffType = DelayBackoffType.Exponential,
        UseJitter = true, // Introduce variabilidad de tiempo aleatoria
        Delay = TimeSpan.FromMilliseconds(200),

        OnRetry = args =>
        {
            Console.WriteLine($"[Retry] Intento {args.AttemptNumber + 1}. Motivo: {args.Outcome.Exception?.Message ?? args.Outcome.Result?.StatusCode.ToString()}");
            return ValueTask.CompletedTask;
        }
    });

    // ESTRATEGIA 3: Disyuntor (Circuit Breaker)
    // Rompe el circuito si la tasa de fallos supera el límite, protegiendo tanto tu sistema como la dependencia remota.
    pipelineBuilder.AddCircuitBreaker(new CircuitBreakerStrategyOptions<HttpResponseMessage>
    {
        ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
            .Handle<HttpRequestException>()
            .HandleResult(response => (int)response.StatusCode >= 500),

        FailureRatio = 0.5, // 50% de peticiones fallidas activa el circuito
        SamplingDuration = TimeSpan.FromSeconds(10), // Período de análisis de fallos
        MinimumThroughput = 8, // Mínimo de peticiones requeridas en la ventana para evaluar
        BreakDuration = TimeSpan.FromSeconds(15), // Cuánto tiempo permanece abierto (bloqueando tráfico)

        OnOpened = args =>
        {
            Console.WriteLine("[Circuit Breaker] ¡El circuito se ha ABIERTO! Bloqueando peticiones salientes.");
            return ValueTask.CompletedTask;
        },
        OnClosed = args =>
        {
            Console.WriteLine("[Circuit Breaker] El circuito se ha CERRADO. El servicio remoto vuelve a ser estable.");
            return ValueTask.CompletedTask;
        }
    });
}); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
