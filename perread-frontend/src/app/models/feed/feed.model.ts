import { ArticlePreview } from "../article/article-preview.model";

export class Feed {
    feedId? : string;
    feedName? : string;
    articlePreviews? : ArticlePreview[];
}
