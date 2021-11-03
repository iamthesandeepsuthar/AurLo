import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UserKYCPostModel, DocumentPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';

@Component({
  selector: 'app-user-kycdocument-detail',
  templateUrl: './user-kycdocument-detail.component.html',
  styleUrls: ['./user-kycdocument-detail.component.scss']
})
export class UserKYCDocumentDetailComponent implements OnInit {
  @Input() kycModel = [] as UserKYCPostModel[];
  @Output() onKYCSubmit = new EventEmitter<UserKYCPostModel[]>();

  @Input() documentModel = [] as DocumentPostModel[];
  @Output() onDocSubmit = new EventEmitter<DocumentPostModel[]>();

  constructor() { }

  ngOnInit(): void {
  }

}
