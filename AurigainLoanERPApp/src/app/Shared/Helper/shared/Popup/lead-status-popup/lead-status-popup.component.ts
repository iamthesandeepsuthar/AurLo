import { ToastrService } from 'ngx-toastr';
import { LeadStatusModel } from './../../../../Model/Leads/lead-status-model.model';
import { Component, Inject, OnInit } from '@angular/core';
import { DropDownModel } from '../../../common-model';
import { DropDown_key } from '../../../constants';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { GoldLoanLeadsService } from 'src/app/Shared/Services/Leads/gold-loan-leads.service';

@Component({
  selector: 'app-lead-status-popup',
  templateUrl: './lead-status-popup.component.html',
  styleUrls: ['./lead-status-popup.component.scss'],
  providers:[GoldLoanLeadsService]
})
export class LeadStatusPopupComponent implements OnInit {
  dropDown = new DropDownModel();
  model = new LeadStatusModel();
  get ddlkeys() { return DropDown_key };
  constructor(private readonly _commonService: CommonService,private readonly _freshLead: GoldLoanLeadsService,private readonly _toast: ToastrService,
    public dialogRef: MatDialogRef<LeadStatusPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string }) {
  }
  ngOnInit(): void {
    this.GetDropDown();
    alert(this.data.Id);
  }
  GetDropDown() {
    let serve = this._commonService.GetDropDown([this.ddlkeys.ddlLeadApprovalStatus]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        let dropdownList = res?.Data as DropDownModel;
        this.dropDown.ddlLeadStatus = dropdownList?.ddlLeadStatus ?? [];
      }
    });
  }
  onSubmit() {
    this.model.LeadId = this.data.Id;
    this.model.ActionDate = new Date();
   let subscription =  this._freshLead.LeadStatus(this.model).subscribe(response => {
     subscription.unsubscribe();
      if(response.IsSuccess) {
    this.onClose();
    } else {
     this._toast.error(response.Message  as string , 'Server Error');
     return;
    }
    });

  }
  onClose() {
    this.dialogRef.close(true);
  }
}
