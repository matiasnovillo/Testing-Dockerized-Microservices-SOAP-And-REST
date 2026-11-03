using FiyiStack.APIWithREST.Areas.Movie.Consumers;
using FiyiStack.APIWithREST.EmailService;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MovieConsultedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        // Configure the receive endpoint
        cfg.ReceiveEndpoint("movie_consulted_queue", e =>
        {
            e.PrefetchCount = 1; // Fetch one message at a time
            e.UseConcurrencyLimit(1); // Process one message at a time
            e.ConfigureConsumer<MovieConsultedConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
