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
import { UserNavMenuComponent } from './components/user/user-nav-menu/user-nav-menu.component';
import { NavbarComponent } from './components/nav/navbar/navbar.component';
import { FeedListComponent } from './components/feed/feed-list/feed-list.component';
import { FeedDetailsComponent } from './components/feed/feed-details/feed-details.component';
import { AboutComponent } from './components/home/about/about.component';
import { HomeComponent } from './components/home/home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTabsModule } from '@angular/material/tabs';
import { MatListModule} from '@angular/material/list'; 
import { UserUnlockedComponent } from './components/user/user-unlocked/user-unlocked.component';
import { UserPreferencesComponent } from './components/user/user-preferences/user-preferences.component';
import {MatButtonModule} from '@angular/material/button';
import { UnlockedArticleComponent } from './components/user/unlocked-article/unlocked-article.component'; 

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
    FeedDetailsComponent,
    AboutComponent,
    HomeComponent,
    UserUnlockedComponent,
    UserPreferencesComponent,
    UnlockedArticleComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTabsModule,
    MatListModule,
    MatButtonModule
  ],
  providers: [authInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
