import { Component, Input, OnInit } from '@angular/core';
import { FileInfo } from 'src/app/Content/Common/file-selector/file-selector.component';
import { DocumentPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';

@Component({
  selector: 'app-user-document-detail-section',
  templateUrl: './user-document-detail-section.component.html',
  styleUrls: ['./user-document-detail-section.component.scss']
})
export class UserDocumentDetailSectionComponent implements OnInit {
  @Input() documentModel: DocumentPostModel[] = [] as DocumentPostModel[];
  DocumentFiles!: FileInfo[];
  constructor() { 

    
  }

  ngOnInit(): void {
  }

}
