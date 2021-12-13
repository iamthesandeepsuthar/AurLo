import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-lead-statusl-history-popup',
  templateUrl: './lead-statusl-history-popup.component.html',
  styleUrls: ['./lead-statusl-history-popup.component.scss']
})
export class LeadStatuslHistoryPopupComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<LeadStatuslHistoryPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit(): void {
  }

  onClose(){
    this.dialogRef.close();
  }
}
