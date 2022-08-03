import { TransactionPreview } from "./transaction-preview.model";

export class Wallet {

    WalletId : string = '';
    tokenAmount: number = 0;
    incomingTransactions: TransactionPreview[] = [];
    outgoingTransactions: TransactionPreview[] = [];
}
