import { ToastrService } from 'ngx-toastr';
import { PersonalHomeCarLoanService } from './../../../../Services/Leads/personal-home-car-loan.service';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { LeadStatusActionHistory } from 'src/app/Shared/Model/Leads/lead-status-model.model';
import { GoldLoanLeadsService } from 'src/app/Shared/Services/Leads/gold-loan-leads.service';
import { BalanceTransferGoldLoanLeadsService } from 'src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service';

@Component({
  selector: 'app-lead-statusl-history-popup',
  templateUrl: './lead-statusl-history-popup.component.html',
  styleUrls: ['./lead-statusl-history-popup.component.scss'],
  providers:[PersonalHomeCarLoanService,GoldLoanLeadsService,BalanceTransferGoldLoanLeadsService]
})
export class LeadStatuslHistoryPopupComponent implements OnInit {
 history!: LeadStatusActionHistory[];
 get History():LeadStatusActionHistory[] {
   return this.history;
 }
  constructor( public dialogRef: MatDialogRef<LeadStatuslHistoryPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private readonly _otherLeadService: PersonalHomeCarLoanService,
    private readonly _leadService: GoldLoanLeadsService,
    private readonly _btGoldLoanService: BalanceTransferGoldLoanLeadsService,
  private readonly _toastService: ToastrService ) {

  }
  ngOnInit( ): void {
    if(this.data.Type == "GoldLoanLeadHistory") {
      this.getFreshGoldLoanLeadStatusHistory();
    } else if( this.data.Type=="OtherLeadHistory"){
      this.getOtherLoanLeadStatusHistory()
    } else if(this.data.Type =="BTLEAD") {
      this.getBTGoldLoanLeadStatusHistory();
    }
  }
  getFreshGoldLoanLeadStatusHistory() {
   let subscription = this._leadService.FreshGoldLoanLeadStatusHistory(this.data.Id).subscribe(response => {
     subscription.unsubscribe();
     if(response.IsSuccess) {
     this.history = response.Data as LeadStatusActionHistory[];
     } else{
     this._toastService.warning('History not found', 'No records');
     return;
      }
   });
  }
  getOtherLoanLeadStatusHistory() {
    let subscription = this._otherLeadService.PersonalHomeCarLoanLeadStatusHistory(this.data.Id).subscribe(response => {
      subscription.unsubscribe();
      if(response.IsSuccess) {
      this.history = response.Data as LeadStatusActionHistory[];
      } else{
      this._toastService.warning('History not found', 'No records');
      return;
       }
    });
  }
  getBTGoldLoanLeadStatusHistory() {
    let subscription = this._btGoldLoanService.BTGoldLoanLeadStatusHistory(this.data.Id).subscribe(response => {
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
