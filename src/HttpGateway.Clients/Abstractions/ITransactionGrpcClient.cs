using HttpGateway.Models.Wallets;

namespace HttpGateway.Clients.Abstractions;

public interface ITransactionGrpcClient
{
    Task<IEnumerable<WalletTransactionDto>> GetByWalletIdAsync(
        long walletId,
        long cursor,
        CancellationToken ct);
}