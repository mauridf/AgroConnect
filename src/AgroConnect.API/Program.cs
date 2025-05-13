using AgroConnect.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configura todos os servi�os
builder.ConfigureServices();

var app = builder.Build();

// Configura o pipeline HTTP
app.ConfigurePipeline();

app.Run();