import { ArticlePreview } from "../article/article-preview.model";
import { AuthorPreview } from "../author/author-preview.model";
import { PledgePreview } from "../pledge/pledge-preview.model";
import { RequestPostPublishState, RequestState } from "./request-preview.model";

export class ArticleRequest {
    requestId : string = '';
    title: string = '';
    description: string = '';
    pledgeCount: number = 0;
    pledgeAmount: number = 0;
    targetAuthor: AuthorPreview = <AuthorPreview>{};
    requestState: RequestState = <RequestState>{};
    postPublishState: RequestPostPublishState = <RequestPostPublishState>{};
    resultingArticle: ArticlePreview = <ArticlePreview>{};
    deadLine?: Date;
    createdAt? : Date;
    pledgePreviews: PledgePreview[] = [];
}
