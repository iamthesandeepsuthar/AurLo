import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {  UserCustomerRoutingModule } from './user-customer-routing.module';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { CustomerDashboardComponent } from './customer-dashboard/customer-dashboard.component';
import { CustomerProfileComponent } from './customer-profile/customer-profile.component';


@NgModule({
  declarations: [
    CustomerDashboardComponent,
    CustomerProfileComponent
  ],
  imports: [
    CommonModule,
    UserCustomerRoutingModule,
    SharedModule
  ]
})
export class UserCustomerModule { }
