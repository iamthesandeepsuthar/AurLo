import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-lead-approval-history-popup',
  templateUrl: './lead-approval-history-popup.component.html',
  styleUrls: ['./lead-approval-history-popup.component.scss']
})
export class LeadApprovalHistoryPopupComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<LeadApprovalHistoryPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
  }

  onClose(){
    this.dialogRef.close();
  }

}
