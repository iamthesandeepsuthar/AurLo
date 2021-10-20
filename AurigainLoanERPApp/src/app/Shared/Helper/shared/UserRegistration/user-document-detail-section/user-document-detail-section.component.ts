import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FileInfo } from 'src/app/Content/Common/file-selector/file-selector.component';
import { DocumentTypeEnum } from 'src/app/Shared/Enum/fixed-value';
import { DocumentPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
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
  get f() { return this.formGroup.controls; };
  TotalAdharDoc!: number | undefined;
  TotalPANDoc!: number | undefined;
  TotalChequeDoc!: number | undefined;
  constructor(private readonly fb: FormBuilder, readonly _commonService: CommonService) { }


  ngOnInit(): void {
    this.formInit();
  }
  onFrmSubmit() {

    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      this.onSubmit.emit(this.documentModel);

    }
  }

  onDocumentAttach(docuemtnTypeId: number, file: FileInfo[], isEdit: boolean) {
    let docIndex = this.documentModel.findIndex(x => x.DocumentTypeId == docuemtnTypeId);
    switch (docuemtnTypeId) {
      case this.docTypeEnum.AadhaarCard:
        this.TotalAdharDoc = undefined;
        break;
      case this.docTypeEnum.PANCard:
        this.TotalPANDoc = undefined;
        break;
      case this.docTypeEnum.CancelledCheque:
        this.TotalChequeDoc = undefined;
        break;

    }
    if (docIndex >= 0) {
      file.forEach(element => {
        let File = {} as FilePostModel;
        console.log(element.FileBase64);
        File.File = element.FileBase64;
        File.FileName = element.Name;
        File.IsEditMode = false;
        this.documentModel[docIndex].Files.push(File);
        switch (docuemtnTypeId) {
          case this.docTypeEnum.AadhaarCard:
            this.TotalAdharDoc = this.documentModel[docIndex].Files.length;
            break;
          case this.docTypeEnum.PANCard:
            this.TotalPANDoc = this.documentModel[docIndex].Files.length;
            break;
          case this.docTypeEnum.CancelledCheque:
            this.TotalChequeDoc = this.documentModel[docIndex].Files.length;
            break;

        }

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

        switch (docuemtnTypeId) {
          case this.docTypeEnum.AadhaarCard:
            this.TotalAdharDoc = this.documentModel[docIndex].Files.length;
            break;
          case this.docTypeEnum.PANCard:
            this.TotalPANDoc = this.documentModel[docIndex].Files.length;
            break;
          case this.docTypeEnum.CancelledCheque:
            this.TotalChequeDoc = this.documentModel[docIndex].Files.length;
            break;
        }
      });
      this.documentModel.push(doc);
    }
  }

  formInit() {
    this.formGroup = this.fb.group({
      AadhaarCard: [undefined, Validators.required],
      PANCard: [undefined, Validators.required],
      CancelledCheque: [undefined, Validators.required],
    });
  }

  getDocsFile(docuemtnTypeId: number): DocumentPostModel {
    let item = this.documentModel.find(x => x.DocumentTypeId == docuemtnTypeId) as DocumentPostModel;
    return item
  }
}
