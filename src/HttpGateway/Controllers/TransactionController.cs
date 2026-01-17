using HttpGateway.Clients.Abstractions;
using HttpGateway.Models.Wallets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HttpGateway.Controllers;

[ApiController]
[Route("gateway/transactions")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionGrpcClient _transactionGrpcClient;

    public TransactionController(ITransactionGrpcClient transactionGrpcClient)
    {
        _transactionGrpcClient = transactionGrpcClient;
    }

    [HttpGet("{walletId}/transactions")]
    [Authorize(Roles = "admin,user,organizer")]
    [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(statusCode: StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(statusCode: StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<WalletTransactionDto>>> GetByWalletId(
        [FromRoute][Required] long walletId,
        [FromQuery][Required] long cursor,
        CancellationToken ct)
    {
        IEnumerable<WalletTransactionDto> items = await _transactionGrpcClient.GetByWalletIdAsync(walletId, cursor, ct);
        return Ok(items);
    }
}