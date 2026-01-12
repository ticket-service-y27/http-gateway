using HttpGateway.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HttpGateway.Controllers;

[ApiController]
[Route("gateway/users")]
public class UserController : ControllerBase
{
    private readonly IUserGrpcClient _userGrpcClient;

    public UserController(IUserGrpcClient userGrpcClient)
    {
        _userGrpcClient = userGrpcClient;
    }

    [HttpPost]
    public async Task<ActionResult<long>> CreateUser(
        [FromQuery][Required] string nickname,
        [FromQuery][Required] string email,
        [FromQuery][Required] string password,
        CancellationToken ct)
    {
        return Ok(await _userGrpcClient.CreateUserAsync(nickname, email, password, ct));
    }

    [HttpPatch("{userId:long}/role")]
    public async Task<ActionResult> AssignUserRole(
        [FromRoute][Required] long userId,
        [FromQuery][Required] UserRoleDto role,
        CancellationToken ct)
    {
        await _userGrpcClient.AssignUserRoleAsync(userId, role, ct);
        return Ok();
    }

    [HttpPatch("{userId:long}/block")]
    public async Task<ActionResult> BlockUserByIdAsync(long userId, CancellationToken ct)
    {
        await _userGrpcClient.BlockUserByIdAsync(userId, ct);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<long>> LogInByNickname(
        string nickname,
        string password,
        CancellationToken ct)
    {
        return await _userGrpcClient.LogInByNicknameAsync(nickname, password, ct);
    }
}