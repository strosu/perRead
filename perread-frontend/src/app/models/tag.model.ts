import { ArticlePreview } from "./article-preview.model";

export class Tag {
    id?: number;
    name?: string;
    firstUsage?: Date;
    articlePreviews?: ArticlePreview[];
}