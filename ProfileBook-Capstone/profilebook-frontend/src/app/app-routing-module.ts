import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

//----------------------------------------------

import { AdminDashboardComponent } from './components/admin-dashboard/admin-dashboard.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component'; // ✅ Add this
//import { AuthGuard } from './guards/auth.guard';
import { AdminGuard } from './guards/admin.guard';
//import { RegisterComponent } from './components/register/register.component';


//-------------------------------------------

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent},
  { path: 'admin', component: AdminDashboardComponent, canActivate: [AdminGuard] },
 // { path: 'profile/:id', component: UserProfileComponent, canActivate: [AuthGuard] },

  //{ path: 'dashboard', redirectTo: '/', pathMatch: 'full' }, // Temporary redirect
  //{ path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
