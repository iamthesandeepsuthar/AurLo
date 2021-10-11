import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { FileInfo } from 'src/app/Content/Common/file-selector/file-selector.component';
import { DocumentTypeEnum } from 'src/app/Shared/Enum/fixed-value';
import { DocumentPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';
import { FilePostModel } from '../../../../Model/doorstep-agent-model/door-step-agent.model';

@Component({
  selector: 'app-user-document-detail-section',
  templateUrl: './user-document-detail-section.component.html',
  styleUrls: ['./user-document-detail-section.component.scss']
})
export class UserDocumentDetailSectionComponent implements OnInit {
  @Input() documentModel = [] as DocumentPostModel[];
  @Output() onSubmit = new EventEmitter<DocumentPostModel[]>();
  DocumentFiles!: FileInfo[];
  formGroup = new FormGroup({});
  docTypeEnum = DocumentTypeEnum;
  get f() { return this.formGroup.controls; }
  constructor() {
    this.formGroup

  }

  ngOnInit(): void {
  }
  onFrmSubmit() {
    debugger
    this.onSubmit.emit(this.documentModel);
  }
  onDocumentAttach(docuemtnTypeId: number, file: FileInfo[], isEdit: boolean) {
    debugger

    let docIndex = this.documentModel.findIndex(x => x.DocumentTypeId == docuemtnTypeId);
    if (docIndex >= 0) {
      this.documentModel[docIndex].Files = [] as FilePostModel[];
      file.forEach(element => {
        let File = {} as FilePostModel;
        console.log(element.FileBase64);
        File.File = element.FileBase64;
        File.FileName = element.Name;
        File.IsEditMode = false;
        this.documentModel[docIndex].Files.push(File);
      });
    } else {
      let doc = {} as DocumentPostModel;
      doc.Files = [] as FilePostModel[];
      doc.DocumentTypeId = docuemtnTypeId;

      file.forEach(element => {
        let File = {} as FilePostModel;
        console.log(element.FileBase64);
        File.File = element.FileBase64;
        File.FileName = element.Name;
        File.IsEditMode = false;
        doc.Files.push(File);
      });
      this.documentModel.push(doc);
    }


  }
}
