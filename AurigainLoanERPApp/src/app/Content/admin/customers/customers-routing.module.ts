import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { DetailCustomerComponent } from './list-customer/detail-customer/detail-customer.component';
import { ListCustomerComponent } from './list-customer/list-customer.component';

const routes: Routes = [
  {
    path: `${Routing_Url.Customer_List_Url}`, component: ListCustomerComponent, canActivate: [AuthenticationGuard],
  },
  {
    path: `${Routing_Url.Customer_Detail_Url}/:id`, component: DetailCustomerComponent, canActivate: [AuthenticationGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CustomersRoutingModule { }
