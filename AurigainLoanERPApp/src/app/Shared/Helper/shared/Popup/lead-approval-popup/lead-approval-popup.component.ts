import { Component, OnInit, Inject } from "@angular/core";
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
  providers:[BalanceTransferGoldLoanLeadsService]
})
export class LeadApprovalPopupComponent implements OnInit {
  dropDown = new DropDownModel();
  model!: BtGoldLoanLeadApprovalStagePostModel;
  get ddlkeys() { return DropDown_key };
  constructor(private readonly _commonService :CommonService,
    public dialogRef: MatDialogRef<LeadApprovalPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string }) {
      this.model = new BtGoldLoanLeadApprovalStagePostModel();
     }

  ngOnInit(): void {
    this.GetDropDown();
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
    alert(this.data.Id);
  }


  onClose() {
    this.dialogRef.close(true);
  }

}
