import { UserRolePostModel } from './../Model/user-role.model';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { ApiResponse } from './../Helper/common-model';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { BaseAPIService } from '../Helper/base-api.service';
import { UserRoleModel } from '../Model/user-role.model';

@Injectable()
export class UserRoleService {

  constructor(private readonly _baseService: BaseAPIService) { }


  GetRoleList(model: IndexModel): Observable<ApiResponse<UserRoleModel[]>> {
    let url = `${this._baseService.Constants.UserRoleList_Api}`;
    return this._baseService.post(url, model);
  }

  GetRole(id: number): Observable<ApiResponse<UserRoleModel[]>> {
    let url = `${this._baseService.Constants.UserRoleDetail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateRole(model: UserRolePostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.Constants.UserRoleAddUpdate_Api}`;
    return this._baseService.post(url, model);
  }

  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<UserRoleModel[]>> {
    let url = `${this._baseService.Constants.UserRoleChangeActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteRole(id: number): Observable<ApiResponse<UserRoleModel[]>> {
    let url = `${this._baseService.Constants.UserRoleDelete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }

  CheckRoleExist(name :string,id?: number): Observable<ApiResponse<UserRoleModel[]>> {
    let url = `${this._baseService.Constants.UserRoleDetail_Api}${name}/${id}`;
    return this._baseService.get(url);
  }

}
