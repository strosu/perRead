import { ArticlePreview } from "../article/article-preview.model";
import { SectionPreview } from "../section/section-preview.model";

export class Author {
    id: string = ``;
    name?: string;
    sectionPreviews: SectionPreview[] = [];
    authorImageUri?: string;
    articleCount: number = 0;
    about: string = '';
    latestArticles: ArticlePreview[] = [];
}
