import { RequestPostPublishState } from "./request-preview.model";

export class RequestCommand {
    requestId: string = '';
    title: string = '';
    description: string = '';
    postPublishState: RequestPostPublishState = <RequestPostPublishState>{};
    percentForPledgers: number = 0;
    deadline: Date = <Date>{};
}
