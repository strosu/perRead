import { AuthorPreview } from "./author-preview.model";
import { TagPreview } from "./tag-preview.model";

export class ArticlePreview {
    articleId: number = 0;
    authorPreviews: AuthorPreview[] = [];
    tagPreviews?: TagPreview[];

    articleTitle: string = '';
    articleCreatedAt: Date = new Date();
    articlePreview?: string;
    articlePrice: number = 0;
}