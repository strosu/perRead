import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { FormsModule } from '@angular/forms';
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
    PopularTagsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [authInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
