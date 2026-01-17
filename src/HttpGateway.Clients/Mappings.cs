using HttpGateway.Models.Users;
using HttpGateway.Models.Wallets;
using PaymentService.Grpc.WalletTransactions;
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

    public static TransactionTypeDto MapTransactionType(this TransactionType type)
    {
        return type switch
        {
            TransactionType.Topup => TransactionTypeDto.Topup,
            TransactionType.Payment => TransactionTypeDto.Payment,
            TransactionType.Refund => TransactionTypeDto.Refund,
            TransactionType.Unspecified =>
                throw new InvalidOperationException("PaymentService returned UNSPECIFIED transaction type"),
            _ => throw new InvalidOperationException($"Unknown transaction type {type}"),
        };
    }
}