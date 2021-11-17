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


const routes: Routes = [
  {path:'', component: BackendOperatorDashboardComponent },
  {path:'register-customers', component: RegisterCustomersComponent, canActivate:[AuthenticationGuard]},
  {path: 'gold-loan-fresh-leads', component: FreshGoldLoanLeadsComponent, canActivate:[AuthenticationGuard]},
  {path: 'gold-loan-fresh-leads/detail/:id', component:DetailFreshGoldLoanLeadComponent, canActivate:[AuthenticationGuard]},
  {path:'balance-transfer-leads', component:BalanceTransferGoldLoanLeadsComponent, canActivate:[AuthenticationGuard]},
  {path:'balance-transfer-leads/detail/:id' , component: DetailBalanceTransferLeadComponent, canActivate:[AuthenticationGuard]},
  {path: 'other-loan-leads', component: OtherLoanLeadsComponent , canActivate:[AuthenticationGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserBackendOperatorRoutingModule { }
