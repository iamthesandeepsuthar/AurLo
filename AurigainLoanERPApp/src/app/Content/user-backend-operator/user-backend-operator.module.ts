import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserBackendOperatorRoutingModule } from './user-backend-operator-routing.module';
import { BackendOperatorDashboardComponent } from './backend-operator-dashboard/backend-operator-dashboard.component';
import { FreshGoldLoanLeadsComponent } from './fresh-gold-loan-leads/fresh-gold-loan-leads.component';
import { DetailFreshGoldLoanLeadComponent } from './fresh-gold-loan-leads/detail-fresh-gold-loan-lead/detail-fresh-gold-loan-lead.component';
import { BalanceTransferGoldLoanLeadsComponent } from './balance-transfer-gold-loan-leads/balance-transfer-gold-loan-leads.component';
import { DetailBalanceTransferLeadComponent } from './balance-transfer-gold-loan-leads/detail-balance-transfer-lead/detail-balance-transfer-lead.component';
import { OtherLoanLeadsComponent } from './other-loan-leads/other-loan-leads.component';
import { DetailOtherLoanLeadComponent } from './other-loan-leads/detail-other-loan-lead/detail-other-loan-lead.component';


@NgModule({
  declarations: [
    BackendOperatorDashboardComponent,
    FreshGoldLoanLeadsComponent,
    DetailFreshGoldLoanLeadComponent,
    BalanceTransferGoldLoanLeadsComponent,
    DetailBalanceTransferLeadComponent,
    OtherLoanLeadsComponent,
    DetailOtherLoanLeadComponent
  ],
  imports: [
    CommonModule,
    UserBackendOperatorRoutingModule
  ]
})
export class UserBackendOperatorModule { }
