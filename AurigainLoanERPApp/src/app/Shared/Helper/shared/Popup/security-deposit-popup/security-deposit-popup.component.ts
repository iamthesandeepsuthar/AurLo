import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, Inject, OnInit } from '@angular/core';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { UserSecurityDepositPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';
import { DropDownModel } from '../../../common-model';
import { DropDown_key } from '../../../constants';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DoorStepAgentService } from 'src/app/Shared/Services/door-step-agent-services/door-step-agent.service';

@Component({
  selector: 'app-security-deposit-popup',
  templateUrl: './security-deposit-popup.component.html',
  styleUrls: ['./security-deposit-popup.component.scss'],
  providers:[DoorStepAgentService]
})
export class SecurityDepositPopupComponent implements OnInit {
  userSecurity = new UserSecurityDepositPostModel();
  formGroup = new FormGroup({});
  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };

  get f() { return this.formGroup.controls; }
  constructor(private readonly fb: FormBuilder,
              private readonly toast:ToastrService,
              private readonly _doorstepAgentService:DoorStepAgentService,
              readonly _commonService: CommonService,
              public dialogRef: MatDialogRef<SecurityDepositPopupComponent>,
              @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string,Heading:string }) {
               }
  ngOnInit(): void {
    this.formInit();
    this.GetDropDown();
  }
  onSubmit() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      this.userSecurity.Amount = this.userSecurity.Amount ? Number(this.userSecurity.Amount) : 0;
      this.userSecurity.UserId = this.data.Id as number;
      this.userSecurity.PaymentModeId = Number(this.userSecurity.PaymentModeId);
     let subscription = this._doorstepAgentService.AddUpdateAgentSecurityDeposit(this.userSecurity).subscribe(res => {
     subscription.unsubscribe();
     if(res.IsSuccess) {
      this.onClose();
     } else {
      this.toast.error(res.Message as string, 'Server Error');
      this.dialogRef.close(false);
      return;
     }
     });
    }
  }
  GetDropDown() {
    this._commonService.GetDropDown([DropDown_key.ddlPaymentMode]).subscribe(res => {
      if (res.IsSuccess) {
        let ddls = res.Data as DropDownModel;
        this.dropDown.ddlPaymentMode = ddls.ddlPaymentMode;
      } else {
        this.dropDown.ddlPaymentMode = [];
      }
    });
  }
  formInit() {
    this.formGroup = this.fb.group({
      AccountNumber: [undefined, Validators.required],
      BankName: [undefined, Validators.required],
      PaymentMode: [undefined, Validators.required],
      Amount: [undefined, Validators.required],
      CreditDate: [undefined, Validators.required],
      ReferanceNumber: [undefined, Validators.required],
    });
  }
  onClose() {
    this.dialogRef.close(true);
  }
}
