import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../../Helper/common-model';
import { ddlPurposeModel, PurposeModel } from '../../Model/master-model/purpose-model.model';


@Injectable()
export class PurposeService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetPurposeList(model: IndexModel): Observable<ApiResponse<PurposeModel[]>> {
    let url = `${this._baseService.API_Url.Purpose_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetPurpose(id: number): Observable<ApiResponse<PurposeModel>> {
    let url = `${this._baseService.API_Url.Purpose_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdatePurpose(model: PurposeModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Purpose_Add_Update_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<PurposeModel[]>> {
    let url = `${this._baseService.API_Url.Purpose_Active_Status_Api}${id}`;
    return this._baseService.get(url);
  }
  DeletePurpose(id: number): Observable<ApiResponse<PurposeModel[]>> {
    let url = `${this._baseService.API_Url.Purpose_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
  GetDDLPurpose(): Observable<ApiResponse<ddlPurposeModel[]>> {
    let url = `${this._baseService.API_Url.Purpose_Dropdown_Api}`;
    return this._baseService.get(url);
  }
}
