import { AuthorPreview } from "./author-preview.model";

export class ArticleOwner {
    ownerPreview: AuthorPreview = <AuthorPreview>{};
    ownershipPercent: number = 0;
    canBeEdited: boolean = false;
    isUserFacing: boolean = false;
}
