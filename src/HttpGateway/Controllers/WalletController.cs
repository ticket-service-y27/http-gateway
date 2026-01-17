using HttpGateway.Clients.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HttpGateway.Controllers;

[ApiController]
[Microsoft.AspNetCore.Components.Route("gateway/wallets")]
public class WalletController : ControllerBase
{
    private readonly IWalletGrpcClient _walletGrpcClient;

    public WalletController(IWalletGrpcClient walletGrpcClient)
    {
        _walletGrpcClient = walletGrpcClient;
    }

    [HttpPatch("{walletId:long}/top-up")]
    [Authorize(Roles = "user")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> TopUpWallet(
        [FromRoute][Required] long walletId,
        [FromQuery][Required] long amount,
        CancellationToken ct)
    {
        await _walletGrpcClient.TopUpWalletAsync(walletId, amount, ct);
        return Ok();
    }

    [HttpPatch("{walletId:long}/blocking")]
    [Authorize(Roles = "admin")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> SetBlockStatus(
        [FromRoute][Required] long walletId,
        [FromQuery][Required] bool isBlocked,
        CancellationToken ct)
    {
        await _walletGrpcClient.SetBlockStatusAsync(walletId, isBlocked, ct);
        return Ok();
    }
}