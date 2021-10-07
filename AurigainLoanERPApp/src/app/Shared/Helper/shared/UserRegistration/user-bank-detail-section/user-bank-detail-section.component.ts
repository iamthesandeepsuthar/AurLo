import { Component, Input, OnInit } from '@angular/core';
import { BankDetailsPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';

@Component({
  selector: 'app-user-bank-detail-section',
  templateUrl: './user-bank-detail-section.component.html',
  styleUrls: ['./user-bank-detail-section.component.scss']
})
export class UserBankDetailSectionComponent implements OnInit {
  @Input()  bankModel : BankDetailsPostModel={} as BankDetailsPostModel;
  constructor() { }

  ngOnInit(): void {
  }

}
