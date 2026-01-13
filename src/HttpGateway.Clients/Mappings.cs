using HttpGateway.Models.Users;
using UserService.Users.Contracts;

namespace HttpGateway.Clients;

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
}