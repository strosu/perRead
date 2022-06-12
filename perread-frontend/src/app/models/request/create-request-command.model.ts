import { PledgeCommand } from "../pledge/pledge--command.model";
import { RequestCommand } from "./request-command.model";

export class CreateRequestCommand {
    requestCommand: RequestCommand = <RequestCommand>{};
    pledgeCommand: PledgeCommand = <PledgeCommand>{};
}
