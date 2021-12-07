import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { AddUpdateInternalBalanceTransferGoldLoanLeadComponent } from './balance-transfer-gold-loan-leads/add-update-internal-balance-transfer-gold-loan-lead/add-update-internal-balance-transfer-gold-loan-lead.component';
import { BalanceTransferGoldLoanLeadsComponent } from './balance-transfer-gold-loan-leads/balance-transfer-gold-loan-leads.component';
import { DetailBalanceTransferLeadComponent } from './balance-transfer-gold-loan-leads/detail-balance-transfer-lead/detail-balance-transfer-lead.component';

const routes: Routes = [
   { path: '', redirectTo: `${Routing_Url.BT_GoldLoan_List_Url}`, pathMatch: 'full' },
  { path: `${Routing_Url.BT_GoldLoan_List_Url}`, component: BalanceTransferGoldLoanLeadsComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.BT_GoldLoan_List_Url}/${Routing_Url.Fresh_Lead_Detail_Url}/:id`, component: DetailBalanceTransferLeadComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.BT_GoldLoan_List_Url}/${Routing_Url.Fresh_Lead_Add_Update_Internal_Lead_Url}/:id`, component: AddUpdateInternalBalanceTransferGoldLoanLeadComponent, canActivate: [AuthenticationGuard] },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeadsRoutingModule { }
