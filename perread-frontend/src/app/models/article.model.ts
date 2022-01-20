import { AuthorPreview } from "./author-preview.model";
import { TagPreview } from "./tag-preview.model";

export class Article {
    id? : any;
    title?: string;
    content? : string;
    price? : number;
    createdAt?: boolean;
    authorPreviews?: AuthorPreview[];
    tagPreviews?: TagPreview[];
}
