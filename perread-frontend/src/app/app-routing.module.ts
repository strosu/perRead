import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddArticleComponent } from './components/article/add-article/add-article.component';
import { AllArticlesComponent } from './components/article/all-articles/all-articles.component';
import { ArticleDetailsComponent } from './components/article/article-details/article-details.component';
import { ArticleOwnersComponent } from './components/article/article-owners/article-owners.component';
import { AllAuthorsComponentComponent } from './components/author/all-authors-component/all-authors-component.component';
import { AuthorDetailsComponent } from './components/author/author-details/author-details.component';
import { AddFeedComponent } from './components/feed/add-feed/add-feed.component';
import { FeedDetailsComponent } from './components/feed/feed-details/feed-details.component';
import { FeedManagementComponent } from './components/feed/feed-management/feed-management.component';
import { HomeComponent } from './components/home/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { AddTokensComponent } from './components/payment/add-tokens/add-tokens.component';
import { TokensPageComponent } from './components/payment/tokens-page/tokens-page.component';
import { WithdrawTokensComponent } from './components/payment/withdraw-tokens/withdraw-tokens.component';
import { AddPledgeComponent } from './components/pledges/add-pledge/add-pledge.component';
import { EditPledgeComponent } from './components/pledges/edit-pledge/edit-pledge.component';
import { PledgeDetailsComponent } from './components/pledges/pledge-details/pledge-details.component';
import { RegisterComponent } from './components/register/register.component';
import { AddRequestComponent } from './components/requests/add-request/add-request.component';
import { EditRequestComponent } from './components/requests/edit-request/edit-request.component';
import { RequestDetailsComponent } from './components/requests/request-details/request-details.component';
import { AddSectionComponent } from './components/sections/add-section/add-section.component';
import { SectionDetailsComponent } from './components/sections/section-details/section-details.component';
import { SectionManagementComponent } from './components/sections/section-management/section-management.component';
import { SectionPageComponent } from './components/sections/section-page/section-page.component';
import { TagDetailsComponent } from './components/tags/tag-details/tag-details.component';
import { UserPreferencesComponent } from './components/user/user-preferences/user-preferences.component';
import { UserUnlockedComponent } from './components/user/user-unlocked/user-unlocked.component';
import { MainWalletDetailsComponent } from './components/wallet/main-wallet-details/main-wallet-details.component';

const routes: Routes = [
  { path: '', component: HomeComponent},
  { path: 'articles', component: AllArticlesComponent },
  { path: 'article/:id', component: ArticleDetailsComponent},
  { path: 'article/:id/owners', component: ArticleOwnersComponent},
  { path: 'articles/new', component: AddArticleComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'authors/:id', component: AuthorDetailsComponent},
  { path: 'authors', component: AllAuthorsComponentComponent},
  { path: 'tags/:id', component: TagDetailsComponent },
  { path: 'tags/popular', component: TagDetailsComponent },
  { path: 'preferences', component: UserPreferencesComponent },
  { path: 'unlocked', component: UserUnlockedComponent },
  { path: 'feed-management', component: FeedManagementComponent },
  { path: 'feed/new', component: AddFeedComponent },
  { path: 'feed/:id/edit', component: FeedDetailsComponent },
  { path: 'section-management', component: SectionManagementComponent },
  { path: 'sections/new', component: AddSectionComponent },
  { path: 'sections/:id', component: SectionPageComponent},
  { path: 'tokens', component: TokensPageComponent},
  { path: 'tokens/add', component: AddTokensComponent},
  { path: 'tokens/withdraw', component: WithdrawTokensComponent},
  { path: 'sections/:id/edit', component: SectionDetailsComponent},
  { path: 'requests/:authorId/new', component: AddRequestComponent},
  { path: 'requests/:id', component: RequestDetailsComponent },
  { path: 'requests/:id/edit', component: EditRequestComponent },
  { path: 'requests/:requestId/pledges/add', component: AddPledgeComponent },
  { path: 'pledges/:id', component: PledgeDetailsComponent },
  { path: 'pledges/:id/edit', component: EditPledgeComponent },
  { path: 'main-wallet', component: MainWalletDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
