import { ApiResponse, IndexModel } from 'src/app/Shared/Helper/common-model';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { BaseAPIService } from '../../Helper/base-api.service';
import { DDLUserRole, UserRoleModel, UserRolePostModel } from '../../Model/master-model/user-role.model';

@Injectable()
export class UserRoleService {

  constructor(private readonly _baseService: BaseAPIService) { }


  GetRoleList(model: IndexModel): Observable<ApiResponse<UserRoleModel[]>> {
    let url = `${this._baseService.API_Url.UserRoleList_Api}`;
    return this._baseService.post(url, model);
  }

  GetUserRoleDDL():Observable<ApiResponse<DDLUserRole[]>> {
    let url = `${this._baseService.API_Url.UserRole_Dropdown_Api}`;
    return this._baseService.get(url);
  }
  GetRole(id: number): Observable<ApiResponse<UserRoleModel>> {
    let url = `${this._baseService.API_Url.UserRoleDetail_Api}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateRole(model: UserRolePostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.UserRoleAddUpdate_Api}`;
    return this._baseService.post(url, model);
  }

  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<UserRoleModel[]>> {
    let url = `${this._baseService.API_Url.UserRoleChangeActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteRole(id: number): Observable<ApiResponse<UserRoleModel[]>> {
    let url = `${this._baseService.API_Url.UserRoleDelete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }

  CheckRoleExist(name :string,id?: number): Observable<ApiResponse<UserRoleModel[]>> {
    let url = `${this._baseService.API_Url.UserRoleCheckRoleExist_Api}${name}/${id}`;
    return this._baseService.get(url);
  }

}
