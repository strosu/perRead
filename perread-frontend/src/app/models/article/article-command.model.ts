export class ArticleCommand {
    title: string = '';
    tags: string[] = [];
    price: number = 1;
    content: string = 'type your content here';
    articleImage: ArticleImage = new ArticleImage();
    sectionIds: string[] = [];
}

export class ArticleImage {
    fileName: string = '';
    base64Encoded: string = '';
}

export enum ArticleRecommendEnum {
    Clear,
    Yes,
    Not
}