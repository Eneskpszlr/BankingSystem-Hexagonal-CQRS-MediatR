export interface CreateAccountRequest {
    customerId: number;
    branchId: number;
    initialBalance: number;
    currencyCode: string;
}