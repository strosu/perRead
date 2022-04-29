import { SectionPreview } from "./section/section-preview.model";

export class Author {
    id?: number;
    name?: string;
    sectionPreviews?: SectionPreview[];
    authorImageUri?: string;
}
