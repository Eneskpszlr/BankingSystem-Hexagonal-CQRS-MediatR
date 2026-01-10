import { TransactionRequest } from "./TransactionRequest.model";

export interface TransferRequest extends TransactionRequest {
    targetAccountId: number;
}
