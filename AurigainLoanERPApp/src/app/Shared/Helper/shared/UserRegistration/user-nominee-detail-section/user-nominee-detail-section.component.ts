import { UserNomineePostModel } from './../../../../Model/doorstep-agent-model/door-step-agent.model';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-nominee-detail-section',
  templateUrl: './user-nominee-detail-section.component.html',
  styleUrls: ['./user-nominee-detail-section.component.scss']
})
export class UserNomineeDetailSectionComponent implements OnInit {
  @Input() nomineeModel : UserNomineePostModel = {} as UserNomineePostModel;
  constructor() { }

  ngOnInit(): void {
  }

}
