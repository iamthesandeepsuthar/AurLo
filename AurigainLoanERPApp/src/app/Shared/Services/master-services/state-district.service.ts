import { DDLDistrictModel, DistrictModel } from '../../Model/master-model/district.model';
import { DDLStateModel } from './../../Model/master-model/state.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';
import { StateModel } from '../../Model/master-model/state.model';


@Injectable()
export class StateDistrictService {

  constructor(private readonly _baseService: BaseAPIService) { }

  //#region  << State>>
  GetStateList(model: IndexModel): Observable<ApiResponse<StateModel[]>> {
    let url = `${this._baseService.API_Url.State_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetState(id: number): Observable<ApiResponse<StateModel>> {
    let url = `${this._baseService.API_Url.State_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  GetDDLState(): Observable<ApiResponse<DDLStateModel[]>> {
    let url = `${this._baseService.API_Url.State_Dropdown_Api}`;
    return this._baseService.get(url);
  }
  AddUpdateState(model: StateModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.State_Add_Update_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id: number, status?: string): Observable<ApiResponse<StateModel[]>> {
    let url = `${this._baseService.API_Url.State_Active_Status_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteState(id: number): Observable<ApiResponse<StateModel[]>> {
    let url = `${this._baseService.API_Url.State_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }


  //#endregion


  //#region  <<District>>
  GetDistrictList(model: IndexModel): Observable<ApiResponse<DistrictModel[]>> {
    let url = `${this._baseService.API_Url.District_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetDistrict(id: number): Observable<ApiResponse<DistrictModel>> {
    let url = `${this._baseService.API_Url.District_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  GetDDLDistrict(id: number): Observable<ApiResponse<DDLDistrictModel[]>> {
    let url = `${this._baseService.API_Url.District_Dropdown_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdateDistrict(model: DistrictModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.District_Add_Update_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeDistrictActiveStatus(id: number, status?: string): Observable<ApiResponse<DistrictModel[]>> {
    let url = `${this._baseService.API_Url.District_Active_Status_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteDistrict(id: number): Observable<ApiResponse<DistrictModel[]>> {
    let url = `${this._baseService.API_Url.District_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
  //#endregion
}
