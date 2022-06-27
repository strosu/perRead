import { ArticlePreview } from "../article/article-preview.model";
import { AuthorPreview } from "../author/author-preview.model";
import { RequestPostPublishState, RequestState } from "./request-preview.model";

export class Request {
    requestId : string = '';
    title: string = '';
    description: string = '';
    authorPreview: AuthorPreview = <AuthorPreview>{};
    pledgeCount: number = 0;
    pledgeAmount: number = 0;
    targetAuthor: AuthorPreview = <AuthorPreview>{};
    requestState: RequestState = <RequestState>{};
    postPublishState: RequestPostPublishState = <RequestPostPublishState>{};
    resultingArticle: ArticlePreview = <ArticlePreview>{};
    deadLine?: Date;
    createdAt? : Date;
}
