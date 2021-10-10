import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FileInfo } from 'src/app/Content/Common/file-selector/file-selector.component';
import { DocumentPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';
import { FilePostModel } from '../../../../Model/doorstep-agent-model/door-step-agent.model';

@Component({
  selector: 'app-user-document-detail-section',
  templateUrl: './user-document-detail-section.component.html',
  styleUrls: ['./user-document-detail-section.component.scss']
})
export class UserDocumentDetailSectionComponent implements OnInit {
  @Input() documentModel: DocumentPostModel[] = [] as DocumentPostModel[];
  @Output() onSubmit = new EventEmitter<DocumentPostModel[]>();
  DocumentFiles!: FileInfo[];
  formGroup!: FormGroup;
  get f() { return this.formGroup.controls; }
  constructor() {


  }

  ngOnInit(): void {
  }
  onFrmSubmit() {
    this.onSubmit.emit(this.documentModel);
  }
  onDocumentAttach(docuemtnTypeId: number, file: FileInfo, isEdit: boolean) {
    debugger;
    let doc  = {} as DocumentPostModel;
    doc.Files = [] as FilePostModel[];
    doc.DocumentTypeId = 1;
    doc.Files[0].File = file.FileBase64;
    doc.Files[0].FileName = file.Name;
    doc.Files[0].IsEditMode = false;


  }
}
