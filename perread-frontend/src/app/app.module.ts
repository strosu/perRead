import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddArticleComponent } from './components/article/add-article/add-article.component';
import { ArticleDetailsComponent } from './components/article/article-details/article-details.component';
import { ArticleListComponent } from './components/article/article-list/article-list.component';

import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';

import { authInterceptorProviders } from './helpers/auth.interceptor';
import { ArticlePreviewComponent } from './components/article/article-preview/article-preview.component';
import { AuthorPreviewComponent } from './components/author/author-preview/author-preview.component';
import { TagPreviewComponent } from './components/tags/tag-preview/tag-preview.component';
import { AuthorDetailsComponent } from './components/author/author-details/author-details.component';
import { TagDetailsComponent } from './components/tags/tag-details/tag-details.component';
import { AllArticlesComponent } from './components/article/all-articles/all-articles.component';
import { TagListComponent } from './components/tags/tag-list/tag-list.component';
import { PopularTagsComponent } from './components/tags/popular-tags/popular-tags.component';
import { UserNavMenuComponent } from './components/user/user-nav-menu/user-nav-menu.component';
import { NavbarComponent } from './components/nav/navbar/navbar.component';
import { FeedListComponent } from './components/feed/feed-list/feed-list.component';
import { FeedArticlesComponent } from './components/feed/feed-articles/feed-articles.component';
import { AboutComponent } from './components/home/about/about.component';
import { HomeComponent } from './components/home/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTabsModule } from '@angular/material/tabs';
import { MatListModule} from '@angular/material/list'; 
import { UserUnlockedComponent } from './components/user/user-unlocked/user-unlocked.component';
import { UserPreferencesComponent } from './components/user/user-preferences/user-preferences.component';
import { MatButtonModule } from '@angular/material/button';
import { MatSlideToggleModule } from '@angular/material/slide-toggle'; 
import { MatCardModule } from '@angular/material/card'; 
import { MatFormFieldModule } from '@angular/material/form-field'; 
import { UnlockedArticleComponent } from './components/user/unlocked-article/unlocked-article.component';
import { FeedManagementComponent } from './components/feed/feed-management/feed-management.component';
import { FeedDetailsComponent } from './components/feed/feed-details/feed-details.component';
import { AddFeedComponent } from './components/feed/add-feed/add-feed.component';
import { AddSectionComponent } from './components/sections/add-section/add-section.component';
import { SectionManagementComponent } from './components/sections/section-management/section-management.component';
import { SectionDetailsComponent } from './components/sections/section-details/section-details.component';
import { SectionArticlesComponent } from './components/sections/section-articles/section-articles.component';
import { SectionListComponent } from './components/sections/section-list/section-list.component';
import { SectionPreviewComponent } from './components/sections/section-preview/section-preview.component';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table'; 
import { MatGridListModule } from '@angular/material/grid-list';
import { SectionPageComponent } from './components/sections/section-page/section-page.component';
import { TokensPageComponent } from './components/payment/tokens-page/tokens-page.component'; 
import { AddTokensComponent } from './components/payment/add-tokens/add-tokens.component';
import { WithdrawTokensComponent } from './components/payment/withdraw-tokens/withdraw-tokens.component';
import { SectionCardComponent } from './components/sections/section-card/section-card.component';
import { AuthorCardComponent } from './components/author/author-card/author-card.component';
import { AuthorListComponent } from './components/author/author-list/author-list.component';
import { AllAuthorsComponentComponent } from './components/author/all-authors-component/all-authors-component.component';
import { RequestPreviewComponent } from './components/requests/request-preview/request-preview.component';
import { RequestDetailsComponent } from './components/requests/request-details/request-details.component';
import { RequestListComponent } from './components/requests/request-list/request-list.component';
import { AuthorRequestsComponent } from './components/requests/author-requests/author-requests.component';
import { AddRequestComponent } from './components/requests/add-request/add-request.component';
import { PledgePreviewComponent } from './components/pledges/pledge-preview/pledge-preview.component';
import { PledgeDetailsComponent } from './components/pledges/pledge-details/pledge-details.component';
import { PledgeListComponent } from './components/pledges/pledge-list/pledge-list.component';
import { AddPledgeComponent } from './components/pledges/add-pledge/add-pledge.component';
import { EditPledgeComponent } from './components/pledges/edit-pledge/edit-pledge.component';
import { EditRequestComponent } from './components/requests/edit-request/edit-request.component';
import { MainWalletDetailsComponent } from './components/wallet/main-wallet-details/main-wallet-details.component';
import { EscrowWalletDetailsComponent } from './components/wallet/escrow-wallet-details/escrow-wallet-details.component';
import { TransactionsListComponent } from './components/wallet/transactions-list/transactions-list.component';
import { ArticleOwnersComponent } from './components/article/article-owners/article-owners.component'; 
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { ArticleReadCommentComponent } from './components/wallet/transaction-comments/article-read-comment/article-read-comment.component';
import { PledgeCreatedCommentComponent } from './components/wallet/transaction-comments/pledge-created-comment/pledge-created-comment.component';
import { RequestAbandonedCommentComponent } from './components/wallet/transaction-comments/request-abandoned-comment/request-abandoned-comment.component';
import { RequestRefusedCommentComponent } from './components/wallet/transaction-comments/request-refused-comment/request-refused-comment.component';
import { RequestAcceptedCommentComponent } from './components/wallet/transaction-comments/request-accepted-comment/request-accepted-comment.component';
import { TokenPurchaseCommentComponent } from './components/wallet/transaction-comments/token-purchase-comment/token-purchase-comment.component';
import { TokenWithdrawalCommentComponent } from './components/wallet/transaction-comments/token-withdrawal-comment/token-withdrawal-comment.component';
import { PledgeCancelledCommentComponent } from './components/wallet/transaction-comments/pledge-cancelled-comment/pledge-cancelled-comment.component';
import { PledgeEditedCommentComponent } from './components/wallet/transaction-comments/pledge-edited-comment/pledge-edited-comment.component';
import { RequestCancelledCommentComponent } from './components/wallet/transaction-comments/request-cancelled-comment/request-cancelled-comment.component';
import { RequestCompletedCommentComponent } from './components/wallet/transaction-comments/request-completed-comment/request-completed-comment.component'
import {MatButtonToggleModule} from '@angular/material/button-toggle';

@NgModule({
  declarations: [
    AppComponent,
    AddArticleComponent,
    ArticleDetailsComponent,
    ArticleListComponent,
    LoginComponent,
    RegisterComponent,
    ArticlePreviewComponent,
    AuthorPreviewComponent,
    TagPreviewComponent,
    AuthorDetailsComponent,
    TagDetailsComponent,
    AllArticlesComponent,
    TagListComponent,
    PopularTagsComponent,
    UserNavMenuComponent,
    NavbarComponent,
    FeedListComponent,
    FeedArticlesComponent,
    AboutComponent,
    HomeComponent,
    UserUnlockedComponent,
    UserPreferencesComponent,
    UnlockedArticleComponent,
    FeedManagementComponent,
    FeedDetailsComponent,
    AddFeedComponent,
    AddSectionComponent,
    SectionManagementComponent,
    SectionDetailsComponent,
    SectionArticlesComponent,
    SectionListComponent,
    SectionPageComponent,
    TokensPageComponent,
    AddTokensComponent,
    WithdrawTokensComponent,
    SectionPreviewComponent,
    AuthorCardComponent,
    AuthorListComponent,
    AllAuthorsComponentComponent,
    SectionCardComponent,
    RequestPreviewComponent,
    RequestDetailsComponent,
    RequestListComponent,
    AuthorRequestsComponent,
    AddRequestComponent,
    PledgePreviewComponent,
    PledgeDetailsComponent,
    PledgeListComponent,
    AddPledgeComponent,
    EditPledgeComponent,
    EditRequestComponent,
    MainWalletDetailsComponent,
    EscrowWalletDetailsComponent,
    TransactionsListComponent,
    ArticleOwnersComponent,
    ArticleReadCommentComponent,
    PledgeCreatedCommentComponent,
    RequestAbandonedCommentComponent,
    RequestRefusedCommentComponent,
    RequestAcceptedCommentComponent,
    TokenPurchaseCommentComponent,
    TokenWithdrawalCommentComponent,
    PledgeCancelledCommentComponent,
    PledgeEditedCommentComponent,
    RequestCancelledCommentComponent,
    RequestCompletedCommentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTabsModule,
    MatListModule,
    MatButtonModule,
    MatSlideToggleModule,
    MatCardModule,
    MatFormFieldModule,
    MatSelectModule,
    MatGridListModule,
    MatTableModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatInputModule,
    MatIconModule,
    MatButtonToggleModule
  ],
  providers: [authInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
