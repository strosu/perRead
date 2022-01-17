import { Author } from "./author.model";

export class Article {
    articleId? : any;
    title?: string;
    content? : string;
    price? : number;
    published?: boolean;
    authors?: Author[];
}
