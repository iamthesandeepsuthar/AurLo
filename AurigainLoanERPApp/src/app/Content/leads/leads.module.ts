import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LeadsRoutingModule } from './leads-routing.module';
import { BalanceTransferGoldLoanLeadsComponent } from './balance-transfer-gold-loan-leads/balance-transfer-gold-loan-leads.component';
import { DetailBalanceTransferLeadComponent } from './balance-transfer-gold-loan-leads/detail-balance-transfer-lead/detail-balance-transfer-lead.component';
import { AddUpdateInternalBalanceTransferGoldLoanLeadComponent } from './balance-transfer-gold-loan-leads/add-update-internal-balance-transfer-gold-loan-lead/add-update-internal-balance-transfer-gold-loan-lead.component';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { FreshGoldLoanLeadsComponent } from './fresh-gold-loan-leads/fresh-gold-loan-leads.component';
import { DetailFreshGoldLoanLeadComponent } from './fresh-gold-loan-leads/detail-fresh-gold-loan-lead/detail-fresh-gold-loan-lead.component';
import { OtherLoanLeadsComponent } from './other-loan-leads/other-loan-leads.component';
import { DetailOtherLoanLeadComponent } from './other-loan-leads/detail-other-loan-lead/detail-other-loan-lead.component';
import { AddUpdateFreshGoldLoanLeadComponent } from './fresh-gold-loan-leads/add-update-fresh-gold-loan-lead/add-update-fresh-gold-loan-lead.component';
import { BalanceTransferReturnComponent } from './balance-transfer-return/balance-transfer-return.component';
import { HomeLoanLeadComponent } from './home-loan-lead/home-loan-lead.component';
import { VehicleLoanLeadsComponent } from './vehicle-loan-leads/vehicle-loan-leads.component';
import { DetailVehicleLoanLeadComponent } from './vehicle-loan-leads/detail-vehicle-loan-lead/detail-vehicle-loan-lead.component';
import { DetailHomeLoanLeadComponent } from './home-loan-lead/detail-home-loan-lead/detail-home-loan-lead.component';
import { AddFreshPersonalLeadComponent } from './other-loan-leads/add-fresh-personal-lead/add-fresh-personal-lead.component';
import { AddFreshVehicleLeadComponent } from './vehicle-loan-leads/add-fresh-vehicle-lead/add-fresh-vehicle-lead.component';
import { AddFreshHomeLeadComponent } from './home-loan-lead/add-fresh-home-lead/add-fresh-home-lead.component';


@NgModule({
  declarations: [
    BalanceTransferGoldLoanLeadsComponent,
    DetailBalanceTransferLeadComponent,
    AddUpdateInternalBalanceTransferGoldLoanLeadComponent,
    FreshGoldLoanLeadsComponent,
    DetailFreshGoldLoanLeadComponent,
    OtherLoanLeadsComponent,
    DetailOtherLoanLeadComponent,
    AddUpdateFreshGoldLoanLeadComponent,
    BalanceTransferReturnComponent,
    HomeLoanLeadComponent,
    VehicleLoanLeadsComponent,
    DetailVehicleLoanLeadComponent,
    DetailHomeLoanLeadComponent,
    AddFreshPersonalLeadComponent,
    AddFreshVehicleLeadComponent,
    AddFreshHomeLeadComponent,

  ],
  imports: [
    CommonModule,
    LeadsRoutingModule,
    SharedModule,
  ]
})
export class LeadsModule { }
