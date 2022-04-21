import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddArticleComponent } from './components/article/add-article/add-article.component';
import { AllArticlesComponent } from './components/article/all-articles/all-articles.component';
import { ArticleDetailsComponent } from './components/article/article-details/article-details.component';
import { AuthorDetailsComponent } from './components/author/author-details/author-details.component';
import { AddFeedComponent } from './components/feed/add-feed/add-feed.component';
import { FeedDetailsComponent } from './components/feed/feed-details/feed-details.component';
import { FeedManagementComponent } from './components/feed/feed-management/feed-management.component';
import { HomeComponent } from './components/home/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { TagDetailsComponent } from './components/tags/tag-details/tag-details.component';
import { UserPreferencesComponent } from './components/user/user-preferences/user-preferences.component';
import { UserUnlockedComponent } from './components/user/user-unlocked/user-unlocked.component';

const routes: Routes = [
  // { path: '', redirectTo: 'articles', pathMatch: 'full' },
  { path: '', component: HomeComponent},
  { path: 'articles', component: AllArticlesComponent },
  { path: 'article/:id', component: ArticleDetailsComponent},
  { path: 'articles/new', component: AddArticleComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'authors/:id', component: AuthorDetailsComponent},
  // { path: 'authors/all', component: AuthorDetailsComponent},
  // { path: 'authors/', component: AuthorDetailsComponent},
  { path: 'tags/:id', component: TagDetailsComponent },
  { path: 'tags/popular', component: TagDetailsComponent },
  { path: 'preferences', component: UserPreferencesComponent },
  { path: 'unlocked', component: UserUnlockedComponent },
  { path: 'feed-management', component: FeedManagementComponent },
  { path: 'feed/new', component: AddFeedComponent },
  { path: 'feed/:id/edit', component: FeedDetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
