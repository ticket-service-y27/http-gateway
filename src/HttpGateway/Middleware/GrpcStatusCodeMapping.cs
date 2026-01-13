#pragma warning disable IDE0072
using Grpc.Core;

namespace HttpGateway.Middleware;

public static class GrpcStatusCodeMapping
{
    public static int MapHttpStatus(this StatusCode statusCode)
    {
        return statusCode switch
        {
            StatusCode.Internal => StatusCodes.Status500InternalServerError,
            StatusCode.NotFound => StatusCodes.Status404NotFound,
            StatusCode.AlreadyExists => StatusCodes.Status409Conflict,
            StatusCode.InvalidArgument => StatusCodes.Status400BadRequest,
            StatusCode.Unauthenticated => StatusCodes.Status401Unauthorized,
            StatusCode.PermissionDenied => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };
    }
}