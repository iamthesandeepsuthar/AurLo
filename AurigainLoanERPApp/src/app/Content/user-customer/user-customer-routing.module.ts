import { UpdateCustomerProfileComponent } from './update-customer-profile/update-customer-profile.component';

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { CustomerDashboardComponent } from './customer-dashboard/customer-dashboard.component';
import { CustomerProfileComponent } from './customer-profile/customer-profile.component';
import { GoldLoanFreshLeadComponent } from './Leads/gold-loan-fresh-lead/gold-loan-fresh-lead.component';
import { AddEditGoldLoanFreshLeadComponent } from './Leads/gold-loan-fresh-lead/add-edit-gold-loan-fresh-lead/add-edit-gold-loan-fresh-lead.component';
import { DetailGoldLoanFreshLeadComponent } from './Leads/gold-loan-fresh-lead/detail-gold-loan-fresh-lead/detail-gold-loan-fresh-lead.component';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { AddUpdateInternalBalanceTransferGoldLoanLeadComponent } from '../user-backend-operator/balance-transfer-gold-loan-leads/add-update-internal-balance-transfer-gold-loan-lead/add-update-internal-balance-transfer-gold-loan-lead.component';
import { BalanceTransferGoldLoanLeadsComponent } from '../user-backend-operator/balance-transfer-gold-loan-leads/balance-transfer-gold-loan-leads.component';
import { DetailBalanceTransferLeadComponent } from '../user-backend-operator/balance-transfer-gold-loan-leads/detail-balance-transfer-lead/detail-balance-transfer-lead.component';
import { BalanceTransferGoldLoanLeadComponent } from './Leads/balance-transfer-gold-loan-lead/balance-transfer-gold-loan-lead.component';
import { CUAddUpdateBalanceExternalTransferGoldLoanLeadComponent } from './Leads/balance-transfer-gold-loan-lead/cuadd-update-balance-external-transfer-gold-loan-lead/cuadd-update-balance-external-transfer-gold-loan-lead.component';
import { CUDetailBalanceTransferGoldLoanLeadComponent } from './Leads/balance-transfer-gold-loan-lead/cudetail-balance-transfer-gold-loan-lead/cudetail-balance-transfer-gold-loan-lead.component';

const routes: Routes = [
  {path:'', component: CustomerDashboardComponent },
  {path:'profile/:id', component: CustomerProfileComponent , canActivate:[AuthenticationGuard]},
  {path:'gold-loan-leads', component: GoldLoanFreshLeadComponent , canActivate:[AuthenticationGuard]},
  {path:'gold-loan-leads/addupdate/:id', component: AddEditGoldLoanFreshLeadComponent , canActivate:[AuthenticationGuard]},
  {path:'gold-loan-leads/detail/:id', component: DetailGoldLoanFreshLeadComponent , canActivate:[AuthenticationGuard]},
  {path:'update-profile/:id', component: UpdateCustomerProfileComponent , canActivate:[AuthenticationGuard]},
  { path: `${Routing_Url.BT_GoldLoan_List_Url}`, component: BalanceTransferGoldLoanLeadComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.BT_GoldLoan_List_Url}/${Routing_Url.Detail_Lead_Url}/:id`, component: CUDetailBalanceTransferGoldLoanLeadComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.BT_GoldLoan_List_Url}/${Routing_Url.BT_Gold_Loan_Add_Update_External_Lead_Url}/:id`, component: CUAddUpdateBalanceExternalTransferGoldLoanLeadComponent, canActivate: [AuthenticationGuard] },

];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserCustomerRoutingModule { }
