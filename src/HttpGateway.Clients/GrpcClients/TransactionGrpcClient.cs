using HttpGateway.Clients.Abstractions;
using HttpGateway.Models.Wallets;
using PaymentService.Grpc.WalletTransactions;

namespace HttpGateway.Clients.GrpcClients;

public class TransactionGrpcClient : ITransactionGrpcClient
{
    private readonly PaymentService.Grpc.WalletTransactions.WalletTransactionsService.WalletTransactionsServiceClient _client;

    public TransactionGrpcClient(
        PaymentService.Grpc.WalletTransactions.WalletTransactionsService.WalletTransactionsServiceClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<WalletTransactionDto>> GetByWalletIdAsync(
        long walletId,
        long cursor,
        CancellationToken ct)
    {
        GetTransactionsByWalletIdResponse response = await _client.GetByWalletIdAsync(
            new GetTransactionsByWalletIdRequest
            {
                WalletId = walletId,
                Cursor = cursor,
            },
            cancellationToken: ct);

        WalletTransaction? transaction = response.Transaction.FirstOrDefault();

        IEnumerable<WalletTransactionDto> items = response.Transaction.Select(item =>
            new WalletTransactionDto(
                item.TransactionId,
                item.WalletId,
                item.Type.MapTransactionType(),
                item.Amount,
                item.PaymentId,
                item.CreatedAt.ToDateTimeOffset()));

        return items;
    }
}