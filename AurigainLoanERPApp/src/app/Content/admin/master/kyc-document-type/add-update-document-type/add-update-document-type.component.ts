import { Component, KeyValueDiffers, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { DocumentTypeModel } from 'src/app/Shared/Model/master-model/document-type.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { KycDocumentTypeService } from 'src/app/Shared/Services/master-services/kyc-document-type.service';
@Component({
  selector: 'app-add-update-document-type',
  templateUrl: './add-update-document-type.component.html',
  styleUrls: ['./add-update-document-type.component.scss'],
  providers: [KycDocumentTypeService]
})
export class AddUpdateDocumentTypeComponent implements OnInit {
  Id: number = 0;
  model = new DocumentTypeModel();
  documentTypeForm!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.documentTypeForm.controls; }
  get Model(): DocumentTypeModel { return this.model; }

  constructor(private readonly fb: FormBuilder, readonly _commonService: CommonService,
    private readonly _documentTypeService: KycDocumentTypeService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private readonly toast: ToastrService) {

    if (this.Id == 0) { this.model.IsNumeric = false; }
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
    }
  }

  ngOnInit(): void {
    this.formInit();
    if (this.Id > 0) {
      this.onGetDetail();
    }
  }
  formInit() {
    this.documentTypeForm = this.fb.group({
      Name: [undefined, Validators.required],
      IsActive: [true],
      IsNumeric: [false],
      DocumentNumberLength: [undefined, Validators.required],
      IsKyc: [false],
      IsFreshLeadKyc: [false],
      IsBtleadKyc: [false],
      IsMandatory: [false],

      RequiredFileCount: [undefined, Validators.required]
    });
  }
  onGetDetail() {
    let subscription = this._documentTypeService.GetDocumentType(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
        this.model = res.Data as DocumentTypeModel;
      } else {
        this.toast.warning('Record not found', 'No Record');
      }
    });
  }
  onSubmit() {
    this.documentTypeForm.markAllAsTouched();
    if (this.documentTypeForm.valid) {
      let subscription = this._documentTypeService.AddUpdateDocumentType(this.Model).subscribe(response => {
        subscription.unsubscribe();
        if (response.IsSuccess) {
          this.toast.success(this.Id == 0 ? 'Record save successful' : 'Record update successful', 'Success');
          this._router.navigate([this.routing_Url.AdminModule + '/' + this.routing_Url.MasterModule + this.routing_Url.Kyc_Document_Type_List_Url]);
        } else {
          this.toast.error(response.Message?.toString(), 'Error');
        }
      })
    }
  }

}
