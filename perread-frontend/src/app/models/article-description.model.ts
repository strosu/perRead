import { Author } from "./author.model";
import { Tag } from "./tag.model";

export class ArticleDescription {
    articleId: number = 0;
    title: string = '';
    authors?: Author[] = [];
    price: number = 0;
    tags: Tag[] = [];
}