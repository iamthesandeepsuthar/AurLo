import { JewellaryTypeModel, DDLJewellaryType } from './../../Model/master-model/jewellary-type-model.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';

@Injectable()
export class JewelleryTypeService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetJewelleryTypeList(model: IndexModel): Observable<ApiResponse<JewellaryTypeModel[]>> {
    let url = `${this._baseService.API_Url.Jewellery_List_Api}`;
    return this._baseService.post(url, model);
  }
  JewelleryTypeById(id: number): Observable<ApiResponse<JewellaryTypeModel>> {
    let url = `${this._baseService.API_Url.Jewellery_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdateJewelleryType(model: JewellaryTypeModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Jewellery_AddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<JewellaryTypeModel[]>> {
    let url = `${this._baseService.API_Url.Jewellery_ActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteJewelleryType(id: number): Observable<ApiResponse<JewellaryTypeModel[]>> {
    let url = `${this._baseService.API_Url.Jewellery_Delete_Api}${id}`;
    return this._baseService.Delete(url);
  }
  GetDDLJewelleryType(): Observable<ApiResponse<DDLJewellaryType[]>> {
    let url = `${this._baseService.API_Url.Jewellery_Dropdown_List_Api}`;
    return this._baseService.get(url);
  }
}
