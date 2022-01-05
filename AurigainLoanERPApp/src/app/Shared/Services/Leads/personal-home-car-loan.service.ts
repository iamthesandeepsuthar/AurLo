import { OtherLoanLeads } from './../../Model/Leads/other-loan-leads.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';
import { LeadStatusActionHistory, LeadStatusModel } from '../../Model/Leads/lead-status-model.model';
import { FreshLeadHLPLCLModel } from '../../Model/Leads/other-loan-leads.model';

@Injectable()
export class PersonalHomeCarLoanService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetList(model: IndexModel): Observable<ApiResponse<FreshLeadHLPLCLModel[]>> {
    let url = `${this._baseService.API_Url.Personal_Home_Car_Loan_List_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id: number, status?: string): Observable<ApiResponse<number>> {
    let url = `${this._baseService.API_Url.Gold_Loan_Fresh_Lead__ActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  Delete(id: number): Observable<ApiResponse<number>> {
    let url = `${this._baseService.API_Url.Gold_Loan_Fresh_Lead__Delete_Api}${id}`;
    return this._baseService.Delete(url);
  }
  LeadStatus(model: LeadStatusModel) : Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.Personal_Home_Car_Loan_Lead_Status_Change_Api}`;
    return this._baseService.post(url, model);
  }
  PersonalHomeCarLoanLeadStatusHistory(leadId: number): Observable<ApiResponse<LeadStatusActionHistory[]>> {
    let url = `${this._baseService.API_Url.Personal_Home_Car_Loan_Lead_Status_History_Api}${leadId}`;
    return this._baseService.get(url);
  }
  GetById(id: number): Observable<ApiResponse<FreshLeadHLPLCLModel>> {
    let url = `${this._baseService.API_Url.Personal_Home_Car_Loan_Lead_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdate(model: FreshLeadHLPLCLModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Personal_Home_Car_Loan_Lead_Add_Update_Api}`;
    return this._baseService.post(url, model);
  }
}
