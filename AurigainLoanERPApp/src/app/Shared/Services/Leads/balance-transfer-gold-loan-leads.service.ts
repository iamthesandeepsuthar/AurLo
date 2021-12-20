import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseAPIService } from "../../Helper/base-api.service";
import { ApiResponse, IndexModel } from "../../Helper/common-model";
import { AppointmentDetail, BtGoldLoanLeadAppointmentPostModel, BtGoldLoanLeadApprovalStagePostModel, BTGoldLoanLeadListModel, BTGoldLoanLeadPostModel, BTGoldLoanLeadViewModel } from "../../Model/Leads/btgold-loan-lead-post-model.model";
import { LeadStatusActionHistory, LeadStatusModel } from "../../Model/Leads/lead-status-model.model";
import { BalanceTransferReturnPostModel, BalanceTransferReturnViewModel } from "./balance-transfer-return-post-model.model";

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
  AddUpdateExternalLead(model: BTGoldLoanLeadPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead__AddUpdateExternalLead_Api}`;
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
  AddUpdateInternalLead(model: BTGoldLoanLeadPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead__AddUpdateInternalLead_Api}`;
    return this._baseService.post(url, model);
  }
  UpdateLeadApprovalStatus(model: BtGoldLoanLeadApprovalStagePostModel) : Observable<ApiResponse<any>> {

    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead_Approval_Status_Api}`;
    return this._baseService.post(url, model);
  }

  LeadStatus(model: LeadStatusModel) : Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead_Status_Change_Api}`;
    return this._baseService.post(url, model);
  }
  BTGoldLoanLeadStatusHistory(leadId: number): Observable<ApiResponse<LeadStatusActionHistory[]>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Fresh_Lead_Status_History_Api}${leadId}`;
    return this._baseService.get(url);
  }
  BTGoldLoanApprovalHistory(leadId: number): Observable<ApiResponse<LeadStatusActionHistory[]>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Fresh_Approval_Status_History_Api}${leadId}`;
    return this._baseService.get(url);
  }
  GetListBalanceReturn(model: IndexModel): Observable<ApiResponse<BTGoldLoanLeadListModel[]>> {

    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead_Balance_return_List_Api}`;
    return this._baseService.post(url, model);
  }
  //#region <<Balance Return Process Service>>

  GetLeadDetailById(id: number): Observable<ApiResponse<BalanceTransferReturnViewModel>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead_Balance_Return_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdateBTBalanceReturn(model: BalanceTransferReturnPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead_Add_Update_Balance_Return_Api}`;
    return this._baseService.post(url, model);
  }
  GetBTAppointmentByLeadId(id: number): Observable<ApiResponse<AppointmentDetail>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead_Appointment_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  SaveAppointment(model: BtGoldLoanLeadAppointmentPostModel): Observable<ApiResponse<any>> {
    let url = `${this._baseService.API_Url.BT_Gold_Loan_Lead_Add_Update_Appointment_Api}`;
    return this._baseService.post(url, model);
  }

  //#endregion </Balance>
}
