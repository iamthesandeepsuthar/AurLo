import { ToastrService } from 'ngx-toastr';
import { LeadStatusModel } from './../../../../Model/Leads/lead-status-model.model';
import { Component, Inject, OnInit } from '@angular/core';
import { DropDownModel } from '../../../common-model';
import { DropDown_key } from '../../../constants';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GoldLoanLeadsService } from 'src/app/Shared/Services/Leads/gold-loan-leads.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-lead-status-popup',
  templateUrl: './lead-status-popup.component.html',
  styleUrls: ['./lead-status-popup.component.scss'],
  providers:[GoldLoanLeadsService]
})
export class LeadStatusPopupComponent implements OnInit {
  dropDown = new DropDownModel();
  model = new LeadStatusModel();
  formgrp!: FormGroup;
  get ddlkeys() { return DropDown_key };
  get f() { return this.formgrp.controls; }
  constructor(private readonly fb: FormBuilder,
              private readonly _commonService: CommonService,
              private readonly _freshLead: GoldLoanLeadsService,
              private readonly _toast: ToastrService,
              public dialogRef: MatDialogRef<LeadStatusPopupComponent>,
             @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string }) {
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
        console.log(this.dropDown.ddlLeadStatus);
      }
    });
  }
  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
       debugger;
       this.model.LeadId = this.data.Id;
       this.model.ActionDate = new Date();
       this.model.LeadStatus = Number(this.model.LeadStatus);
   let subscription =  this._freshLead.LeadStatus(this.model).subscribe(response => {
     subscription.unsubscribe();
      if(response.IsSuccess) {
        this.onClose();
    } else {
     this._toast.error(response.Message  as string , 'Server Error');
     this.dialogRef.close(false);
     return;
    }
    });
    }

  }
  onClose() {
    this.dialogRef.close(true);
  }
}
