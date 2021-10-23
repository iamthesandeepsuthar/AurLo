import { AlertService } from './../../../../Services/alert.service';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { UserKYCPostModel } from '../../../../Model/doorstep-agent-model/door-step-agent.model';
import { DropDownModel } from '../../../common-model';
import { DropDown_key } from '../../../constants';
import { KycDocumentTypeService } from '../../../../Services/master-services/kyc-document-type.service';
import { DDLDocumentTypeModel, DocumentTypeModel } from 'src/app/Shared/Model/master-model/document-type.model';

@Component({
  selector: 'app-user-kycdetail-section',
  templateUrl: './user-kycdetail-section.component.html',
  styleUrls: ['./user-kycdetail-section.component.scss'],
  providers: [KycDocumentTypeService]
})
export class UserKYCDetailSectionComponent implements OnInit {
  @Input() kycModel = [] as UserKYCPostModel[];
  @Output() onSubmit = new EventEmitter<UserKYCPostModel[]>();

  model: UserKYCPostModel = {} as UserKYCPostModel;

  formGroup = new FormGroup({});
  docTypeModel!: DDLDocumentTypeModel[];
  get f() { return this.formGroup.controls; }
  DocCharLenght=0;
  constructor(private readonly fb: FormBuilder, private readonly _kycDocumentTypeService: KycDocumentTypeService,
    readonly _commonService: CommonService, private readonly _alertService: AlertService) { }

  ngOnInit(): void {
    this.getDocumentType()
  }


  getDocumentType() {
    this._kycDocumentTypeService.GetDDLDocumentType().subscribe(res => {
      if (res.IsSuccess) {

        this.docTypeModel = res.Data as DDLDocumentTypeModel[];

      }
    });
  }

  getDocumentTypeText(value: string | number) {
    return this.docTypeModel?.find(x => x.Id == value)?.Name;
  }
  AddEditKycItem() {

    if (this.model.Kycnumber!.length > 0 && this.model.KycdocumentTypeId > 0) {
      let itm = this.kycModel.findIndex(x => x.KycdocumentTypeId == this.model.KycdocumentTypeId);
      if (itm < 0) {
        this.kycModel.push(this.model);
      } else {
        this._alertService.Warning("Kyc Document Detail Already exist!");
      }
      this.model = {} as UserKYCPostModel;
    }
  }

  fillKycItem(index: number) {
    if (index >= 0) {
      let item = this.kycModel.find((x, i) => i == index) as UserKYCPostModel;
      this.model = {} as UserKYCPostModel;
      this.model.Id = item!.Id;
      this.model.KycdocumentTypeId = item!.KycdocumentTypeId;
      this.model.Kycnumber = item!.Kycnumber;
      this.kycModel.splice(index, 1);
    }
  }
  removeKycItem(index: number) {
    if (index >= 0) {

      this.kycModel.splice(index, 1);
    }
  }

  onFrmSubmit() {
    this.onSubmit.emit(this.kycModel);
  }
  onChangeDocType() {
    this.model.Kycnumber = undefined;
    let dataItem = this.docTypeModel.find(x => x.Id == this.model?.KycdocumentTypeId) as DDLDocumentTypeModel;

    this.DocCharLenght =dataItem.DocumentNumberLength;
  }
  onCheckValidInput(val: any) {
    debugger
    let dataItem = this.docTypeModel.find(x => x.Id == this.model?.KycdocumentTypeId) as DDLDocumentTypeModel;

    if (dataItem.IsNumeric) {
      return this._commonService.NumberOnly(val);

    } else {
      return this._commonService.AlphaNumericOnly(val);

    }
  }


}
