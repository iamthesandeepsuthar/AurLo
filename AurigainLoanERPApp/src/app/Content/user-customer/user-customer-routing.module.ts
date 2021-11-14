
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { CustomerDashboardComponent } from './customer-dashboard/customer-dashboard.component';
import { CustomerProfileComponent } from './customer-profile/customer-profile.component';
import { GoldLoanFreshLeadComponent } from './Leads/gold-loan-fresh-lead/gold-loan-fresh-lead.component';
import { AddEditGoldLoanFreshLeadComponent } from './Leads/gold-loan-fresh-lead/add-edit-gold-loan-fresh-lead/add-edit-gold-loan-fresh-lead.component';
import { DetailGoldLoanFreshLeadComponent } from './Leads/gold-loan-fresh-lead/detail-gold-loan-fresh-lead/detail-gold-loan-fresh-lead.component';

const routes: Routes = [
  {path:'', component: CustomerDashboardComponent },
  {path:'profile/', component: CustomerProfileComponent , canActivate:[AuthenticationGuard]},
  {path:'gold-loan-lead/', component: GoldLoanFreshLeadComponent , canActivate:[AuthenticationGuard]},
  {path:'gold-loan-lead/addupdate/:id', component: AddEditGoldLoanFreshLeadComponent , canActivate:[AuthenticationGuard]},
  {path:'gold-loan-lead/detail/:id', component: DetailGoldLoanFreshLeadComponent , canActivate:[AuthenticationGuard]},

];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserCustomerRoutingModule { }
