import { AuthorPreview } from "../author/author-preview.model";
import { SectionPreview } from "../section/section-preview.model";
import { TagPreview } from "../tag/tag-preview.model";

export class Article {
    id? : any;
    title?: string;
    content? : string;
    price? : number;
    createdAt?: boolean;
    authorPreviews?: AuthorPreview[];
    tagPreviews?: TagPreview[];
    articleImageUrl: string = '';
    sectionPreviews: SectionPreview[] = [];
    recommendsReadingCount: number = 0;
    notRecommendsReadingCount: number = 0;
    currentUserRecommends: boolean | null = null;
}
