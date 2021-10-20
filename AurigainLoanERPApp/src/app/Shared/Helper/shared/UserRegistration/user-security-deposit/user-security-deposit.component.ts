import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { UserSecurityDepositPostModel } from '../../../../Model/doorstep-agent-model/door-step-agent.model';
import { DropDownModel } from '../../../common-model';
import { DropDown_key } from '../../../constants';

@Component({
  selector: 'app-user-security-deposit',
  templateUrl: './user-security-deposit.component.html',
  styleUrls: ['./user-security-deposit.component.scss']
})
export class UserSecurityDepositComponent implements OnInit {

  @Input() userSecurity = new UserSecurityDepositPostModel();
  @Output() onSubmit = new EventEmitter<UserSecurityDepositPostModel>();
  formGroup = new FormGroup({});
  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };

  get f() { return this.formGroup.controls; }
  constructor(private readonly fb: FormBuilder, readonly _commonService: CommonService) { }

  ngOnInit(): void {
    this.formInit();
    this.GetDropDown();
  }


  onFrmSubmit() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
    //  this.userSecurity.Amount= (this.userSecurity.Amount);
      this.onSubmit.emit(this.userSecurity);
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


}
