import { PersonalHomeCarLoanService } from 'src/app/Shared/Services/Leads/personal-home-car-loan.service';
import { ToastrService } from 'ngx-toastr';
import { LeadStatusModel } from './../../../../Model/Leads/lead-status-model.model';
import { Component, Inject, OnInit } from '@angular/core';
import { DropDownModel } from '../../../common-model';
import { DropDown_key } from '../../../constants';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GoldLoanLeadsService } from 'src/app/Shared/Services/Leads/gold-loan-leads.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BalanceTransferGoldLoanLeadsService } from '../../../../Services/Leads/balance-transfer-gold-loan-leads.service';
import { AuthService } from '../../../auth.service';

@Component({
  selector: 'app-lead-status-popup',
  templateUrl: './lead-status-popup.component.html',
  styleUrls: ['./lead-status-popup.component.scss'],
  providers: [GoldLoanLeadsService, BalanceTransferGoldLoanLeadsService,PersonalHomeCarLoanService]
})
export class LeadStatusPopupComponent implements OnInit {
  dropDown = new DropDownModel();
  model = new LeadStatusModel();
  formgrp!: FormGroup;
  get ddlkeys() { return DropDown_key };
  get f() { return this.formgrp.controls; }
  constructor(private readonly fb: FormBuilder,
    private readonly _commonService: CommonService,
    readonly _authService: AuthService,
    private readonly _freshLead: GoldLoanLeadsService,
    private readonly _balanceTransfer: BalanceTransferGoldLoanLeadsService,
    private readonly _toast: ToastrService,
    private readonly _otherLead: PersonalHomeCarLoanService,
    public dialogRef: MatDialogRef<LeadStatusPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string,Heading: string }) {
  }
  ngOnInit(): void {
    this.formInit();
    this.GetDropDown();
  }
  formInit() {
    this.formgrp = this.fb.group({
      status: [undefined, Validators.required],
      Remark: [undefined]
    });
  }
  GetDropDown() {
    let serve = this._commonService.GetDropDown([this.ddlkeys.ddlLeadStatus]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        let dropdownList = res?.Data as DropDownModel;
        this.dropDown.ddlLeadStatus = dropdownList?.ddlLeadStatus ?? [];
      }
    });
  }
  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      this.model.LeadId = this.data.Id;
      this.model.ActionDate = new Date();
      this.model.LeadStatus = Number(this.model.LeadStatus);
      if (this.data.Type == "FreshGold") {
        this.FreshLeadStatusUpdate();
      } else if (this.data.Type == "BTLEAD") {
        this.BalanceTransferLeadStatusUpdate();
      } else if(this.data.Type == "OtherLead") {
       this.FreshLeadOtherStatusUpdate();
      }
    }
  }

  FreshLeadStatusUpdate() {
    this.model.ActionTakenByUserId = this._authService.GetUserDetail()?.UserId as number;
    let subscription = this._freshLead.LeadStatus(this.model).subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        this.onClose();
      } else {
        this._toast.error(response.Message as string, 'Server Error');
        this.dialogRef.close(false);
        return;
      }
    });

  }
  FreshLeadOtherStatusUpdate() {
    this.model.ActionTakenByUserId = this._authService.GetUserDetail()?.UserId as number;
    let subscription = this._otherLead.LeadStatus(this.model).subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        this.dialogRef.close(false);
      } else {
        this._toast.error(response.Message as string, 'Server Error');
        this.dialogRef.close(false);
        return;
      }
    });

  }
  BalanceTransferLeadStatusUpdate() {
    this.model.ActionTakenByUserId = this._authService.GetUserDetail()?.UserId as number;
    let subscription = this._balanceTransfer.LeadStatus(this.model).subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        this.dialogRef.close(false);
      } else {
        this._toast.error(response.Message as string, 'Server Error');
        this.dialogRef.close(false);
        return;
      }
    });
  }
  onClose() {
    this.dialogRef.close(true);
  }
}
