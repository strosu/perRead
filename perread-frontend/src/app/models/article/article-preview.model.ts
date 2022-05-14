import { AuthorPreview } from "../author/author-preview.model";
import { SectionPreview } from "../section/section-preview.model";
import { TagPreview } from "../tag/tag-preview.model";

export class ArticlePreview {
    articleId: number = 0;
    authorPreviews: AuthorPreview[] = [];
    tagPreviews?: TagPreview[];

    articleTitle: string = '';
    articleCreatedAt: Date = new Date();
    articlePreview?: string;
    articlePrice: number = 0;
    articleImageUrl: string = '';
    readingState: string = '';
    sectionPreviews: SectionPreview[] = [];
}

export enum ReadingState {
    Purchased, 
    WithinBuyingLimit, 
    OutsideOfLimitButAffordable, 
    Unaffordable
  }