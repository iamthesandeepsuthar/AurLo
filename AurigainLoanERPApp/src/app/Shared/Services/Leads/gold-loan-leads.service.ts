import { GoldLoanFreshLeadViewModel } from './../../Model/Leads/gold-loan-fresh-lead.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { IndexModel, ApiResponse } from '../../Helper/common-model';
import { GoldLoanFreshLeadListModel, GoldLoanFreshLeadModel } from '../../Model/Leads/gold-loan-fresh-lead.model';
import { BankModel } from '../../Model/master-model/bank-model.model';
import { LeadStatusModel } from '../../Model/Leads/lead-status-model.model';

@Injectable()
export class GoldLoanLeadsService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetList(model: IndexModel): Observable<ApiResponse<GoldLoanFreshLeadListModel[]>> {
    let url = `${this._baseService.API_Url.Gold_Loan_Fresh_Lead_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetById(id: number): Observable<ApiResponse<GoldLoanFreshLeadModel>> {
    let url = `${this._baseService.API_Url.Gold_Loan_Fresh_Lead__Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  GetGoldLoanFreshLeadDetailById(id: number) : Observable<ApiResponse<GoldLoanFreshLeadViewModel>> {
    let url = `${this._baseService.API_Url.Gold_Loan_Fresh_Lead__Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdate(model: GoldLoanFreshLeadModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Gold_Loan_Fresh_Lead__AddUpdate_Api}`;
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
    let url = `${this._baseService.API_Url.Gold_Loan_Fresh_Lead_Status_Change_Api}`;
    return this._baseService.post(url, model);
  }
}
