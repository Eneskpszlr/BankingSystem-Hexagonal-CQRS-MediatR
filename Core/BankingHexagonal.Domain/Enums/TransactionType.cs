namespace BankingHexagonal.Domain.Enums
{
    public enum TransactionType
    {
        Deposit = 1,        // Para Yatırma (+)
        Withdraw = 2,       // Para Çekme (-)
        TransferOut = 3,    // Havale/EFT Gönderimi (-)
        TransferIn = 4,     // Havale/EFT Gelimi (+)
        Payment = 5         // Fatura Ödeme (-)
    }
}
