import { TransactionPreview } from "./transaction-preview.model";

export class Wallet {

    walletId : string = '';
    tokenAmount: number = 0;
    incomingTransactions: TransactionPreview[] = [];
    outgoingTransactions: TransactionPreview[] = [];
}
