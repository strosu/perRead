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
import {MatFormFieldModule} from '@angular/material/form-field'; 
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
import {MatSelectModule} from '@angular/material/select';
import { SectionPageComponent } from './components/sections/section-page/section-page.component';
import { TokensPageComponent } from './components/payment/tokens-page/tokens-page.component'; 
import {MatGridListModule} from '@angular/material/grid-list';
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
    EditRequestComponent
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
    MatGridListModule
  ],
  providers: [authInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
