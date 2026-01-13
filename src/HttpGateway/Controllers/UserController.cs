using HttpGateway.Clients;
using HttpGateway.Models.Users;
using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("login")]
    public async Task<ActionResult<string>> LogInByNickname(
        string nickname,
        string password,
        CancellationToken ct)
    {
        return await _userGrpcClient.LogInByNicknameAsync(nickname, password, ct);
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
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> AssignUserRole(
        [FromRoute][Required] long userId,
        [FromQuery][Required] UserRoleDto role,
        CancellationToken ct)
    {
        await _userGrpcClient.AssignUserRoleAsync(userId, role, ct);
        return Ok();
    }

    [HttpPatch("{userId:long}/block")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> BlockUserByIdAsync(long userId, CancellationToken ct)
    {
        await _userGrpcClient.BlockUserByIdAsync(userId, ct);
        return Ok();
    }
}