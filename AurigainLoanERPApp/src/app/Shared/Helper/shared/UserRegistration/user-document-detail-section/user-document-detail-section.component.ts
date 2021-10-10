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
  onDocumentAttach(docuemtnTypeId: number, file: FileInfo[], isEdit: boolean) {
    debugger;
    let doc = {} as DocumentPostModel;
    doc.DocumentTypeId = 1;
    doc.Files = [] as FilePostModel[];

    file.forEach(element => {
      let File = {} as FilePostModel;
      console.log(element.FileBase64);
      File.File = element.FileBase64;
      File.FileName = element.Name;
      File.IsEditMode = false;
      doc.Files.push(File);
    });


  }
}
