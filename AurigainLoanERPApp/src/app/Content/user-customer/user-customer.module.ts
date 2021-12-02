import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {  UserCustomerRoutingModule } from './user-customer-routing.module';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { CustomerDashboardComponent } from './customer-dashboard/customer-dashboard.component';
import { CustomerProfileComponent } from './customer-profile/customer-profile.component';
import { GoldLoanFreshLeadComponent } from './Leads/gold-loan-fresh-lead/gold-loan-fresh-lead.component';
import { AddEditGoldLoanFreshLeadComponent } from './Leads/gold-loan-fresh-lead/add-edit-gold-loan-fresh-lead/add-edit-gold-loan-fresh-lead.component';
import { DetailGoldLoanFreshLeadComponent } from './Leads/gold-loan-fresh-lead/detail-gold-loan-fresh-lead/detail-gold-loan-fresh-lead.component';
import { UpdateCustomerProfileComponent } from './update-customer-profile/update-customer-profile.component';
import { BalanceTransferGoldLoanLeadComponent } from './Leads/balance-transfer-gold-loan-lead/balance-transfer-gold-loan-lead.component';
import { CUDetailBalanceTransferGoldLoanLeadComponent } from './Leads/balance-transfer-gold-loan-lead/cudetail-balance-transfer-gold-loan-lead/cudetail-balance-transfer-gold-loan-lead.component';
import { CUAddUpdateBalanceExternalTransferGoldLoanLeadComponent } from './Leads/balance-transfer-gold-loan-lead/cuadd-update-balance-external-transfer-gold-loan-lead/cuadd-update-balance-external-transfer-gold-loan-lead.component';


@NgModule({
  declarations: [
    CustomerDashboardComponent,
    CustomerProfileComponent,
    GoldLoanFreshLeadComponent,
    AddEditGoldLoanFreshLeadComponent,
    DetailGoldLoanFreshLeadComponent,
    UpdateCustomerProfileComponent,
    BalanceTransferGoldLoanLeadComponent,
    CUDetailBalanceTransferGoldLoanLeadComponent,
    CUAddUpdateBalanceExternalTransferGoldLoanLeadComponent
  ],
  imports: [
    CommonModule,
    UserCustomerRoutingModule,
    SharedModule
  ]
})
export class UserCustomerModule { }
