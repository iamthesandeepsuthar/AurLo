import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseAPIService } from "../../Helper/base-api.service";
import { ApiResponse, IndexModel } from "../../Helper/common-model";
import { BTGoldLoanLeadListModel, BTGoldLoanLeadPostModel, BTGoldLoanLeadViewModel } from "../../Model/Leads/btgold-loan-lead-post-model.model";

@Injectable()
export class BalanceTransferGoldLoanLeadsService {
  constructor(private readonly _baseService: BaseAPIService) { }
  GetList(model: IndexModel): Observable<ApiResponse<BTGoldLoanLeadListModel[]>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetById(id: number): Observable<ApiResponse<BTGoldLoanLeadViewModel>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead__Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdate(model: BTGoldLoanLeadPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead__AddUpdate_Api}`;
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

}
