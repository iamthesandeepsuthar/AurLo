import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';
import { FreshLeadHLPLCLModel } from '../../Model/Leads/other-loan-leads.model';

@Injectable()
export class PersonalHomeCarLoanService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetList(model: IndexModel): Observable<ApiResponse<FreshLeadHLPLCLModel[]>> {
    let url = `${this._baseService.API_Url.Personal_Home_Car_Loan_List_Api}`;
    return this._baseService.post(url, model);
  }
}
