using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HttpGateway.Authentication;

public class JwtBearerOptionsConfigurator : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IOptionsMonitor<JwtAuthenticationOptions> _jwtOptionsMonitor;

    public JwtBearerOptionsConfigurator(IOptionsMonitor<JwtAuthenticationOptions> jwtOptions)
    {
        _jwtOptionsMonitor = jwtOptions;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme)
            return;

        JwtAuthenticationOptions jwtOptions = _jwtOptionsMonitor.Get(JwtBearerDefaults.AuthenticationScheme);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
            RoleClaimType = "role",
        };

        options.MapInboundClaims = false;
    }

    public void Configure(JwtBearerOptions options)
    {
        Configure(JwtBearerDefaults.AuthenticationScheme, options);
    }
}