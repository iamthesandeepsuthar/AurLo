import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomersRoutingModule } from './customers-routing.module';
import { ListCustomerComponent } from './list-customer/list-customer.component';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';


@NgModule({
  declarations: [
    ListCustomerComponent,
  ],
  imports: [
    CommonModule,
    CustomersRoutingModule,
    SharedModule,
  ]
})
export class CustomersModule { }
