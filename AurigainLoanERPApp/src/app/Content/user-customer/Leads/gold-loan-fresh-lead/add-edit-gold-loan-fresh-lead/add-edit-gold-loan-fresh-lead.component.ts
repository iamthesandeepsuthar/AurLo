import { ProductService } from 'src/app/Shared/Services/master-services/product.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GoldLoanFreshLeadModel } from 'src/app/Shared/Model/Leads/gold-loan-fresh-lead.model';
import { DDLProductModel, ProductModel } from '../../../../../Shared/Model/master-model/product-model.model';
import { DDLDocumentTypeModel } from '../../../../../Shared/Model/master-model/document-type.model';
import { DDLJewellaryType } from '../../../../../Shared/Model/master-model/jewellary-type-model.model';
import { DDLBranchModel } from '../../../../../Shared/Model/master-model/bank-model.model';
import { CommonService } from '../../../../../Shared/Services/common.service';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key } from 'src/app/Shared/Helper/constants';
import { ProductCategoryEnum } from 'src/app/Shared/Enum/fixed-value';
import { KycDocumentTypeService } from 'src/app/Shared/Services/master-services/kyc-document-type.service';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { AvailableAreaModel } from 'src/app/Shared/Model/User-setting-model/user-availibility.model';

@Component({
  selector: 'app-add-edit-gold-loan-fresh-lead',
  templateUrl: './add-edit-gold-loan-fresh-lead.component.html',
  styleUrls: ['./add-edit-gold-loan-fresh-lead.component.scss'],
  providers: [ProductService, KycDocumentTypeService, StateDistrictService]
})
export class AddEditGoldLoanFreshLeadComponent implements OnInit {
  model = new GoldLoanFreshLeadModel();
  maxDate = new Date();
  leadFromPersonalDetail!: FormGroup;
  leadFromDocumentDetail!: FormGroup;
  leadFromJewelleryDetail!: FormGroup;
  leadFromAppointmentDetail!: FormGroup;
  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };

  ddlProductModel!: DDLProductModel[];
  ddlDocumentTypeModel!: DDLDocumentTypeModel[];
  ddlJewelleryType!: DDLJewellaryType[];
  ddlBranchModel!: DDLBranchModel[];
  ddlAreaModel!: AvailableAreaModel[];
  PinCode!: string;

  get f1() { return this.leadFromPersonalDetail.controls; }
  get f2() { return this.leadFromDocumentDetail.controls; }
  get f3() { return this.leadFromJewelleryDetail.controls; }
  get f4() { return this.leadFromAppointmentDetail.controls; }
  get docMaxChar() { return this.ddlDocumentTypeModel.find(x => x.Id == this.model?.KycDocument?.KycDocumentTypeId)?.DocumentNumberLength ?? 0 }

  constructor(private readonly fb: FormBuilder, readonly _commonService: CommonService,
    private readonly _productService: ProductService, private readonly _kycDocumentTypeService: KycDocumentTypeService,
    private readonly _stateDistrictService: StateDistrictService
  ) {

  }

  ngOnInit(): void {
    this.formInit();
    this.GetDropDown();
    this.getDDLProducts();
    this.getDDLDocumentType();
  }

  formInit() {
    this.leadFromPersonalDetail = this.fb.group({
      Product: [undefined, Validators.required],
      Email: [undefined, Validators.compose([Validators.required, Validators.email])],
      FullName: [undefined, Validators.required],
      FatherName: [undefined, Validators.required],
      DOB: [undefined, Validators.required],
      Gender: [undefined, Validators.required],
      Mobile: [undefined, Validators.required],
      AlternativeMobile: [undefined, Validators.required],
      Amount: [undefined, Validators.required],
      Purpose: [undefined, Validators.required]
    });

    this.leadFromDocumentDetail = this.fb.group({
      DocumentType: [undefined, undefined],
      DocumentNumber: [undefined, undefined],
      PanNumber: [undefined, undefined],
      Pincode: [undefined, undefined],
      Aarea: [undefined, undefined],
    });

    this.leadFromJewelleryDetail = this.fb.group({
      JewelleryType: [undefined, undefined],
      Quantity: [undefined, undefined],
      Weight: [undefined, undefined],
      Karats: [undefined, undefined],
      Tenure: [undefined, undefined]
    });

    this.leadFromAppointmentDetail = this.fb.group({
      Branch: [undefined, undefined],
      DateofAppointment: [undefined, undefined],
      TimeofAppointment: [undefined, undefined],
    });



  }
  onSubmit() {
    this.leadFromPersonalDetail.markAllAsTouched();
    this.leadFromDocumentDetail.markAllAsTouched();
    this.leadFromJewelleryDetail.markAllAsTouched();
    this.leadFromAppointmentDetail.markAllAsTouched();
  }
  getDDLProducts() {
    this._productService.GetProductbyCategory(ProductCategoryEnum.GoldLoan).subscribe(res => {
      if (res.IsSuccess) {
        this.ddlProductModel = res.Data as DDLProductModel[];

      }

    });
  }
  getDDLDocumentType() {
    debugger
    this._kycDocumentTypeService.GetDDLDocumentType(true).subscribe(res => {
      if (res.IsSuccess) {
        debugger
        this.ddlDocumentTypeModel = res.Data as DDLDocumentTypeModel[];
      }
    });
  }

  GetDropDown() {
    this._commonService.GetDropDown([DropDown_key.ddlGender]).subscribe(res => {
      if (res.IsSuccess) {
        let ddls = res.Data as DropDownModel;
        this.dropDown.ddlState = ddls.ddlState;
        this.dropDown.ddlQualification = ddls.ddlQualification;
        this.dropDown.ddlGender = ddls.ddlGender;
      }
    });
  }

  onChangeProduct(value: number) {

  }
  onChangeDocument(vale: number) { }


  onCheckDocumentNumber(val: any) {

    let dataItem = this.ddlDocumentTypeModel.find(x => x.Id == this.model.KycDocument.KycDocumentTypeId) as DDLDocumentTypeModel;

    if (dataItem.IsNumeric) {
      return this._commonService.NumberOnly(val);

    } else {
      return this._commonService.AlphaNumericOnly(val);

    }
  }

  onChangePinCode() {
    // this.PinCode
    this.getPinCodeArea();
    this.getBranch();
  }
  getBranch() { }
  getPinCodeArea() {
    this._stateDistrictService.GetAreaByPincode(this.PinCode).subscribe(res => {
      if (res.IsSuccess) {
        this.ddlAreaModel = res.Data as AvailableAreaModel[];
      }
    })

  }
}
