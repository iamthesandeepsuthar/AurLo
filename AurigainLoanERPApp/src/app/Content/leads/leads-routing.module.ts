import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { AddUpdateInternalBalanceTransferGoldLoanLeadComponent } from './balance-transfer-gold-loan-leads/add-update-internal-balance-transfer-gold-loan-lead/add-update-internal-balance-transfer-gold-loan-lead.component';
import { BalanceTransferGoldLoanLeadsComponent } from './balance-transfer-gold-loan-leads/balance-transfer-gold-loan-leads.component';
import { DetailBalanceTransferLeadComponent } from './balance-transfer-gold-loan-leads/detail-balance-transfer-lead/detail-balance-transfer-lead.component';
import { FreshGoldLoanLeadsComponent } from './fresh-gold-loan-leads/fresh-gold-loan-leads.component';
import { DetailFreshGoldLoanLeadComponent } from './fresh-gold-loan-leads/detail-fresh-gold-loan-lead/detail-fresh-gold-loan-lead.component';
import { DetailOtherLoanLeadComponent } from './other-loan-leads/detail-other-loan-lead/detail-other-loan-lead.component';
import { OtherLoanLeadsComponent } from './other-loan-leads/other-loan-leads.component';
import { AddUpdateFreshGoldLoanLeadComponent } from './fresh-gold-loan-leads/add-update-fresh-gold-loan-lead/add-update-fresh-gold-loan-lead.component';

const routes: Routes = [
   { path: '', redirectTo: `${Routing_Url.BT_GoldLoan_List_Url}`, pathMatch: 'full' },

  { path: `${Routing_Url.BT_GoldLoan_List_Url}`, component: BalanceTransferGoldLoanLeadsComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.BT_GoldLoan_List_Url}/${Routing_Url.Detail_Lead_Url}/:id`, component: DetailBalanceTransferLeadComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.BT_GoldLoan_List_Url}/${Routing_Url.Add_Update_Internal_Lead_Url}/:id`, component: AddUpdateInternalBalanceTransferGoldLoanLeadComponent, canActivate: [AuthenticationGuard] },

  { path: `${Routing_Url.Fresh_Lead_List_Url}`, component: FreshGoldLoanLeadsComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.Fresh_Lead_List_Url}/${Routing_Url.Detail_Lead_Url}/:id`, component: DetailFreshGoldLoanLeadComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.Fresh_Lead_List_Url}/${Routing_Url.Add_Update_Lead_Url}/:id`, component: AddUpdateFreshGoldLoanLeadComponent, canActivate: [AuthenticationGuard] },

  { path: `${Routing_Url.Other_Loan_Leads_Url}`, component: OtherLoanLeadsComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.Other_Loan_Leads_Url}/${Routing_Url.Detail_Lead_Url}/:id`, component: DetailOtherLoanLeadComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.Other_Loan_Leads_Url}/${Routing_Url.Add_Update_Lead_Url}/:id`, component: AddUpdateInternalBalanceTransferGoldLoanLeadComponent, canActivate: [AuthenticationGuard] },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeadsRoutingModule { }
