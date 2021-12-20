
import { GoldLoanLeadsService } from 'src/app/Shared/Services/Leads/gold-loan-leads.service';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { AuthService } from '../../../auth.service';
import { GoldLoanFreshLeadAppointmentDetailModel } from 'src/app/Shared/Model/Leads/gold-loan-fresh-lead.model';
import { DDLBranchModel } from 'src/app/Shared/Model/master-model/bank-model.model';
import { BankBranchService } from 'src/app/Shared/Services/master-services/bank-branch.service';
import { BalanceTransferGoldLoanLeadsService } from 'src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service';
import { BtGoldLoanLeadAppointmentPostModel } from 'src/app/Shared/Model/Leads/btgold-loan-lead-post-model.model';

@Component({
  selector: 'app-appointment-booking-popup',
  templateUrl: './appointment-booking-popup.component.html',
  styleUrls: ['./appointment-booking-popup.component.scss'],
  providers:[GoldLoanLeadsService,BankBranchService,BalanceTransferGoldLoanLeadsService]
})
export class AppointmentBookingPopupComponent implements OnInit {

  leadFromAppointmentDetail!: FormGroup;
  ddlBranchModel!: DDLBranchModel[];
  btModel: BtGoldLoanLeadAppointmentPostModel = new BtGoldLoanLeadAppointmentPostModel();
  model: GoldLoanFreshLeadAppointmentDetailModel = new GoldLoanFreshLeadAppointmentDetailModel();
  get f4 () { return this.leadFromAppointmentDetail.controls; }
  constructor( private readonly toast: ToastrService,
               private readonly _leadService: GoldLoanLeadsService,
               private readonly fb: FormBuilder,
               private readonly _bankService: BankBranchService,
               private readonly _commonService: CommonService,
               private  readonly _authService: AuthService,
               private readonly _btLeadService: BalanceTransferGoldLoanLeadsService,
               public dialogRef: MatDialogRef<AppointmentBookingPopupComponent>,
               @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string ,Heading: string }) { }

  ngOnInit(): void {
    this.formInit();
    if (this.data.Type == "FreshGoldAppointmentBooking") {

    } else if(this.data.Type == "BTAppointmentBooking") {

    }
  }
  formInit() {
    this.leadFromAppointmentDetail = this.fb.group({
      Branch: [undefined, undefined],
      DateofAppointment: [undefined, undefined],
      TimeofAppointment: [undefined, undefined],
      Pincode:[undefined]
    });
  }
  getAppointmentDetail() {
    let serve = this._leadService.GetAppointmentByLeadId(this.data.Id).subscribe(response => {
       serve.unsubscribe();
       if(response.IsSuccess) {
        this.model = response.Data as GoldLoanFreshLeadAppointmentDetailModel;
       } else {
         this.toast.error(response.Message as string, 'Server Error');
         return;
       }
      });
  }
  getBranch() {
  let subscription  = this._bankService.GetBranchesbyPinCode(this.model.Pincode).subscribe(response => {
    if(response.IsSuccess)
    {
    this.ddlBranchModel = response.Data as DDLBranchModel[];
    } else{
      this.toast.error(response.Message as string ,'Server Error');
      return;
    }
  });
  }
  onSubmit(){
    if (this.data.Type == "FreshGoldAppointmentBooking") {
    this.SaveFreshGoldLeadAppointment();
    } else if(this.data.Type == "BTAppointmentBooking") {
    this.SaveBTGoldLeadAppointment();
    }
  }
  SaveFreshGoldLeadAppointment()
  {
    this.model.LeadId = Number(this.data.Id);
    this.model.BranchId = Number(this.model.BranchId);
    let subscription = this._leadService.SaveAppointment(this.model).subscribe
    (response => {
      subscription.unsubscribe();
      if(response.IsSuccess){
      this.onClose();
      } else {
      this.toast.error(response.Message as string , 'Server Error');
      return;
      }
    });
  }
  SaveBTGoldLeadAppointment(){
    this.btModel.LeadId = Number(this.data.Id);
    this.btModel.BranchId = Number(this.model.BranchId);
    this.btModel.AppointmentDate = this.model.AppointmentDate;
    this.btModel.AppointmentTime = this.model.AppointmentTime;
    let subscription = this._btLeadService.SaveAppointment(this.btModel).subscribe
    (response => {
      subscription.unsubscribe();
      if(response.IsSuccess){
      this.onClose();
      } else {
      this.toast.error(response.Message as string , 'Server Error');
      return;
      }
    });
  }
  onClose() {
    this.dialogRef.close(true);
  }
}
