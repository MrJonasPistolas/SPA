import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {
  AppLayoutComponent,
  HomeComponent,
  LoginComponent
} from './components';
import { AfterLoginGuard } from './guards';

const routes: Routes = [
  // Basic Routes
  {
    path: '',
    component: AppLayoutComponent,
    children: [
      { path: '', component: HomeComponent },
    ],
    canActivate: [AfterLoginGuard]
  },
  // login route
  { path: 'login', component: LoginComponent },
  // redirect to home
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
