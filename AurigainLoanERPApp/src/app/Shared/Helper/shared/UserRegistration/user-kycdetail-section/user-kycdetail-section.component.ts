import { Component, Input, OnInit } from '@angular/core';
import { UserKYCPostModel } from '../../../../Model/doorstep-agent-model/door-step-agent.model';

@Component({
  selector: 'app-user-kycdetail-section',
  templateUrl: './user-kycdetail-section.component.html',
  styleUrls: ['./user-kycdetail-section.component.scss']
})
export class UserKYCDetailSectionComponent implements OnInit {
  @Input() kycModel : UserKYCPostModel[] = [] as UserKYCPostModel[];

  constructor() { }

  ngOnInit(): void {
  }

}
