import { Routing_Url } from './Shared/Helper/constants';
import { NgModule } from '@angular/core';
import { NoPreloading, RouterModule, Routes } from '@angular/router';
import { PageNotFoundComponent } from './Content/Common/page-not-found/page-not-found.component';
import { AuthenticationGuard } from './Shared/Helper/authentication.guard';
import { HtmlComponent } from './Content/html/html.component';
import { LoginComponent } from './Content/Common/login/login.component';
import { CustomerSignUpComponent } from './Content/Common/customer-sign-up/customer-sign-up.component';



const routes: Routes = [
  { path: '', redirectTo: 'dashboard', pathMatch: 'full'},
  { path: "dashboard", loadChildren: () => import('./Content/dashboard/dashboard.module').then(m => m.DashboardModule) },
  { path: Routing_Url.AdminModule, loadChildren: () => import('./Content/admin/admin.module').then(m => m.AdminModule) },
  { path: Routing_Url.UserCustomerModule, loadChildren: () => import('./Content/user-customer/user-customer.module').then(m => m.UserCustomerModule) },
  { path: Routing_Url.UserBackendOperatorModule, loadChildren: () => import('./Content/user-backend-operator/user-backend-operator.module').then(m => m.UserBackendOperatorModule) },

  { path: 'manish', component: HtmlComponent },
  { path: Routing_Url.LoginUrl , component: LoginComponent },
  { path: Routing_Url.CustomerSignUpUrl , component: CustomerSignUpComponent },
  { path: '**', component: PageNotFoundComponent, canActivate: [AuthenticationGuard]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes, { preloadingStrategy: NoPreloading })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
