import { BankModel, DDLBankModel, DDLBranchModel } from './../../Model/master-model/bank-model.model';
import { Injectable } from '@angular/core';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BankBranchService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetBankList(model: IndexModel): Observable<ApiResponse<BankModel[]>> {
    let url = `${this._baseService.API_Url.Bank_List_Api}`;
    return this._baseService.post(url, model);
  }
  BankById(id: number): Observable<ApiResponse<BankModel>> {
    let url = `${this._baseService.API_Url.Bank_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdateBank(model: BankModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Bank_AddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id: number, status?: string): Observable<ApiResponse<BankModel[]>> {
    let url = `${this._baseService.API_Url.Bank_ActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteBank(id: number): Observable<ApiResponse<BankModel[]>> {
    let url = `${this._baseService.API_Url.Bank_Delete_Api}${id}`;
    return this._baseService.Delete(url);
  }
  GetDDLBanks(): Observable<ApiResponse<DDLBankModel[]>> {
    let url = `${this._baseService.API_Url.Bank_Dropdown_List_Api}`;
    return this._baseService.get(url);
  }


  GetBranchesbyPinCode(pinCode: string) : Observable<ApiResponse<DDLBranchModel[]>> {
    let url = `${this._baseService.API_Url.Branches_by_PinCode_Api}${pinCode}`;
    return this._baseService.get(url);
  }

  GetBranchesbyBankId(id: string) : Observable<ApiResponse<DDLBranchModel[]>> {
    let url = `${this._baseService.API_Url.Branch_Dropdown_List_Api}/${id}`;
    return this._baseService.get(url);
  }



}
