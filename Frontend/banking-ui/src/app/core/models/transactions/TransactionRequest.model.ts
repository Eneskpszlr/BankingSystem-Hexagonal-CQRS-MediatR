export interface TransactionRequest {
    accountId: number;
    amount: number;
    currencyCode: string;
    description: string;
}