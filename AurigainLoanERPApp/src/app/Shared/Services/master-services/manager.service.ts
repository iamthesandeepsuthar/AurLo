import { UserManagerModel } from './../../Model/master-model/user-manager-model.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';

@Injectable()
export class ManagerService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetUserManagerList(model: IndexModel): Observable<ApiResponse<UserManagerModel[]>> {
    let url = `${this._baseService.API_Url.Manager_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetUserManagerById(id: number): Observable<ApiResponse<UserManagerModel>> {
    let url = `${this._baseService.API_Url.Manager_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdateUserManager(model: UserManagerModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Manager_AddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<UserManagerModel[]>> {
    let url = `${this._baseService.API_Url.Manager_ActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteUserManager(id: number): Observable<ApiResponse<UserManagerModel[]>> {
    let url = `${this._baseService.API_Url.Manager_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
}
