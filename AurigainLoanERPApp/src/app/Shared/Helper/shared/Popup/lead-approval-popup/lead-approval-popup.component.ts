import { Component, OnInit, Inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { BtGoldLoanLeadApprovalStagePostModel } from "src/app/Shared/Model/Leads/btgold-loan-lead-post-model.model";
import { BalanceTransferGoldLoanLeadsService } from "src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service";
import { CommonService } from "../../../../Services/common.service";
import { DropDownModel } from "../../../common-model";
import { DropDown_key } from "../../../constants";

@Component({
  selector: 'app-lead-approval-popup',
  templateUrl: './lead-approval-popup.component.html',
  styleUrls: ['./lead-approval-popup.component.scss'],
  providers: [BalanceTransferGoldLoanLeadsService]
})
export class LeadApprovalPopupComponent implements OnInit {
  dropDown = new DropDownModel();
  model = new BtGoldLoanLeadApprovalStagePostModel();
  get ddlkeys() { return DropDown_key };
  formgrp!: FormGroup;
  get f() { return this.formgrp.controls; }

  constructor(private readonly fb: FormBuilder,
    private readonly _commonService: CommonService,
     private readonly _btLeadService: BalanceTransferGoldLoanLeadsService,
    public dialogRef: MatDialogRef<LeadApprovalPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string }) {
    this.model.LeadId = data.Id;
  }

  ngOnInit(): void {
    this.formInit();
    this.GetDropDown();
  }

  formInit() {
    this.formgrp = this.fb.group({
      ApprovalStatus: [undefined, Validators.required],
      Remark: [undefined]
    });
  }

  GetDropDown() {
    let serve = this._commonService.GetDropDown([this.ddlkeys.ddlLeadApprovalStatus]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        let dropdownList = res?.Data as DropDownModel;
        this.dropDown.ddlLeadApprovalStatus = dropdownList?.ddlLeadApprovalStatus ?? [];
      }
    });
  }
  onSubmit() {
    let serve = this._btLeadService.UpdateLeadApprovalStatus(this.model).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.dialogRef.close(true);
      } else {
        this.dialogRef.close(false);
      }
    }, error => {

    });

  }


  onClose() {
    this.dialogRef.close(false);
  }

}
