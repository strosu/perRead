import { Time } from "@angular/common";
import { ArticlePreview } from "../article/article-preview.model";

export class ArticleUnlockInfo {
    articleUnlockId: number = 0;
    article: ArticlePreview = <ArticlePreview>{};
    aquisitionPrice: number = 0;
    aquisitionDate?: Date;
}