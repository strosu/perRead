export class TransactionPreview {

    transactionId: string = '';
    sourceWalletId: string = '';
    destinationWalletId: string = '';
    transactionType: number = 0; // TODO - this should be an enum
    tokenAmount: number = 0;
    transactionDate: Date = <Date>{};
}
