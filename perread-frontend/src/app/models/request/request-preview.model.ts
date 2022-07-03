import { ArticlePreview } from "../article/article-preview.model";
import { AuthorPreview } from "../author/author-preview.model";

export class RequestPreview {
    requestId: string = '';
    title: string = '';
    description : string = '';
    targetAuthor: AuthorPreview = <AuthorPreview>{};
    pledgeCount: number = 0;
    pledgeAmount: number = 0;
    requestState : RequestState = <RequestState>{};
    postPublishState: RequestPostPublishState = <RequestPostPublishState>{};
    resultingArticle: ArticlePreview = <ArticlePreview>{};
    deadLine?: Date;
    createdAt? : Date;
}

export enum RequestPostPublishState {
    Public = "Public",
    ProfitShare = "Profit Share",
    Exclusive = "Exclusive" 
  }

  export enum RequestState {
    Created, 
    Accepted, 
    Completed, 
    Rejected,
    Expired,
    Cancelled
  }