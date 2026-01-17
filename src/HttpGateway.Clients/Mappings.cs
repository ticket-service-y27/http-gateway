using HttpGateway.Models.Users;
using Users.UserService.Contracts;

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

    public static UserLoyaltyLevelDto MapUserLoyaltyLevel(this UserLoyaltyLevelGrpc level)
    {
        return level switch
        {
            UserLoyaltyLevelGrpc.Bronze => UserLoyaltyLevelDto.Bronze,
            UserLoyaltyLevelGrpc.Silver => UserLoyaltyLevelDto.Silver,
            UserLoyaltyLevelGrpc.Gold => UserLoyaltyLevelDto.Gold,
            UserLoyaltyLevelGrpc.Platinum => UserLoyaltyLevelDto.Platinum,
            UserLoyaltyLevelGrpc.Unspecified =>
                throw new InvalidOperationException("UserService returned UNSPECIFIED loyalty level"),
            _ => throw new InvalidOperationException($"Unknown loyalty level: {level}"),
        };
    }
}