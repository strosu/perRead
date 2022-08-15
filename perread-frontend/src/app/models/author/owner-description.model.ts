export class OwnerDescription {
    authorId: string = '';
    ownershipPercent: number = 0;
}

export class UpdateOwnershipCommand {
    owners: OwnerDescription[] = [];
}