import { CustomerListModel } from './../../Model/CustomerModel/customer-registration-model.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';
import { CustomerRegistrationModel, CustomerRegistrationViewModel } from '../../Model/CustomerModel/customer-registration-model.model';

@Injectable()
export class CustomerService {

  constructor(private readonly _baseService: BaseAPIService) { }

  CustomerRegistration(model: CustomerRegistrationModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Customer_Registration_Api}`;
    return this._baseService.post(url, model);
  }
  GetCustomerProfile(userId: number): Observable<ApiResponse<CustomerRegistrationViewModel>>
  {
    let url = `${this._baseService.API_Url.Customer_Profile_Detail_Api}${userId}`;
    return this._baseService.get(url);
  }
  GetCustomerGetById(id: number): Observable<ApiResponse<CustomerRegistrationModel>>{
    let url = `${this._baseService.API_Url.Customer_Get_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  GetCustomerList(model: IndexModel): Observable<ApiResponse<CustomerListModel[]>> {
    let url = `${this._baseService.API_Url.Customer_List_Api}`;
    return this._baseService.post(url, model);
  }
}
