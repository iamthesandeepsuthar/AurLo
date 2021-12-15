import { ReportingUser, UserManagerModel } from './../Model/master-model/user-manager-model.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../Helper/common-model';
import { UserReportingPersonPostModel } from '../Model/doorstep-agent-model/door-step-agent.model';

@Injectable()
export class UserManagerService {
  constructor(private readonly _baseService: BaseAPIService) { }
  GetManagerList(model: IndexModel): Observable<ApiResponse<UserManagerModel[]>> {
    let url = `${this._baseService.API_Url.Manager_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetManagerById(id: number): Observable<ApiResponse<UserManagerModel>> {
    let url = `${this._baseService.API_Url.Manager_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdateManager(model: UserManagerModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Manager_AddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<UserManagerModel[]>> {
    let url = `${this._baseService.API_Url.Manager_ActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteManager(id: number): Observable<ApiResponse<UserManagerModel[]>> {
    let url = `${this._baseService.API_Url.Manager_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
  GetReportingPersonList(): Observable<ApiResponse<ReportingUser[]>> {
    let url = `${this._baseService.API_Url.Reporting_Persons_List_Api}`;
    return this._baseService.get(url);
  }
  AssignReportingPerson(model: UserReportingPersonPostModel): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.Assign_Reporting_Person_Api}`;
    return this._baseService.post(url, model);
  }
}
