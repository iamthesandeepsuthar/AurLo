
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Subject, Observable } from 'rxjs';
import { LoginPostModel, LoginResponseModel } from '../Model/User-setting-model/user-setting.model';
import { BaseAPIService } from "./base-api.service";
import { SecurityService } from "./security.service";
import { ApiResponse } from 'src/app/Shared/Helper/common-model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public IsAuthentication = new Subject<boolean>();

  constructor(private readonly _baseService: BaseAPIService, private _router: Router, private readonly _securityService: SecurityService) {

    this.IsAuthenticate();
  }

  IsAccessibleUrl(requestedUrl: string): boolean {
    return true;
  }

  Login(model: LoginPostModel): Observable<ApiResponse<LoginResponseModel>> {
    let url = `${this._baseService.API_Url.Login_Api}`;
    return this._baseService.post(url, model);
  }

  SaveUserToken(token: string) {
    this._securityService.setStorage('authToken', token)
    this._securityService.setStorage('sessionTime', String(new Date().setHours(24)));
    this.IsAuthentication.next(true);

  }

  SaveUserDetail(model: LoginResponseModel) {
    this._securityService.setStorage('userDetail', JSON.stringify(model));

  }

  GetUserDetail() : LoginResponseModel|null {
    let data = this._securityService.getStorage('userDetail');
    if (data) {
      return JSON.parse(data!) as LoginResponseModel;
    }
    else {
      return null;
    }
  }


  IsAuthenticate() {
    setTimeout(() => {
      let token = this._securityService.getStorage('authToken');
      let sessionTime = this._securityService.getStorage('sessionTime');
      let currentSessionTime = Number(new Date().getTime());
      if (token != null && Number(sessionTime) > currentSessionTime) {
        this.IsAuthentication.next(true);
      } else {

        this.LogOut();
      }
    }, 5);
  }

  LogOut() {
    this.IsAuthentication.next(false);
    this._securityService.removeStorage('authToken');
    this._securityService.removeStorage('sessionTime');
    setTimeout(() => {
      if (this._router.url !== this._baseService.Routing_Url.LoginUrl) {
        this._router.navigate([this._baseService.Routing_Url.LoginUrl]);
      }
    }, 10);
  }
}
