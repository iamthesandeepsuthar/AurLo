import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse } from '../../Helper/common-model';
import { UserSettingPostModel } from '../../Model/User-setting-model/user-setting.model';

@Injectable( )
export class UserSettingService {

  constructor(private readonly _baseService: BaseAPIService) { }

  UpdateUserProfile(model: UserSettingPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.UserUpdateProfileApi}`;
    return this._baseService.post(url, model);
  } 
}