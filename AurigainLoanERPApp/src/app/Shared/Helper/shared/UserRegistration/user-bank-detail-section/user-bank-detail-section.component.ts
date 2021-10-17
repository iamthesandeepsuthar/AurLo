import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserBankDetailsPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';
import { CommonService } from 'src/app/Shared/Services/common.service';

@Component({
  selector: 'app-user-bank-detail-section',
  templateUrl: './user-bank-detail-section.component.html',
  styleUrls: ['./user-bank-detail-section.component.scss']
})
export class UserBankDetailSectionComponent implements OnInit {
  @Input() bankModel = new UserBankDetailsPostModel();
  @Output() onSubmit = new EventEmitter<UserBankDetailsPostModel>();
  formGroup = new FormGroup({});

  get f() { return this.formGroup.controls; }
  constructor(private readonly fb: FormBuilder, private readonly _commonService: CommonService) { }

  ngOnInit(): void {
    this.formInit();
  }


  onFrmSubmit() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      this.onSubmit.emit(this.bankModel);

    }
  }


  formInit() {
    this.formGroup = this.fb.group({
      AccountNumber: [undefined, Validators.required],
      Ifsccode: [undefined, Validators.required],
      BankName: [undefined, Validators.required]

    });
  }

}
