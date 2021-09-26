import { ApiResponse } from './../Helper/common-model';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { BaseAPIService } from '../Helper/base-api.service';
import { mode } from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetAllDDL(model: string[]): Observable<ApiResponse<any>> {
  
    return this._baseService.post(this._baseService.API_Url.DropDown_Api, model);
  }
}