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


@NgModule({
  declarations: [
    CustomerDashboardComponent,
    CustomerProfileComponent,
    GoldLoanFreshLeadComponent,
    AddEditGoldLoanFreshLeadComponent,
    DetailGoldLoanFreshLeadComponent,
    UpdateCustomerProfileComponent
  ],
  imports: [
    CommonModule,
    UserCustomerRoutingModule,
    SharedModule
  ]
})
export class UserCustomerModule { }
