import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ListCustomerComponent } from './list-customer/list-customer.component';

const routes: Routes = [
  {
    path: `${Routing_Url.Customer_List_Url}`, component: ListCustomerComponent, canActivate: [AuthenticationGuard]
  },  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomersRoutingModule { }
