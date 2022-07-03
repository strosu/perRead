import { AuthorPreview } from "../author/author-preview.model";

export class PledgePreview {
    requestPledgeId: string = '';
    pledger: AuthorPreview = <AuthorPreview>{};
    totalTokenSum: number = 0;
}
