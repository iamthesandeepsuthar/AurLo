import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CustomersRoutingModule } from './customers-routing.module';
import { ListCustomerComponent } from './list-customer/list-customer.component';


@NgModule({
  declarations: [
    ListCustomerComponent,    
  ],
  imports: [
    CommonModule,
    CustomersRoutingModule
  ]
})
export class CustomersModule { }
