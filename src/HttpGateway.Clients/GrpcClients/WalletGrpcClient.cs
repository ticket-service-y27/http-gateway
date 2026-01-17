using HttpGateway.Clients.Abstractions;
using PaymentService.Grpc.Wallets;

namespace HttpGateway.Clients.GrpcClients;

public class WalletGrpcClient : IWalletGrpcClient
{
    private readonly PaymentService.Grpc.Wallets.WalletService.WalletServiceClient _client;

    public WalletGrpcClient(PaymentService.Grpc.Wallets.WalletService.WalletServiceClient client)
    {
        _client = client;
    }

    public async Task TopUpWalletAsync(long walletId, long amount, CancellationToken ct)
    {
        await _client.TopUpWalletAsync(
            new TopUpWalletRequest
            {
                WalletId = walletId,
                Amount = amount,
            },
            cancellationToken: ct);
    }

    public async Task SetBlockStatusAsync(long walletId, bool isBlocked, CancellationToken ct)
    {
        await _client.SetBlockStatusAsync(
            new SetBlockStatusRequest
            {
                WalletId = walletId,
                IsBlocked = isBlocked,
            },
            cancellationToken: ct);
    }
}