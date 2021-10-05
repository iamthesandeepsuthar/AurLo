import { QualificationModel } from './../../Model/master-model/qualification.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../../Helper/common-model';



@Injectable()
export class QualificationService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetQualificationList(model: IndexModel): Observable<ApiResponse<QualificationModel[]>> {
    let url = `${this._baseService.API_Url.Qualification_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetQualification(id: number): Observable<ApiResponse<QualificationModel>> {
    let url = `${this._baseService.API_Url.Qualification_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdateQualification(model: QualificationModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Qualification_Add_Update_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<QualificationModel[]>> {
    let url = `${this._baseService.API_Url.Qualification_Active_Status_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteQualification(id: number): Observable<ApiResponse<QualificationModel[]>> {
    let url = `${this._baseService.API_Url.Qualification_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
}
