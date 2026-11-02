using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using FiyiStack.APIWithSOAP.Areas.Client.Interfaces;
using FiyiStack.APIWithSOAP.Areas.Client.Services;
using FiyiStack.APIWithSOAP.Areas.Product.Interfaces;
using FiyiStack.APIWithSOAP.Areas.Product.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();

builder.Services
    .AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();  

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<ClientService>();


    serviceBuilder.AddServiceEndpoint<ClientService, IClientService>(
        new BasicHttpBinding(), "/ClientService.svc"
        );

    serviceBuilder.AddService<ProductService>();

    serviceBuilder.AddServiceEndpoint<ProductService, IProductService>(
        new BasicHttpBinding(), "/ProductService.svc"
        );
});

var metadata = app.Services.GetRequiredService<ServiceMetadataBehavior>();

metadata.HttpGetEnabled = true;

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
