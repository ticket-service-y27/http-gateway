using HttpGateway.Clients;
using HttpGateway.Models.Users;
using HttpGateway.Models.Users.Requests;
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
    [AllowAnonymous]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<string>> LogInByNickname(
        [FromBody] LoginRequest request,
        CancellationToken ct)
    {
        string token = await _userGrpcClient.LogInByNicknameAsync(request.Nickname, request.Password, ct);
        return Ok(token);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
    [ProducesResponseType(statusCode: StatusCodes.Status409Conflict)]
    [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<long>> CreateUser(
        [FromBody] CreateUserRequest request,
        CancellationToken ct)
    {
        return Ok(await _userGrpcClient.CreateUserAsync(request.Nickname, request.Email, request.Password, ct));
    }

    [HttpPatch("{userId:long}/role/{role}")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public async Task<ActionResult> AssignUserRole(
        [FromRoute][Required] long userId,
        [FromRoute][Required] UserRoleDto role,
        CancellationToken ct)
    {
        await _userGrpcClient.AssignUserRoleAsync(userId, role, ct);
        return Ok();
    }

    [HttpPatch("{userId:long}/block")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    public async Task<ActionResult> BlockUserById(
        [FromRoute][Required] long userId,
        CancellationToken ct)
    {
        await _userGrpcClient.BlockUserByIdAsync(userId, ct);
        return Ok();
    }

    [HttpPatch("{userId:long}/unblock")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    [ProducesResponseType(statusCode: StatusCodes.Status412PreconditionFailed)]
    public async Task<ActionResult> UnblockUserById(
        [FromRoute][Required] long userId,
        CancellationToken ct)
    {
        await _userGrpcClient.UnblockUserByIdAsync(userId, ct);
        return Ok();
    }

    [HttpGet("{userId:long}/level")]
    [Authorize(Roles = "user, admin")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden)]
    [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
    [ProducesResponseType(statusCode: StatusCodes.Status502BadGateway)]
    public async Task<ActionResult<UserLoyaltyLevelDto>> GetUserLoyaltyLevel(
        [FromRoute][Required] long userId,
        CancellationToken ct)
    {
        return Ok(await _userGrpcClient.GetUserLoyaltyLevelAsync(userId, ct));
    }
}