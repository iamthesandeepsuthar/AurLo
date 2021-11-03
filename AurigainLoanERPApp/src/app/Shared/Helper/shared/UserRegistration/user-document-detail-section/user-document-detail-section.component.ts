import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { FileInfo } from 'src/app/Content/Common/file-selector/file-selector.component';
import { DocumentTypeEnum } from 'src/app/Shared/Enum/fixed-value';
import { DocumentPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { FilePostModel } from '../../../../Model/doorstep-agent-model/door-step-agent.model';
import { DoorStepAgentService } from '../../../../Services/door-step-agent-services/door-step-agent.service';

@Component({
  selector: 'app-user-document-detail-section',
  templateUrl: './user-document-detail-section.component.html',
  styleUrls: ['./user-document-detail-section.component.scss'],
  providers: [DoorStepAgentService]
})
export class UserDocumentDetailSectionComponent implements OnInit {
  @Input() documentModel = [] as DocumentPostModel[];
  @Output() onSubmit = new EventEmitter<DocumentPostModel[]>();
  DocumentFiles!: FileInfo[];
  formGroup = new FormGroup({});
  docTypeEnum = DocumentTypeEnum;
  TotalAdharDoc!: number | undefined;
  TotalPANDoc!: number | undefined;
  TotalChequeDoc!: number | undefined;
  get f() { return this.formGroup.controls; };

  get getAdharDocsFile(): DocumentPostModel {
    let item = this.documentModel?.find(x => x.DocumentTypeId == this.docTypeEnum.AadhaarCard) as DocumentPostModel;
    return item ?? undefined;
  }
  get getPANDocsFile(): DocumentPostModel {
    let item = this.documentModel?.find(x => x.DocumentTypeId == this.docTypeEnum.PANCard) as DocumentPostModel;
    return item ?? undefined;
  }
  get getChequeDocsFile(): DocumentPostModel {
    let item = this.documentModel?.find(x => x.DocumentTypeId == this.docTypeEnum.CancelledCheque) as DocumentPostModel;
    return item ?? undefined;
  }

  constructor(private readonly fb: FormBuilder, private readonly _toast: ToastrService, readonly _commonService: CommonService, readonly _doorStepAgent: DoorStepAgentService) { }


  ngOnInit(): void {
    this.formInit();

  }

  ngOnChanges(changes: SimpleChanges): void {

    if (changes.documentModel.previousValue != changes.documentModel.currentValue) {
    this.checkExistingFilesCount();
     }

  }
  onFrmSubmit() {
    debugger
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      this.onSubmit.emit(this.documentModel);

    }
  }

  onDocumentAttach(docuemtnTypeId: number, file: FileInfo[], isEdit: boolean) {
debugger
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

      // this.documentModel[docIndex].Files.forEach((element) => {
      //   if (element.Id > 0) {
      //     element.IsEditMode = true;
      //     element.File = undefined;

      //   }

      // });

      file.forEach(element => {

        let Indx = this.documentModel[docIndex].Files.findIndex(x => x.File == element.FileBase64);

        if (Indx < 0) {
          let File = {} as FilePostModel;
          console.log(element.FileBase64);
          File.File = element.FileBase64;
          File.FileName = element.Name;
          File.IsEditMode = false;
          this.documentModel[docIndex].Files.push(File);

          switch (docuemtnTypeId) {
            case this.docTypeEnum.AadhaarCard:
              this.TotalAdharDoc = + 1; // this.documentModel[docIndex].Files.length;
              break;
            case this.docTypeEnum.PANCard:
              this.TotalPANDoc = + 1; // this.documentModel[docIndex].Files.length;
              break;
            case this.docTypeEnum.CancelledCheque:
              this.TotalChequeDoc = + 1; // this.documentModel[docIndex].Files.length;
              break;

          }
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
            this.TotalAdharDoc = + 1;//  this.documentModel[docIndex].Files.length;
            break;
          case this.docTypeEnum.PANCard:
            this.TotalPANDoc = +1;//  this.documentModel[docIndex].Files.length;
            break;
          case this.docTypeEnum.CancelledCheque:
            this.TotalChequeDoc = + 1;//  this.documentModel[docIndex].Files.length;
            break;
        }
      });
      this.documentModel.push(doc);
    }
    return true;
  }

  formInit() {

    this.formGroup = this.fb.group({
      AadhaarCard: [undefined, Validators.required],
      PANCard: [undefined, Validators.required],
      CancelledCheque: [undefined, Validators.required],
    });
  }

  checkExistingFilesCount() {

    if (this.documentModel.length > 0) {

      this.documentModel.forEach(element => {

        switch (element.DocumentTypeId) {
          case this.docTypeEnum.AadhaarCard:
            debugger
            this.TotalAdharDoc = element?.Files?.length > 0 ? element?.Files?.length : undefined;
            break;
          case this.docTypeEnum.PANCard:
            debugger
            this.TotalPANDoc = element?.Files?.length > 0 ? element?.Files?.length : undefined;
            break;
          case this.docTypeEnum.CancelledCheque:
            debugger
            this.TotalChequeDoc = element?.Files?.length > 0 ? element?.Files?.length : undefined;
            break;
        }


      });
    }

  }

  removeDocFile(file: FilePostModel, docId: number, docTypeId: number) {
    this._doorStepAgent.DeleteDocumentFile(file.Id, docId).subscribe(res => {
      if (res.IsSuccess) {
        this._toast.success('File Removed', 'Success');
        let docIndex = this.documentModel!.findIndex(x => x.DocumentTypeId == docTypeId)
        let fileIndex = this.documentModel![docIndex].Files!.findIndex(x => x.Id == file.Id);
        this.documentModel![docIndex].Files.splice(fileIndex, 1);

        switch (docTypeId) {
          case this.docTypeEnum.AadhaarCard:
            this.TotalAdharDoc = - 1;
            break;
          case this.docTypeEnum.PANCard:
            this.TotalPANDoc = -1;
            break;
          case this.docTypeEnum.CancelledCheque:
            this.TotalChequeDoc = - 1;
            break;
        }

      }
    });
  }

}
