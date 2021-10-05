import { PaymentModeModel } from '../../Model/master-model/payment-mode.model';
import { Injectable } from '@angular/core';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';
import { Observable } from 'rxjs';

@Injectable()
export class PaymentModeService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetPaymentModeList(model: IndexModel): Observable<ApiResponse<PaymentModeModel[]>> {
    let url = `${this._baseService.API_Url.PaymentMode_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetPaymentMode(id: number): Observable<ApiResponse<PaymentModeModel>> {
    let url = `${this._baseService.API_Url.PaymentMode_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdatePaymentMode(model: PaymentModeModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.PaymentMode_Add_Update_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<PaymentModeModel[]>> {
    let url = `${this._baseService.API_Url.PaymentMode_Active_Status_Api}${id}`;
    return this._baseService.get(url);
  }
  DeletePaymentMode(id: number): Observable<ApiResponse<PaymentModeModel[]>> {
    let url = `${this._baseService.API_Url.PaymentMode_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }

  // CheckRoleExist(name :string,id?: number): Observable<ApiResponse<PaymentModeModel[]>> {
  //   let url = `${this._baseService.API_Url.UserRoleCheckRoleExist_Api}${name}/${id}`;
  //   return this._baseService.get(url);
  // }
}
