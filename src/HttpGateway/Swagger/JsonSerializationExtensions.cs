using System.Text.Json.Serialization;

namespace HttpGateway.Swagger;

public static class JsonSerializationExtensions
{
    public static IMvcBuilder AddJson(this IMvcBuilder builder)
    {
        builder
            .AddJsonOptions(o =>
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)));
        return builder;
    }
}