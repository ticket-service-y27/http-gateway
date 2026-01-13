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
    .AddControllers();

WebApplication app = builder.Build();
app
    .UseSwaggerSettings()
    .UseMiddleware();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();