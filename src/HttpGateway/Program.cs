using HttpGateway.Authentication;
using HttpGateway.Clients;
using HttpGateway.Middleware;
using HttpGateway.Swagger;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddGrpcClients(builder.Configuration)
    .AddSwaggerSettings()
    .AddMiddleware()
    .AddJwtTokenAuthentication(builder.Configuration)
    .AddControllers()
    .AddJson();

WebApplication app = builder.Build();
app
    .UseSwaggerSettings()
    .UseAuthentication()
    .UseAuthorization()
    .UseMiddleware();
app.MapControllers();
app.Run();