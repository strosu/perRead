import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddArticleComponent } from './components/add-article/add-article.component';
import { AllArticlesComponent } from './components/all-articles/all-articles.component';
import { ArticleDetailsComponent } from './components/article-details/article-details.component';
import { AuthorDetailsComponent } from './components/author-details/author-details.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { TagDetailsComponent } from './components/tag-details/tag-details.component';

const routes: Routes = [
  { path: '', redirectTo: 'articles', pathMatch: 'full' },
  { path: 'articles', component: AllArticlesComponent },
  { path: 'article/:id', component: ArticleDetailsComponent},
  { path: 'articles/new', component: AddArticleComponent},
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent},
  { path: 'authors/:id', component: AuthorDetailsComponent},
  { path: 'tags/:id', component: TagDetailsComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
