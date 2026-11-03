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
    client.Timeout = TimeSpan.FromSeconds(15);
});


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
