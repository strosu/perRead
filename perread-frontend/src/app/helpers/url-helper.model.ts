export class UrlHelper {
    static getPledgeUrl(pledgeId: string) : string {
        return `/pledges/${pledgeId}`;
    }

    static getRequestUrl(requestId: string) : string {
        return `/requests/${requestId}`;
    }

    static getArticleUrl(articleId: string) : string {
        return `/articles/${articleId}`;
    }
}
