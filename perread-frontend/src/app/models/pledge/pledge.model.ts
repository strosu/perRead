import { AuthorPreview } from "../author/author-preview.model";
import { ArticleRequest } from "../request/request.model";

export class Pledge {

    requestPledgeId: string = '';
    parentRequest: ArticleRequest = <ArticleRequest>{};
    pledger: AuthorPreview = <AuthorPreview>{};
    totalTokenSum: number = 0;
    tokensOnAccept: number = 0;
    createdAt: Date = <Date>{};
}
