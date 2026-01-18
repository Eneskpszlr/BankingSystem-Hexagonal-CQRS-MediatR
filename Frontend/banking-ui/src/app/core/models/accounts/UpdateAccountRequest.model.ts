export interface UpdateAccountRequest {
    id: number;
    name?: string;
    status?: number | string;
    accountNumber?: string;
    customerId?: number;
    branchId?: number;
    currencyCode?: string
}