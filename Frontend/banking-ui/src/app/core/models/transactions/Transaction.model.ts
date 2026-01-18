export interface Transaction {
    id: number;
    accountId: number;
    targetAccountId?: number;
    amount: number;
    currencyCode: string;
    description: string;
    transactionType: string; // "Deposit", "Withdraw", "TransferOut"
    referenceNumber: string;
    createdDate: string;
}