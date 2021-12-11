import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-lead-approval-popup',
  templateUrl: './lead-approval-popup.component.html',
  styleUrls: ['./lead-approval-popup.component.scss']
})
export class LeadApprovalPopupComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<LeadApprovalPopupComponent>, @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string }) { }

  ngOnInit(): void {
    this.onSubmit();
  }

  onSubmit() {
    alert(this.data.Id);
  }


  onClose() {
    this.dialogRef.close(true);
  }

}
