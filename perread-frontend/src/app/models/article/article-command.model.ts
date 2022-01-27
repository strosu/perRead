export class ArticleCommand {
    title: string = '';
    tags: string[] = [];
    price: number = 1;
    content: string = 'type your content here';
    articleImage: ArticleImage = new ArticleImage();
}

export class ArticleImage {
    fileName: string = '';
    base64Encoded: string = '';
}