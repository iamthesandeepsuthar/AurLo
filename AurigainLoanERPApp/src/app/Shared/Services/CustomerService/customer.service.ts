import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse } from '../../Helper/common-model';
import { CustomerRegistrationModel } from '../../Model/CustomerModel/customer-registration-model.model';

@Injectable()
export class CustomerService {

  constructor(private readonly _baseService: BaseAPIService) { }
  
  CustomerRegistration(model: CustomerRegistrationModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Customer_Registration_Api}`;
    return this._baseService.post(url, model);
  }
}
