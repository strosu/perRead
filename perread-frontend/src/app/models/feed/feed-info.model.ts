import { AuthorPreview } from "../author-preview.model";

export class FeedInfo {
    feedId? : string;
    feedName? : string;
    authorPreviews?: AuthorPreview[];
}
