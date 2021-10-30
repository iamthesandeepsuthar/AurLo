import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseAPIService } from "../../Helper/base-api.service";
import { ApiResponse, Dictionary } from '../../Helper/common-model';
import { UserViewModel } from "../../Model/doorstep-agent-model/door-step-agent.model";
import { AvailableAreaModel, UserAvailabilityViewModel, UserAvailibilityPostModel } from "../../Model/User-setting-model/user-availibility.model";
import { UserSettingPostModel } from "../../Model/User-setting-model/user-setting.model";


@Injectable()
export class UserSettingService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetUserProfile(id: number): Observable<ApiResponse<UserViewModel>> {
    var parmas = new Dictionary<any>();
    parmas.Add("id", Number(id));
    let url = `${this._baseService.API_Url.GetUserProfileApi}${id}`;
    return this._baseService.get(url, parmas);
  }

  UpdateUserProfile(model: UserSettingPostModel): Promise<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.UserUpdateProfileApi}`;
    return this._baseService.post(url, model).toPromise();
  }


  UpdateApproveStatus(id: number, status?: string): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.UserApproveStatusApi}${id}`;
    return this._baseService.get(url);
  }

  SetUserAvailibility(model: UserAvailibilityPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.SetUserAvailabilityApi}`;
    return this._baseService.post(url, model);
  }


  GetAvailableAreaForRolebyPinCode(pinCode: string, roleId: number,id:number=0): Observable<ApiResponse<AvailableAreaModel[]>> {
    let url = `${this._baseService.API_Url.GetUserAvailableAreaApi}${pinCode}/${roleId}/${id}`;
    return this._baseService.get(url);
  }


  GetUserAvailibilityList(id: number): Observable<ApiResponse<UserAvailabilityViewModel[]>> {
    let url = `${this._baseService.API_Url.GetUserAvailibiltyListApi}${id}`;
    return this._baseService.get(url);
  }
}
