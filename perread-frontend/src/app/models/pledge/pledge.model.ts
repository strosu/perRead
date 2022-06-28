import { AuthorPreview } from "../author/author-preview.model";

export class Pledge {

    requestPledgeId: string = '';
    parentRequest: Request = <Request>{};
    pledger: AuthorPreview = <AuthorPreview>{};
    totalTokenSum: number = 0;
    tokensOnAccept: number = 0;
    createdAt: Date = <Date>{};
}
