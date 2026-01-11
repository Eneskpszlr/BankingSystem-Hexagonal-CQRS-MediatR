export interface TransferRequest {
    fromAccountId: number; 
    toAccountNumber: string;
    amount: number;
    currencyCode: string;
    description: string;
}
