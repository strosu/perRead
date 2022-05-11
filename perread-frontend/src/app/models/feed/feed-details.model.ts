import { AuthorPreview } from "../author/author-preview.model";

export class FeedDetails {
    feedId : string = '';
    feedName? : string;
    authorPreviews?: AuthorPreview[];
    requireConfirmationAbove? : number;
    showFreeArticles?: boolean;
    showArticlesAboveConfirmationLimit?: boolean;
    showUnaffordableArticles? : boolean;
}
