import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { PaymentMethod, UserRoleEnum } from "src/app/Shared/Enum/fixed-value";
import { AuthService } from "src/app/Shared/Helper/auth.service";
import { CommonService } from "src/app/Shared/Services/common.service";
import { BalanceTransferGoldLoanLeadsService } from "src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service";
import { FilePostModel } from "../../../Shared/Model/doorstep-agent-model/door-step-agent.model";
import { FileInfo } from "../../Common/file-selector/file-selector.component";
import { BalanceTransferReturnViewModel, BalanceTransferReturnPostModel, BalanceTransferReturnBankChequeDetail } from "./../../../Shared/Services/Leads/balance-transfer-return-post-model.model";

@Component({
  selector: 'app-balance-transfer-return',
  templateUrl: './balance-transfer-return.component.html',
  styleUrls: ['./balance-transfer-return.component.scss'],
  providers: [BalanceTransferGoldLoanLeadsService]
})
export class BalanceTransferReturnComponent implements OnInit {
  leadId: number = 0;
  detailModel: BalanceTransferReturnViewModel = new BalanceTransferReturnViewModel();
  model: BalanceTransferReturnPostModel = new BalanceTransferReturnPostModel();
  chequeDetail = new BalanceTransferReturnBankChequeDetail();
  IsDisbursement: boolean = false;
  IsFinalPaymentDate: boolean = false;
  IsLoanAccountNumber: boolean = false;
  IsChequeDetail: boolean = false;
  formBalanceReturn!: FormGroup;
  get f() { return this.formBalanceReturn.controls; }
  @ViewChild('HideAddUpdateModel') HideAddUpdateModel!: ElementRef;
  get DetailModel(): BalanceTransferReturnViewModel {
    return this.detailModel;
  }
  get Model(): BalanceTransferReturnPostModel {
    return this.model;
  }
  get ChequeDetail(): BalanceTransferReturnBankChequeDetail {
    return this.chequeDetail;
  }
  get UserDetail() { return this._auth.GetUserDetail() };
  get UserRoleEnum() { return UserRoleEnum };
  constructor(private readonly _toast: ToastrService,
    private readonly fb: FormBuilder,
    private readonly _btLeadService: BalanceTransferGoldLoanLeadsService,
    readonly _commonService: CommonService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _auth: AuthService,) {
    if (_activatedRoute.snapshot.params.id) {
      this.leadId = _activatedRoute.snapshot.params.id;
    }
  }
  ngOnInit(): void {
    this.formInit();
    if (this.leadId > 0) {
      this.getLeadDetail();
    }
  }
  getLeadDetail() {
    let subscription = this._btLeadService.GetLeadDetailById(this.leadId).subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        this.detailModel = response.Data as BalanceTransferReturnViewModel;
        this.detailModel.BalanceTransferReturn.Id = Number(this.detailModel.BalanceTransferReturn.Id);
      } else {
        this._toast.error(response.Message as string, 'Server Error');
        return;
      }
    });
  }
  formInit() {
    this.formBalanceReturn = this.fb.group({
      IsDisbursement: [undefined, Validators.required],
      LoanAccountNumber: [undefined],
      CustomerName: [undefined, Validators.required],
      BankName: [undefined, Validators.required],
      PaymentMethod: [undefined, Validators.required],
      ReturnAmount: [undefined, Validators.required],
      FinalPaymentDate: [undefined],
      UtrNo: [undefined],
      Remark: [undefined],
      ChequeNumber: [undefined]
    });
  }
  CheckDisbursementStatus(value: any) {
    if (value == "true") {
      this.IsLoanAccountNumber = true;
    } else if (value == "false") {
      this.IsLoanAccountNumber = false;
    }
  }
  SaveStatus() {
    this.model.GoldReceived = this.DetailModel.BalanceTransferReturn.GoldReceived;
    this.model.AmountPainToExistingBank = this.detailModel.BalanceTransferReturn.AmountPainToExistingBank;
    this.model.GoldSubmittedToBank = this.detailModel.BalanceTransferReturn.GoldSubmittedToBank;
    this.model.LeadId = Number(this.detailModel.Id);
    this.model.BtReturnId = Number(this.detailModel.BalanceTransferReturn.Id);
    let subscription = this._btLeadService.AddUpdateBTBalanceReturn(this.model).subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        this._toast.success(response.Message as string, 'Success');
        return;
      } else {
        this._toast.error(response.Message as string, 'Server Error');
        return;
      }
    });
  }
  checkLoanAmount(): boolean {
    if (this.model.AmountReturn != this.detailModel.LoanAmount) {
      this.IsFinalPaymentDate = true;
      this._toast.warning('Please select final payment date', 'Required');
      return false;
    } {
      this.IsFinalPaymentDate = false;
      return true;
    }
  }
  checkPaymentMethod(paymentMethod: number | null) {
    let paymentMethodValue = Number(paymentMethod);
    if (paymentMethod == PaymentMethod.CHEQUE) {
      this.IsChequeDetail = true;
    } else {
      this.IsChequeDetail = false;
    }
  }
  HandleChequeFiles(files: FileInfo[]) {
    if (files && files.length > 0) {

      this.chequeDetail.ChequeImageUrl.File = files[0]!.FileBase64;
      this.chequeDetail.ChequeImageUrl.FileName = files[0]!.Name;
      this.chequeDetail.ChequeImageUrl.IsEditMode = false;
      this.chequeDetail.ChequeImageUrl.FileType = files[0]!.Name.split('.')[1];
    }
    else {
      this.chequeDetail.ChequeImageUrl = {} as any;
    }
    //  this.model.ChequeDetail.ChequeImageUrl = this.chequeDetail.ChequeImageUrl;

  }
  SetChequeDetail() {
    this.chequeDetail.ChequeNumber = this.chequeDetail?.ChequeNumber ? String(this.chequeDetail?.ChequeNumber) : undefined as any;
    this.model.ChequeDetail = this.chequeDetail;
    this.HideAddUpdateModel.nativeElement.click();
  }
  FinalSubmit() {
    this.formBalanceReturn.markAllAsTouched();
    if (this.formBalanceReturn.valid) {
      let returnAmountValue = this.checkLoanAmount();
      if (returnAmountValue) {
        this.model.BtReturnId = Number(this.detailModel.BalanceTransferReturn.Id);
        this.model.LoanDisbursment = Boolean(this.model.LoanDisbursment);
        this.model.AmountReturn = Number(this.model.AmountReturn);
        this.model.PaymentMethod = Number(this.model.PaymentMethod);
        this.model.GoldReceived = this.DetailModel.BalanceTransferReturn.GoldReceived;
        this.model.AmountPainToExistingBank = this.detailModel.BalanceTransferReturn.AmountPainToExistingBank;
        this.model.GoldSubmittedToBank = this.detailModel.BalanceTransferReturn.GoldSubmittedToBank;
        this.model.LeadId = this.detailModel.Id;
        this.model.BtReturnId = this.detailModel.BalanceTransferReturn.Id;
        let subscription = this._btLeadService.AddUpdateBTBalanceReturn(this.model).subscribe(response => {
          subscription.unsubscribe();
          if (response.IsSuccess) {
            this._toast.success(response.Message as string, 'Success');
            return;
          } else {
            this._toast.error(response.Message as string, 'Server Error');
            return;
          }
        });
      } else if (this.model.FinalPaymentDate != undefined) {
        this.model.BtReturnId = Number(this.detailModel.BalanceTransferReturn.Id);
        this.model.LoanDisbursment = Boolean(this.model.LoanDisbursment);
        this.model.AmountReturn = Number(this.model.AmountReturn);
        this.model.PaymentMethod = Number(this.model.PaymentMethod);
        this.model.GoldReceived = this.DetailModel.BalanceTransferReturn.GoldReceived;
        this.model.AmountPainToExistingBank = this.detailModel.BalanceTransferReturn.AmountPainToExistingBank;
        this.model.GoldSubmittedToBank = this.detailModel.BalanceTransferReturn.GoldSubmittedToBank;
        this.model.LeadId = this.detailModel.Id;
        this.model.BtReturnId = this.detailModel.BalanceTransferReturn.Id;
        let subscription = this._btLeadService.AddUpdateBTBalanceReturn(this.model).subscribe(response => {
          subscription.unsubscribe();
          if (response.IsSuccess) {
            this._toast.success(response.Message as string, 'Success');
            return;
          } else {
            this._toast.error(response.Message as string, 'Server Error');
            return;
          }
        });

      } else {
        this._toast.warning('Please select final payment date', 'Required');
        return;
      }
    }
  }
}
