import { ProductService } from 'src/app/Shared/Services/master-services/product.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GoldLoanFreshLeadJewelleryDetailModel, GoldLoanFreshLeadModel } from 'src/app/Shared/Model/Leads/gold-loan-fresh-lead.model';
import { DDLProductModel, ProductModel } from '../../../../../Shared/Model/master-model/product-model.model';
import { DDLDocumentTypeModel } from '../../../../../Shared/Model/master-model/document-type.model';
import { DDLJewellaryType } from '../../../../../Shared/Model/master-model/jewellary-type-model.model';
import { DDLBranchModel } from '../../../../../Shared/Model/master-model/bank-model.model';
import { CommonService } from '../../../../../Shared/Services/common.service';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key, Message } from 'src/app/Shared/Helper/constants';
import { ProductCategoryEnum } from 'src/app/Shared/Enum/fixed-value';
import { KycDocumentTypeService } from 'src/app/Shared/Services/master-services/kyc-document-type.service';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { AvailableAreaModel } from 'src/app/Shared/Model/User-setting-model/user-availibility.model';
import { BankBranchService } from '../../../../../Shared/Services/master-services/bank-branch.service';
import { JewelleryTypeService } from 'src/app/Shared/Services/master-services/jewellery-type.service';
import { AuthService } from '../../../../../Shared/Helper/auth.service';
import { GoldLoanLeadsService } from 'src/app/Shared/Services/Leads/gold-loan-leads.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-add-edit-gold-loan-fresh-lead',
  templateUrl: './add-edit-gold-loan-fresh-lead.component.html',
  styleUrls: ['./add-edit-gold-loan-fresh-lead.component.scss'],
  providers: [ProductService, KycDocumentTypeService, StateDistrictService, BankBranchService, JewelleryTypeService, GoldLoanLeadsService]
})
export class AddEditGoldLoanFreshLeadComponent implements OnInit {
  leadId: number = 0;
  model = new GoldLoanFreshLeadModel();
  JewelleryModel = new GoldLoanFreshLeadJewelleryDetailModel();

  get DobMaxDate() {
    var date = new Date();
    date.setFullYear(date.getFullYear() - 18);
    return date
  };
  leadFromPersonalDetail!: FormGroup;
  leadFromDocumentDetail!: FormGroup;
  leadFromJewelleryDetail!: FormGroup;
  leadFromAppointmentDetail!: FormGroup;
  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };
  ddlProductModel!: DDLProductModel[];
  ddlDocumentTypeModel!: DDLDocumentTypeModel[];
  ddlBranchModel!: DDLBranchModel[];
  ddlAreaModel!: AvailableAreaModel[];
  ddlJewellaryType!: DDLJewellaryType[];
  ddlKarats = [{ Name: "18 Karats", Id: 18 }, { Name: "20  Karats", Id: 20 }, { Name: "22  Karats", Id: 22 }, { Name: "24  Karats", Id: 24 }];
  UserId = 0;
  // PinCode: string | any;

  get f1() { return this.leadFromPersonalDetail.controls; }
  get f2() { return this.leadFromDocumentDetail.controls; }
  get f3() { return this.leadFromJewelleryDetail.controls; }
  get f4() { return this.leadFromAppointmentDetail.controls; }

  get docMaxChar() { return this.ddlDocumentTypeModel?.find(x => x.Id == this.model?.KycDocument?.KycDocumentTypeId)?.DocumentNumberLength ?? 0 }

  constructor(private readonly fb: FormBuilder, readonly _commonService: CommonService,
    private readonly _productService: ProductService, private readonly _kycDocumentTypeService: KycDocumentTypeService,
    private readonly _stateDistrictService: StateDistrictService, readonly _router: Router,
    private readonly _bankBranchService: BankBranchService, private readonly _activatedRoute: ActivatedRoute,
    private readonly _jewelleryTypeService: JewelleryTypeService, private readonly _auth: AuthService,
    private readonly _goldLoanLeadService: GoldLoanLeadsService, private readonly toast: ToastrService) {
    this.UserId = this._auth.GetUserDetail()?.UserId as number;
  }

  ngOnInit(): void {
    this.formInit();
    this.GetDropDowns();


    if (this._activatedRoute.snapshot.params.id) {
      this.leadId = this._activatedRoute.snapshot.params.id;
      this.onGetDetail();
    }
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
      Purpose: [undefined, Validators.required],
      Tenure: [undefined, undefined]
    });

    this.leadFromDocumentDetail = this.fb.group({
      DocumentType: [undefined],
      DocumentNumber: [undefined],
      PanNumber: [undefined, Validators.compose([Validators.minLength(10), Validators.maxLength(10)])],
      Pincode: [undefined, Validators.compose([Validators.minLength(6), Validators.maxLength(6)])],
      Aarea: [undefined],
    });

    this.leadFromJewelleryDetail = this.fb.group({
      JewelleryType: [undefined, undefined],
      Quantity: [undefined, undefined],
      Weight: [undefined, undefined],
      Karats: [undefined, undefined],

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
    if (this.leadFromPersonalDetail.valid && this.leadFromDocumentDetail.valid && this.leadFromJewelleryDetail.valid && this.leadFromAppointmentDetail.valid) {

      if (this.model.Id == undefined || this.model.Id == 0) {
        this.model.IsActive = true;
      }
      if (this.UserId > 0) {
        this.model.CustomerUserId = this._auth.GetUserDetail()?.UserId as number;

      }
      this.model.LoanAmountRequired = this.model.LoanAmountRequired ? Number(this.model.LoanAmountRequired) : null;
      this.model.PreferredLoanTenure = this.model.PreferredLoanTenure ? Number(this.model.PreferredLoanTenure) : null;

      this.model.JewelleryDetail.forEach(element => {
        element.Quantity = element.Quantity ? Number(element.Quantity) : null;
        element.Karat = element.Karat ? Number(element.Karat) : null;
        element.Weight = element.Weight ? Number(element.Weight) : null;

      });
      this._goldLoanLeadService.AddUpdate(this.model).subscribe(res => {
        if (res.IsSuccess) {
          this.toast.success(Message.SaveSuccess);
          this.leadFromPersonalDetail.reset();
          this.leadFromDocumentDetail.reset();
          this.leadFromJewelleryDetail.reset();
          this.leadFromAppointmentDetail.reset();
          this.model = new GoldLoanFreshLeadModel();
          this._router.navigate(['/user-customer/gold-loan-leads'])

        } else {
          this.toast.success(Message.SaveFail);

        }
      });
    }
  }

  onGetDetail() {
    if (this.leadId > 0) {


      let serve = this._goldLoanLeadService.GetById(this.leadId).subscribe(res => {

        serve.unsubscribe();
        if (res.IsSuccess) {
          this.model = res.Data as GoldLoanFreshLeadModel;

          if (this.model.KycDocument.KycDocumentTypeId) {
            this.onChangeDocument(this.model.KycDocument.KycDocumentTypeId);
          }


          if (this.model.KycDocument.Pincode) {
            this.onChangePinCode();
          }
        }
      })
    }
  }
  //#region  <<DropDown>>
  GetDropDowns() {
    this.getDDLDocumentType();
    this.GetDropDownGender();
    this.getDDLProducts();
    this.GetDDLJewelleryType();
  }

  getDDLProducts() {
    let serve = this._productService.GetProductbyCategory(ProductCategoryEnum.GoldLoan).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlProductModel = res?.Data as DDLProductModel[];
      }
    });
  }

  getDDLDocumentType() {
    let serve = this._kycDocumentTypeService.GetDDLDocumentTypeForFreshLeadKYC(true).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlDocumentTypeModel = res?.Data as DDLDocumentTypeModel[];
      }
    });
  }

  GetDropDownGender() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlGender]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        let ddls = res?.Data as DropDownModel;
        this.dropDown.ddlState = ddls?.ddlState;
        this.dropDown.ddlQualification = ddls?.ddlQualification;
        this.dropDown.ddlGender = ddls?.ddlGender;
      }
    });
  }

  getDropDownBranch() {
    let serve = this._bankBranchService.GetBranchesbyPinCode(this.model.KycDocument.Pincode).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlBranchModel = res?.Data as DDLBranchModel[];
      }
    });
  }

  getDropDownPinCodeArea() {

    let serve = this._stateDistrictService.GetAreaByPincode(this.model.KycDocument.Pincode as string).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlAreaModel = res?.Data as AvailableAreaModel[];
      }
    })

  }

  GetDDLJewelleryType() {
    let serve = this._jewelleryTypeService.GetDDLJewelleryType().subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlJewellaryType = res.Data as DDLJewellaryType[];
      }
    });

  }


  onChangeDocument(value: number) {

    let doc = this.ddlDocumentTypeModel?.find(x => x.Id == value);
    this.f2.DocumentNumber.setValidators(Validators.compose([Validators.minLength(doc?.DocumentNumberLength as number), Validators.maxLength(doc?.DocumentNumberLength as number)]));
    this.f2.DocumentNumber.updateValueAndValidity();
  }

  onCheckDocumentNumber(val: any) {

    let dataItem = this.ddlDocumentTypeModel?.find(x => x.Id == this.model.KycDocument.KycDocumentTypeId) as DDLDocumentTypeModel;

    if (dataItem.IsNumeric) {
      return this._commonService.NumberOnly(val);

    } else {
      return this._commonService.AlphaNumericOnly(val);

    }
  }

  onChangePinCode() {
    if (this.model.KycDocument.Pincode) {
      this.getDropDownPinCodeArea();
      this.getDropDownBranch();
    }
  }


  onGetJewellaryTypeName(value: number) {
    return this.ddlJewellaryType.find(x => x.Id == value)?.Name;

  }
  onJewellaryAdd() {
    this.UpdateJewellaryValidation(true);
    this.leadFromJewelleryDetail.markAllAsTouched();
    if (this.leadFromJewelleryDetail.valid) {
      this.model.JewelleryDetail.push(this.JewelleryModel);
      this.UpdateJewellaryValidation(false);

    }
  }
  onJewellaryEdit(idx: number) {
    this.UpdateJewellaryValidation(true);
    this.JewelleryModel = this.model.JewelleryDetail[idx];
    this.model.JewelleryDetail.splice(idx, 1);


  }
  onJewellaryDelete(idx: number) {
    this.model.JewelleryDetail.splice(idx, 1);
  }
  onJewellaryAddCancel() {
    if (this.JewelleryModel.JewelleryTypeId && this.JewelleryModel.Karat && this.JewelleryModel.Quantity && this.JewelleryModel.Weight) {
      this.model.JewelleryDetail.push(this.JewelleryModel);
      this.JewelleryModel = new GoldLoanFreshLeadJewelleryDetailModel();
    }
    this.UpdateJewellaryValidation(false);

  }
  UpdateJewellaryValidation(enable = false) {
    if (enable) {
      this.f3.JewelleryType.setValidators([Validators.required]);
      this.f3.Quantity.setValidators([Validators.required]);
      this.f3.Weight.setValidators([Validators.required]);
      this.f3.Karats.setValidators([Validators.required]);
    } else {
      this.f3.JewelleryType.removeValidators([Validators.required]);
      this.f3.Quantity.removeValidators([Validators.required]);
      this.f3.Weight.removeValidators([Validators.required]);
      this.f3.Karats.removeValidators([Validators.required]);
    }
    this.f3.JewelleryType.updateValueAndValidity();
    this.f3.Quantity.updateValueAndValidity();
    this.f3.Weight.updateValueAndValidity();
    this.f3.Karats.updateValueAndValidity();
  }
  //#endregion

}
