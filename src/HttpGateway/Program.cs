using HttpGateway;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentationGrpcClients(builder.Configuration)
    .AddSwaggerSettings()
    .AddMiddleware()
    .AddControllers();

WebApplication app = builder.Build();
app
    .UseSwaggerSettings()
    .UseMiddleware();
app.MapControllers();

app.Run();