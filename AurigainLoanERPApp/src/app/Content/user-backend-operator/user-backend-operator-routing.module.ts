import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { BackendUserProfileComponent } from './backend-user-profile/backend-user-profile.component';
import { OtherLoanLeadsComponent } from './other-loan-leads/other-loan-leads.component';
import { DetailBalanceTransferLeadComponent } from './balance-transfer-gold-loan-leads/detail-balance-transfer-lead/detail-balance-transfer-lead.component';
import { BalanceTransferGoldLoanLeadsComponent } from './balance-transfer-gold-loan-leads/balance-transfer-gold-loan-leads.component';
import { DetailFreshGoldLoanLeadComponent } from './fresh-gold-loan-leads/detail-fresh-gold-loan-lead/detail-fresh-gold-loan-lead.component';
import { FreshGoldLoanLeadsComponent } from './fresh-gold-loan-leads/fresh-gold-loan-leads.component';
import { BackendOperatorDashboardComponent } from './backend-operator-dashboard/backend-operator-dashboard.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { RegisterCustomersComponent } from './register-customers/register-customers.component';
import { AddUpdateInternalBalanceTransferGoldLoanLeadComponent } from './balance-transfer-gold-loan-leads/add-update-internal-balance-transfer-gold-loan-lead/add-update-internal-balance-transfer-gold-loan-lead.component';


const routes: Routes = [
  { path: '', component: BackendOperatorDashboardComponent },
  { path: 'profile/:id', component: BackendUserProfileComponent, canActivate: [AuthenticationGuard] },
  { path: 'register-customers', component: RegisterCustomersComponent, canActivate: [AuthenticationGuard] },
  { path: 'gold-loan-fresh-leads', component: FreshGoldLoanLeadsComponent, canActivate: [AuthenticationGuard] },
  { path: 'gold-loan-fresh-leads/detail/:id', component: DetailFreshGoldLoanLeadComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.BT_GoldLoan_List_Url}`, component: BalanceTransferGoldLoanLeadsComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.BT_GoldLoan_List_Url}/${Routing_Url.Detail_Lead_Url}/:id`, component: DetailBalanceTransferLeadComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.BT_GoldLoan_List_Url}/${Routing_Url.Fresh_Lead_Add_Update_Internal_Lead_Url}/:id`, component: AddUpdateInternalBalanceTransferGoldLoanLeadComponent, canActivate: [AuthenticationGuard] },
  { path: 'other-loan-leads', component: OtherLoanLeadsComponent, canActivate: [AuthenticationGuard] }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserBackendOperatorRoutingModule { }
