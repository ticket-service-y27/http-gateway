using HttpGateway.Models.Users;

namespace HttpGateway.Clients;

public interface IUserGrpcClient
{
    Task<string> LogInByNicknameAsync(string nickname, string password, CancellationToken ct);

    Task<long> CreateUserAsync(string nickname, string email, string password, CancellationToken ct);

    Task AssignUserRoleAsync(long userId, UserRoleDto role, CancellationToken ct);

    Task BlockUserByIdAsync(long userId, CancellationToken ct);

    Task UnblockUserByIdAsync(long userId, CancellationToken ct);
}