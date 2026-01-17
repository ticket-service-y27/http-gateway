using HttpGateway.Models.Users;
using Users.UserService.Contracts;

namespace HttpGateway.Clients.GrpcClients;

public class UserGrpcClient : IUserGrpcClient
{
    private readonly Users.UserService.Contracts.UserService.UserServiceClient _client;

    public UserGrpcClient(Users.UserService.Contracts.UserService.UserServiceClient client)
    {
        _client = client;
    }

    public async Task<string> LogInByNicknameAsync(string nickname, string password, CancellationToken ct)
    {
        LogInByNicknameResponse response = await _client.LogInByNicknameAsync(
            new LogInByNicknameRequest
            {
                Nickname = nickname,
                Password = password,
            },
            cancellationToken: ct);
        return response.JwtAccessToken;
    }

    public async Task<long> CreateUserAsync(string nickname, string email, string password, CancellationToken ct)
    {
        CreateUserResponse response = await _client.CreateUserAsync(
            new CreateUserRequest
            {
                Nickname = nickname,
                Email = email,
                Password = password,
            },
            cancellationToken: ct);

        return response.UserId;
    }

    public async Task AssignUserRoleAsync(long userId, UserRoleDto role, CancellationToken ct)
    {
        await _client.AssignUserRoleAsync(
            new AssignUserRoleRequest
            {
                UserId = userId,
                Role = role.MapUserRole(),
            },
            cancellationToken: ct);
    }

    public async Task BlockUserByIdAsync(long userId, CancellationToken ct)
    {
        await _client.BlockUserByIdAsync(
            new BlockUserByIdRequest
            {
                UserId = userId,
            },
            cancellationToken: ct);
    }

    public async Task UnblockUserByIdAsync(long userId, CancellationToken ct)
    {
        await _client.UnblockUserByIdAsync(
            new UnblockUserByIdRequest
            {
                UserId = userId,
            },
            cancellationToken: ct);
    }

    public async Task<UserLoyaltyLevelDto> GetUserLoyaltyLevelAsync(long userId, CancellationToken ct)
    {
        GetUserLoyaltyLevelResponse response = await _client.GetUserLoyaltyLevelAsync(
            new GetUserLoyaltyLevelRequest
            {
                UserId = userId,
            },
            cancellationToken: ct);

        return response.Level.MapUserLoyaltyLevel();
    }
}