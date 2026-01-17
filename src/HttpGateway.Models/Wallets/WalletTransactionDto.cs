namespace HttpGateway.Models.Wallets;

public record WalletTransactionDto(
    long TransactionId,
    long WalletId,
    TransactionTypeDto Type,
    long Amount,
    long PaymentId,
    DateTimeOffset CreatedAt);