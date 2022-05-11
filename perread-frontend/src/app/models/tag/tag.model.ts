import { ArticlePreview } from "../article/article-preview.model";

export class Tag {
    id?: number;
    name?: string;
    firstUsage?: Date;
    articlePreviews?: ArticlePreview[];
}