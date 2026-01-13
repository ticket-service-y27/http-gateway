using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace HttpGateway.Authentication;

public static class JwtServiceCollectionExtensions
{
    public static IServiceCollection AddJwtTokenAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtAuthenticationOptions>(
            JwtBearerDefaults.AuthenticationScheme,
            configuration.GetSection("Authentication:JwtOptions"));

        services.AddAuthorization();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerOptionsConfigurator>();

        return services;
    }
}