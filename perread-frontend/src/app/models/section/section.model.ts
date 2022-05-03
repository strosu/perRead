import { ArticlePreview } from "../article/article-preview.model";
import { FeedPreview } from "../feed/feed-preview.model";

export class Section {
    sectionId: string = '';
    Name: string = '';
    description: string = '';
    articlePreviews: ArticlePreview[] = [];
    feedSubscriptionStatuses: SectionSubscriptionStatus[] = [];
}

export class SectionSubscriptionStatus {
    feed: FeedPreview = <FeedPreview>{};
    isSubscribedToSection: boolean = false;
}