import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import {
  AppLayoutComponent,
  HomeComponent,
  IncomeCategoriesComponent,
  LoginComponent
} from './components';
import { Role } from './enums';
import { AfterLoginGuard, RoleGuard } from './guards';

const routes: Routes = [
  // Basic Routes
  {
    path: '',
    component: AppLayoutComponent,
    children: [
      {
        path: '',
        component: HomeComponent
      },
      {
        path: 'admin/category/incomes',
        component: IncomeCategoriesComponent,
        canActivate: [RoleGuard],
        data: { roles: [Role.Administrator] }
      },
    ],
    canActivate: [AfterLoginGuard]
  },
  // login route
  { path: 'login', component: LoginComponent },
  // redirect to home
  { path: '**', redirectTo: '' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes), CommonModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
