import { BalanceTransferReturnBankChequeDetail } from './../../../Shared/Services/Leads/balance-transfer-return-post-model.model';
import { subscribeOn } from 'rxjs/operators';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService, ToastrModule } from 'ngx-toastr';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { BalanceTransferReturnPostModel, BalanceTransferReturnViewModel } from 'src/app/Shared/Services/Leads/balance-transfer-return-post-model.model';
import { BalanceTransferGoldLoanLeadsService } from 'src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { PaymentMethod } from 'src/app/Shared/Enum/fixed-value';

@Component({
  selector: 'app-balance-transfer-return',
  templateUrl: './balance-transfer-return.component.html',
  styleUrls: ['./balance-transfer-return.component.scss'],
  providers:[BalanceTransferGoldLoanLeadsService]
})
export class BalanceTransferReturnComponent implements OnInit {

  leadId: number=0;
  detailModel: BalanceTransferReturnViewModel = new BalanceTransferReturnViewModel();
  model: BalanceTransferReturnPostModel = new BalanceTransferReturnPostModel();
  chequeDetail: BalanceTransferReturnBankChequeDetail = new BalanceTransferReturnBankChequeDetail();
  IsDisbursement:boolean = false;
  IsFinalPaymentDate:boolean= false;
  IsLoanAccountNumber:boolean = false;
  IsChequeDetail:boolean = false;
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
  constructor(private readonly _toast: ToastrService,
              private readonly fb: FormBuilder,
              private readonly _btLeadService: BalanceTransferGoldLoanLeadsService,
              readonly _commonService: CommonService,
              private readonly _activatedRoute: ActivatedRoute) {
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
     if(response.IsSuccess)
     {
       this.detailModel = response.Data as BalanceTransferReturnViewModel;
     } else {
       this._toast.error(response.Message as string , 'Server Error');
       return;
     }
    });
  }
  formInit() {
    this.formBalanceReturn = this.fb.group({
      IsDisbursement: [undefined, Validators.required],
      LoanAccountNumber: [undefined],
      CustomerName: [undefined,Validators.required],
      BankName:[undefined,Validators.required],
      PaymentMethod:[undefined, Validators.required],
      ReturnAmount:[undefined,Validators.required],
      FinalPaymentDate:[undefined],
      UtrNo:[undefined]
    });
  }
  CheckDisbursementStatus(value: any) {
    if(value == "true") {
  this.IsLoanAccountNumber = true;
  } else  if(value =="false"){
  this.IsLoanAccountNumber = false;
  }
  }
  SaveStatus(){
   this.model.GoldReceived = this.DetailModel.BalanceTransferReturn.GoldReceived;
   this.model.AmountPainToExistingBank = this.detailModel.BalanceTransferReturn.AmountPainToExistingBank;
   this.model.GoldSubmittedToBank = this.detailModel.BalanceTransferReturn.GoldSubmittedToBank;
   this.model.LeadId = this.detailModel.Id;
   this.model.BtReturnId = this.detailModel.BalanceTransferReturn.Id;
   let subscription = this._btLeadService.AddUpdateBTBalanceReturn(this.model).subscribe(response =>{
   subscription.unsubscribe();
   if(response.IsSuccess){
   this._toast.success(response.Message as string, 'Success');
   return;
   } else {
     this._toast.error(response.Message as string , 'Server Error');
     return;
   }
   });
  }
  checkLoanAmount() : boolean {
    if(this.model.AmountReturn != this.detailModel.LoanAmount) {
      this.IsFinalPaymentDate = true;
      this._toast.warning('Please select final payment date','Required');
      return false;
    } {
      this.IsFinalPaymentDate = false;
      return true;
    }
  }
  checkPaymentMethod(paymentMethod: number| null){
    let paymentMethodValue = Number(paymentMethod);
    if(paymentMethod == PaymentMethod.CHEQUE) {
       this.IsChequeDetail = true;
    } else {
     this.IsChequeDetail = false;
   }
  }
  SetChequeDetail() {
    this.HideAddUpdateModel.nativeElement.click();
  }
  FinalSubmit(){
    this.formBalanceReturn.markAllAsTouched();
    if(this.formBalanceReturn.valid){
     if(this.checkLoanAmount()) {
      this.model.GoldReceived = this.DetailModel.BalanceTransferReturn.GoldReceived;
      this.model.AmountPainToExistingBank = this.detailModel.BalanceTransferReturn.AmountPainToExistingBank;
      this.model.GoldSubmittedToBank = this.detailModel.BalanceTransferReturn.GoldSubmittedToBank;
      this.model.LeadId = this.detailModel.Id;
      this.model.BtReturnId = this.detailModel.BalanceTransferReturn.Id;
      let subscription = this._btLeadService.AddUpdateBTBalanceReturn(this.model).subscribe(response =>{
        subscription.unsubscribe();
        if(response.IsSuccess){
        this._toast.success(response.Message as string, 'Success');
        return;
        } else {
          this._toast.error(response.Message as string , 'Server Error');
          return;
        }
        });
     }
  }
  }
}
