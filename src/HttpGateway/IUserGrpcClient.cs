using HttpGateway.Models.Users;

namespace HttpGateway;

public interface IUserGrpcClient
{
    Task<long> CreateUserAsync(string nickname, string email, string password, CancellationToken ct);

    Task AssignUserRoleAsync(long userId, UserRoleDto role, CancellationToken ct);

    Task BlockUserByIdAsync(long userId, CancellationToken ct);

    Task<long> LogInByNicknameAsync(string nickname, string password, CancellationToken ct);
}