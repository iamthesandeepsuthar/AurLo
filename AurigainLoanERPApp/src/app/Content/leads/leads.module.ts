import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LeadsRoutingModule } from './leads-routing.module';
import { BalanceTransferGoldLoanLeadsComponent } from './balance-transfer-gold-loan-leads/balance-transfer-gold-loan-leads.component';
import { DetailBalanceTransferLeadComponent } from './balance-transfer-gold-loan-leads/detail-balance-transfer-lead/detail-balance-transfer-lead.component';
import { AddUpdateInternalBalanceTransferGoldLoanLeadComponent } from './balance-transfer-gold-loan-leads/add-update-internal-balance-transfer-gold-loan-lead/add-update-internal-balance-transfer-gold-loan-lead.component';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { FreshGoldLoanLeadsComponent } from './fresh-gold-loan-leads/fresh-gold-loan-leads.component';


@NgModule({
  declarations: [
    BalanceTransferGoldLoanLeadsComponent,
    DetailBalanceTransferLeadComponent,
    AddUpdateInternalBalanceTransferGoldLoanLeadComponent,
    FreshGoldLoanLeadsComponent,

  ],
  imports: [
    CommonModule,
    LeadsRoutingModule,
    SharedModule,
  ]
})
export class LeadsModule { }
