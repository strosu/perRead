import { WalletPreview } from "../wallet/wallet-preview.model";

export class UserPreview {
    userId: string = '';
    userName: string = '';
    profileImageUri: string = '';
    readingWallet: WalletPreview = <WalletPreview>{};
}
