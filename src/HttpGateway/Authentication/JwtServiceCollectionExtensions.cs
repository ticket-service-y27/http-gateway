using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HttpGateway.Authentication;

public static class JwtServiceCollectionExtensions
{
    public static IServiceCollection AddJwtTokenAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtAuthenticationOptions>(
            configuration.GetSection("Authentication:JwtOptions"));

        services.AddAuthorization();
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                using ServiceProvider sp = services.BuildServiceProvider();

                JwtAuthenticationOptions options = sp.GetRequiredService<IOptions<JwtAuthenticationOptions>>().Value;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = options.Issuer,
                    ValidateAudience = true,
                    ValidAudience = options.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)),
                    RoleClaimType = "role",
                };

                o.MapInboundClaims = false;
            });

        return services;
    }
}