import { BalanceTransferGoldLoanLeadsService } from 'src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service';
import { LeadStatusActionHistory } from 'src/app/Shared/Model/Leads/lead-status-model.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-lead-approval-history-popup',
  templateUrl: './lead-approval-history-popup.component.html',
  styleUrls: ['./lead-approval-history-popup.component.scss'],
  providers:[BalanceTransferGoldLoanLeadsService]
})
export class LeadApprovalHistoryPopupComponent implements OnInit {
 history!:LeadStatusActionHistory[];
 get History():LeadStatusActionHistory[] {
  return this.history;
}
  constructor(private readonly _btGoldLoanService: BalanceTransferGoldLoanLeadsService,
    private readonly _toastService: ToastrService ,public dialogRef: MatDialogRef<LeadApprovalHistoryPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
  this.getBTLoanApprovalHistory();
  }
  getBTLoanApprovalHistory() {
    let subscription = this._btGoldLoanService.BTGoldLoanApprovalHistory(this.data.Id).subscribe(response => {
      subscription.unsubscribe();
      if(response.IsSuccess) {
      this.history = response.Data as LeadStatusActionHistory[];
      } else{
      this._toastService.warning('History not found', 'No records');
      return;
       }
    });
   }
  onClose(){
    this.dialogRef.close();
  }

}
