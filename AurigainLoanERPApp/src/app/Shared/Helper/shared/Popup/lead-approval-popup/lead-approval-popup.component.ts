import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommonService } from '../../../../Services/common.service';
import { DropDownModel } from '../../../common-model';
import { DropDown_key } from '../../../constants';

@Component({
  selector: 'app-lead-approval-popup',
  templateUrl: './lead-approval-popup.component.html',
  styleUrls: ['./lead-approval-popup.component.scss']
})
export class LeadApprovalPopupComponent implements OnInit {
  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };
  constructor(private readonly _commonService :CommonService,
    public dialogRef: MatDialogRef<LeadApprovalPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string }) { }

  ngOnInit(): void {
    this.GetDropDown();

  }


  GetDropDown() {
    let serve = this._commonService.GetDropDown([this.ddlkeys.ddlLeadApprovalStatus]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        let ddls = res?.Data as DropDownModel;
        this.dropDown.ddlLeadApprovalStatus = ddls?.ddlLeadApprovalStatus ?? [];
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
