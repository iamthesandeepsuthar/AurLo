import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseAPIService } from "../../Helper/base-api.service";
import { ApiResponse } from "../../Helper/common-model";
import { AvailableAreaModel, UserAvailibilityPostModel } from "../../Model/User-setting-model/user-availibility.model";
import { UserSettingPostModel } from "../../Model/User-setting-model/user-setting.model";


@Injectable( )
export class UserSettingService {

  constructor(private readonly _baseService: BaseAPIService) { }

  UpdateUserProfile(model: UserSettingPostModel): Promise<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.UserUpdateProfileApi}`;
    return this._baseService.post(url, model).toPromise();
  }


  UpdateApproveStatus(id: number, status?: string): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.UserApproveStatusApi}${id}`;
    return this._baseService.get(url);
  }

  SetUserAvailibility(model: UserAvailibilityPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.UserAvailabilityApi}`;
    return this._baseService.post(url, model);
  }


  GetAvailableAreaForRolebyPinCode(pinCode: string, roleId: number): Observable<ApiResponse<AvailableAreaModel[]>> {
    let url = `${this._baseService.API_Url.UserAvailableAreaApi}${pinCode}/${roleId}`;
    return this._baseService.get(url);
  }
}
