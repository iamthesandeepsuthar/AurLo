import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges, OnChanges } from "@angular/core";
import { FormGroup, AbstractControl, FormArray, FormBuilder, Validators } from "@angular/forms";
import { ToastrService } from "ngx-toastr";
import { FileInfo } from "src/app/Content/Common/file-selector/file-selector.component";
import { UserKYCPostModel, DocumentPostModel, FilePostModel } from "src/app/Shared/Model/doorstep-agent-model/door-step-agent.model";
import { DDLDocumentTypeModel } from "src/app/Shared/Model/master-model/document-type.model";
import { AlertService } from "src/app/Shared/Services/alert.service";
import { CommonService } from "src/app/Shared/Services/common.service";
import { KycDocumentTypeService } from "src/app/Shared/Services/master-services/kyc-document-type.service";
import { UserSettingService } from 'src/app/Shared/Services/user-setting-services/user-setting.service';

@Component({
  selector: 'app-user-kycdocument-detail',
  templateUrl: './user-kycdocument-detail.component.html',
  styleUrls: ['./user-kycdocument-detail.component.scss'],
  providers: [KycDocumentTypeService, UserSettingService]

})
export class UserKYCDocumentDetailComponent implements OnInit, OnChanges {
  @Input() kycModel = [] as UserKYCPostModel[];
  @Output() onKYCSubmit = new EventEmitter<UserKYCPostModel[]>();

  @Input() documentModel = [] as DocumentPostModel[];
  @Output() onDocSubmit = new EventEmitter<DocumentPostModel[]>();

  docTypeModel!: DDLDocumentTypeModel[];
  frmGroup: FormGroup = new FormGroup({});

  get f(): any { return this.frmGroup.controls; }
  public get documentFormField() { return this.f.DocumentFormField as FormArray };



  constructor(private readonly fb: FormBuilder, private readonly _kycDocumentTypeService: KycDocumentTypeService, private readonly _userSettingService: UserSettingService,
    readonly _commonService: CommonService, private readonly _alertService: AlertService, private readonly _toast: ToastrService) { }

  ngOnInit(): void {

    this.formInt();
    this.getDocumentType();


  }
  ngOnChanges(): void {
    //Called before any other lifecycle hook. Use it to inject dependencies, but avoid any serious work here.
    //Add '${implements OnChanges}' to the class.


    // if (this.documentModel) {
    //   this.documentModel = this.documentModel?.sort(function (a, b) { return a.DocumentTypeId - b.DocumentTypeId });
    // }


    // if (this.kycModel) {
    //   this.kycModel = this.kycModel?.sort(function (a, b) { return a.KycdocumentTypeId - b.KycdocumentTypeId });
    // }

  }

  getDocumentType() {
    this._kycDocumentTypeService.GetDDLDocumentTypeForUserKYC(true).subscribe(res => {
      if (res.IsSuccess) {
        this.docTypeModel = res.Data as DDLDocumentTypeModel[];
        this.docTypeModel = this.docTypeModel?.sort(function (a, b) { return a.Id - b.Id });

        this.docTypeModel!.forEach(x => {
          let existingKYCItem = this.kycModel.findIndex(xi => xi.KycdocumentTypeId == x.Id);
          let existingDocItem = this.documentModel.findIndex(xi => xi.DocumentTypeId == x.Id);

          // Add newly Item in Document
          if (this.kycModel!.length != this.docTypeModel!.length && existingKYCItem < 0) {
            let item = {} as UserKYCPostModel;
            item.KycdocumentTypeId = x.Id;
            this.kycModel.push(item);
          }

          if (this.documentModel!.length != this.docTypeModel!.length && existingDocItem < 0) {
            let item = {} as DocumentPostModel;
            item.DocumentTypeId = x.Id;
            item.Files = [];
            this.documentModel.push(item);
          }

          this.addDocsItemControl(x.DocumentNumberLength, x.IsMandatory);


        });

      }
    });
  }
  formInt() {
    this.frmGroup = this.fb.group({
      DocumentFormField: this.fb.array([])
    });

  }
  onFrmSubmit() {
    this.frmGroup.markAllAsTouched();

    if (this.frmGroup.valid) {
      this.onDocSubmit.emit(this.documentModel);
      this.onKYCSubmit.emit(this.kycModel);
    }
  }
  addDocsItemControl(DocCharLenght: number, IsMandatory: boolean) {
    const docs = this.f.DocumentFormField as FormArray;
    if (IsMandatory) {
      this.f.DocumentFormField.push(

        this.fb.group({

          DocumentNumber: [undefined, Validators.compose([Validators.required, Validators.maxLength(DocCharLenght), Validators.minLength(DocCharLenght)])],
          File: [undefined, Validators.required],
        })
      );
    } else {
      this.f.DocumentFormField.push(

        this.fb.group({

          DocumentNumber: [undefined, Validators.compose([Validators.maxLength(DocCharLenght), Validators.minLength(DocCharLenght)])],
          File: [undefined],
        })
      );
    }


  }
  getDocName(Id: number) {
    return this.docTypeModel.find(x => x.Id == Id)?.Name
  }

  onCheckDocumentNumber(val: any, docTypeId: number) {

    let dataItem = this.docTypeModel.find(x => x.Id == docTypeId) as DDLDocumentTypeModel;

    if (dataItem.IsNumeric) {
      return this._commonService.NumberOnly(val);

    } else {
      return this._commonService.AlphaNumericOnly(val);

    }
  }

  removeDocFile(file: FilePostModel, docId: number, docTypeId: number) {
    this._userSettingService.DeleteDocumentFile(file.Id, docId).subscribe(res => {
      if (res.IsSuccess) {
        this._toast.success('File Removed', 'Success');
        let docIndex = this.documentModel!.findIndex(x => x.DocumentTypeId == docTypeId)
        let fileIndex = this.documentModel![docIndex].Files!.findIndex(x => x.Id == file.Id);
        this.documentModel![docIndex].Files.splice(fileIndex, 1);


      }
    });
  }

  onDocumentAttach(docuemtnTypeId: number, files: FileInfo[]) {

    let docIndex = this.documentModel.findIndex(x => x.DocumentTypeId == docuemtnTypeId);


    if (docIndex >= 0) {
      this.documentModel[docIndex].Files = this.documentModel[docIndex].Files.filter(x => x.Id > 0);
      files.forEach(element => {
        let Indx = this.documentModel[docIndex].Files.findIndex(x => x.File == element.FileBase64);
        if (Indx < 0) {
          let File = {} as FilePostModel;
          File.File = element.FileBase64;
          File.FileName = element.Name;
          File.IsEditMode = false;
          this.documentModel[docIndex].Files.push(File);

        }
      });
    } else {
      let doc = {} as DocumentPostModel;
      doc.Files = [] as FilePostModel[];
      doc.DocumentTypeId = docuemtnTypeId;
      files.forEach(element => {
        let File = {} as FilePostModel;
        File.File = element.FileBase64;
        File.FileName = element.Name;
        File.IsEditMode = false;
        doc.Files.push(File);
      });
      this.documentModel.push(doc);
    }
    return true;
  }



}
