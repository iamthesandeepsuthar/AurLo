import { Component, Input, OnInit } from '@angular/core';
import { DocumentPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';

@Component({
  selector: 'app-user-document-detail-section',
  templateUrl: './user-document-detail-section.component.html',
  styleUrls: ['./user-document-detail-section.component.scss']
})
export class UserDocumentDetailSectionComponent implements OnInit {
  @Input() documentModel: DocumentPostModel[] = [] as DocumentPostModel[];

  constructor() { }

  ngOnInit(): void {
  }

}
