#pragma warning disable IDE0072
using Grpc.Core;
using HttpGateway.Models.Users;
using UserService.Users.Contracts;

namespace HttpGateway;

public static class Mappings
{
    public static UserRoleGrpc MapUserRole(this UserRoleDto role)
    {
        return role switch
        {
            UserRoleDto.User => UserRoleGrpc.User,
            UserRoleDto.Admin => UserRoleGrpc.Admin,
            UserRoleDto.Organizer => UserRoleGrpc.Organizer,
            _ => UserRoleGrpc.Unspecified,
        };
    }

    public static int MapHttpStatus(this StatusCode statusCode)
    {
        return statusCode switch
        {
            StatusCode.Internal => StatusCodes.Status500InternalServerError,
            StatusCode.NotFound => StatusCodes.Status404NotFound,
            StatusCode.AlreadyExists => StatusCodes.Status409Conflict,
            StatusCode.InvalidArgument => StatusCodes.Status400BadRequest,
            StatusCode.Unauthenticated => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError,
        };
    }
}