export class TransactionPreview {

    transactionId: string = '';
    sourceWalletId: string = '';
    destinationWalletId: string = '';
    transactionType: TransactionType = <TransactionType>{}; // TODO - this should be an enum
    tokenAmount: number = 0;
    transactionDate: Date = <Date>{};
    comment: string = '';
    isSender: boolean = false;
}

export enum TransactionType {
    TokenPurchase = "TokenPurchase",
    TokenWithdrawal = "TokenWithdrawal",
    ArticleRead = "ArticleRead",
    PledgeCreated = "PledgeCreated",
    PledgeCancelled = "PledgeCancelled",
    PledgeEdited = "PledgeEdited",
    RequestAccepted = "RequestAccepted",
    RequestCompleted = "RequestCompleted",
    RequestCancelled = "RequestCancelled",
    RequestRefused = "RequestRefused"
  }