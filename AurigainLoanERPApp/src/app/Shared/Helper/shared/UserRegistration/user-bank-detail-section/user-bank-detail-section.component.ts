import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import {  UserBankDetailsPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';

@Component({
  selector: 'app-user-bank-detail-section',
  templateUrl: './user-bank-detail-section.component.html',
  styleUrls: ['./user-bank-detail-section.component.scss']
})
export class UserBankDetailSectionComponent implements OnInit {
  @Input()  bankModel : UserBankDetailsPostModel={} as UserBankDetailsPostModel;
  @Output() onSubmit = new EventEmitter<UserBankDetailsPostModel>();
  formGroup!: FormGroup;

  get f() { return this.formGroup.controls; }
  constructor() { }

  ngOnInit(): void {
  }

  onFrmSubmit(){
    this.onSubmit.emit(this.bankModel);
  }
}
