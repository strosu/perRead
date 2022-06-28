import { AuthorPreview } from "../author/author-preview.model";

export class PledgePreview {
    requestPledgeId: string = '';
    pledges: AuthorPreview = <AuthorPreview>{};
    totalTokenSum: number = 0;
}
