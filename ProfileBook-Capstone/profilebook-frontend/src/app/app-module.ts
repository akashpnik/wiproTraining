/*import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';

@NgModule({
  declarations: [
    App
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    
  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
*/

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideHttpClient } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing-module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { PostsComponent } from './components/posts/posts.component';
import { CreatePostComponent } from './components/create-post/create-post.component';
import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
//import { AppComponent } from './app';

import { AuthService } from './services/auth.service';
import { PostService } from './services/post.service';
import { AdminService } from './services/admin.service';
import { LikeService } from './services/like.service';
import { CommentComponent } from './components/comment/comment.component';
import { CommentService } from './services/comment.service';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { UserProfileService } from './services/user-profile.service';
//import { RegisterComponent } from './components/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    DashboardComponent,
    PostsComponent,
    CreatePostComponent,
    AdminDashboardComponent,
    CommentComponent,
    UserProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,           // For template-driven forms
    ReactiveFormsModule    // For reactive forms
  ],
  providers: [
    provideHttpClient() ,
    AuthService,
    PostService,
    AdminService,
    LikeService,
    CommentService,
    UserProfileService//   // New Angular 20 way instead of HttpClientModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
