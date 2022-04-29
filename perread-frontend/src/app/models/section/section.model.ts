import { ArticlePreview } from "../article/article-preview.model";

export class Section {
    sectionId: string = '';
    Name: string = '';
    description: string = '';
    articlePreviews?: ArticlePreview[];
}
