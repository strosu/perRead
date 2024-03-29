import { PledgeCommand } from "../pledge/pledge--command.model";
import { RequestCommand } from "./request-command.model";

export class CreateRequestCommand {
    targetAuthorId: string = '';
    requestCommand: RequestCommand = <RequestCommand>{};
    pledgeCommand: PledgeCommand = <PledgeCommand>{};
}

export class CompleteRequestCommand {
    requestId: string = '';
    resultingArticleId: string = '';
}
