namespace HttpGateway.Clients.Abstractions;

public interface IWalletGrpcClient
{
    Task TopUpWalletAsync(long walletId, long amount, CancellationToken ct);

    Task SetBlockStatusAsync(long walletId, bool isBlocked, CancellationToken ct);
}