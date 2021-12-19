import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserRoleEnum, ProductCategoryEnum } from 'src/app/Shared/Enum/fixed-value';
import { AuthService } from 'src/app/Shared/Helper/auth.service';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key, Message, Routing_Url } from 'src/app/Shared/Helper/constants';
import { BTGoldLoanLeadPostModel, BTGoldLoanLeadViewModel } from 'src/app/Shared/Model/Leads/btgold-loan-lead-post-model.model';
import { DDLBranchModel } from 'src/app/Shared/Model/master-model/bank-model.model';
import { DDLDocumentTypeModel } from 'src/app/Shared/Model/master-model/document-type.model';
import { DDLJewellaryType } from 'src/app/Shared/Model/master-model/jewellary-type-model.model';
import { DDLProductModel } from 'src/app/Shared/Model/master-model/product-model.model';
import { AvailableAreaModel } from 'src/app/Shared/Model/User-setting-model/user-availibility.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { BalanceTransferGoldLoanLeadsService } from 'src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service';
import { BankBranchService } from 'src/app/Shared/Services/master-services/bank-branch.service';
import { JewelleryTypeService } from 'src/app/Shared/Services/master-services/jewellery-type.service';
import { KycDocumentTypeService } from 'src/app/Shared/Services/master-services/kyc-document-type.service';
import { ProductService } from 'src/app/Shared/Services/master-services/product.service';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';

@Component({
  selector: 'app-add-update-internal-balance-transfer-gold-loan-lead',
  templateUrl: './add-update-internal-balance-transfer-gold-loan-lead.component.html',
  styleUrls: ['./add-update-internal-balance-transfer-gold-loan-lead.component.scss'],
  providers: [ProductService, KycDocumentTypeService,
    StateDistrictService, BankBranchService,
    JewelleryTypeService,
    BalanceTransferGoldLoanLeadsService]

})
export class AddUpdateInternalBalanceTransferGoldLoanLeadComponent implements OnInit {


  leadId: number = 0;
  model = new BTGoldLoanLeadPostModel();
  maxDate = new Date();
  AeraPincode!: string | any;
  CorrespondAeraPincode!: string | any;
  BankId!: number;
  leadFormPersonalDetail!: FormGroup;
  leadFormAddressDetail!: FormGroup;
  leadFormAppointmentDetail!: FormGroup;
  leadFormJewelleryDetail!: FormGroup;
  // leadFormDocumentDetail!: FormGroup;
  leadFormExistingLoanDetail!: FormGroup;
  leadFormKYCDetail!: FormGroup;

  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };
  ddlProductModel!: DDLProductModel[];
  ddlBranchModel!: DDLBranchModel[];
  ddlAreaModel!: AvailableAreaModel[];
  ddlCorespondAreaModel!: AvailableAreaModel[];
  ddlDocumentType!: DDLDocumentTypeModel[];
  ddlDocumentTypePOI!: DDLDocumentTypeModel[];
  ddlDocumentTypePOA!: DDLDocumentTypeModel[];
  isSameAddress = false;
  ddlJewellaryType!: DDLJewellaryType[];
  ddlKarats = [{ Name: "18 Karats", Id: 18 }, { Name: "20  Karats", Id: 20 }, { Name: "22  Karats", Id: 22 }, { Name: "24  Karats", Id: 24 }];
  docPOIMaxChar: number = 0;
  docPOAMaxChar: number = 0;

  get f1() { return this.leadFormPersonalDetail.controls; }
  get f2() { return this.leadFormAddressDetail.controls; }
  get f3() { return this.leadFormAppointmentDetail.controls; }
  get f4() { return this.leadFormJewelleryDetail.controls; }
  //get f5() { return this.leadFormDocumentDetail.controls; }
  get f6() { return this.leadFormExistingLoanDetail.controls; }
  get f7() { return this.leadFormKYCDetail.controls; }

  get userDetail() { return this._auth.GetUserDetail() };

  constructor(private readonly fb: FormBuilder, readonly _commonService: CommonService,
    private readonly _productService: ProductService,
    private readonly _stateDistrictService: StateDistrictService,
    readonly _router: Router, private readonly _bankBranchService: BankBranchService,
    private readonly _activatedRoute: ActivatedRoute, private readonly _auth: AuthService,
    private readonly _balanceTransferService: BalanceTransferGoldLoanLeadsService,
    private readonly _jewelleryTypeService: JewelleryTypeService,
    private readonly _kycDocumentTypeService: KycDocumentTypeService,
    private readonly toast: ToastrService) {
  }

  ngOnInit(): void {
    this.formInit();
    this.GetDropDowns();
    if (this._activatedRoute.snapshot?.params?.id) {
      this.leadId = this._activatedRoute.snapshot.params.id;
      this.onGetDetail();
    }
  }
  formInit() {
    this.leadFormPersonalDetail = this.fb.group({
      Product: [undefined, Validators.required],
      Email: [undefined, Validators.compose([Validators.required, Validators.email])],
      FullName: [undefined, Validators.required],
      FatherName: [undefined, Validators.required],
      DOB: [undefined, Validators.required],
      Gender: [undefined, Validators.required],
      Mobile: [undefined, Validators.required],
      AlternativeMobile: [undefined, Validators.required],
      Amount: [undefined, Validators.required],
      AccountNumber: [undefined, Validators.required],
      Purpose: [undefined, Validators.required]
    });

    this.leadFormAddressDetail = this.fb.group({

      Pincode: [undefined, Validators.compose([Validators.minLength(6), Validators.maxLength(6)])],
      Area: [undefined],
      Address: [undefined],
      CorrespondPincode: [undefined, Validators.compose([Validators.minLength(6), Validators.maxLength(6)])],
      CorrespondArea: [undefined],
      CorrespondAddress: [undefined],

    });

    this.leadFormJewelleryDetail = this.fb.group({
      JewelleryType: [undefined, undefined],
      Quantity: [undefined, undefined],
      Weight: [undefined, undefined],
      Karats: [undefined, undefined],

    });


    this.leadFormAppointmentDetail = this.fb.group({
      Bank: [undefined, undefined],
      Branch: [undefined, undefined],
      DateofAppointment: [undefined, undefined],
      TimeofAppointment: [undefined, undefined],
    });


    this.leadFormKYCDetail = this.fb.group({
      PoidocumentType: [undefined],
      PoidocumentNumber: [undefined],
      PoadocumentType: [undefined],
      PoadocumentNumber: [undefined],
      PanNumber: [undefined, Validators.compose([Validators.minLength(10), Validators.maxLength(10)])],

    });


    this.leadFormExistingLoanDetail = this.fb.group({

      BankName: [undefined],
      Amount: [undefined],
      Date: [undefined],
      JewelleryValuation: [undefined],
      OutstandingAmount: [undefined],
      BalanceTransferAmount: [undefined],
      RequiredAmount: [undefined],
      Tenure: [undefined],

    });

    //jwelarry --done
    //kyc/
    //existitng loan

    // document upload


  }

  onSubmit() {
    debugger
    this.leadFormPersonalDetail.markAllAsTouched();
    this.leadFormAddressDetail.markAllAsTouched();
    this.leadFormAppointmentDetail.markAllAsTouched();
    this.leadFormJewelleryDetail.markAllAsTouched()
    this.leadFormKYCDetail.markAllAsTouched()
    //this.leadFormDocumentDetail.markAllAsTouched();
    this.leadFormExistingLoanDetail.markAllAsTouched();

    this.model.LeadSourceByuserId = this._auth.GetUserDetail()?.UserId as number;
    //&& this.leadFormDocumentDetail.valid
    if (this.leadFormPersonalDetail.valid && this.leadFormAddressDetail.valid && this.leadFormAppointmentDetail.valid
      && this.leadFormJewelleryDetail.valid && this.leadFormKYCDetail.valid
      && this.leadFormExistingLoanDetail.valid) {

      if (this.userDetail?.RoleId == UserRoleEnum.Operator || this.userDetail?.RoleId == UserRoleEnum.Agent || this.userDetail?.RoleId == UserRoleEnum.DoorStepAgent) {
        this.model.LeadSourceByuserId = this.userDetail.UserId as number;

      }
      else if (this.userDetail?.RoleId == UserRoleEnum.Customer) {
        this.model.CustomerUserId = this.userDetail.UserId as number;

      }

      if (this.model.LoanAmount) {
        this.model.LoanAmount = Number(this.model.LoanAmount);

      } else {
        this.model.LoanAmount = 0;
      }

      if (this.model.ExistingLoanDetail.Amount) {
        this.model.ExistingLoanDetail.Amount = Number(this.model.ExistingLoanDetail.Amount);

      } else {
        this.model.ExistingLoanDetail.Amount = 0;
      }

      if(this.model.ExistingLoanDetail.JewelleryValuation)
      {
        this.model.ExistingLoanDetail.JewelleryValuation=Number(this.model.ExistingLoanDetail.JewelleryValuation);
      }
      if(this.model.ExistingLoanDetail.OutstandingAmount)
      {
        this.model.ExistingLoanDetail.OutstandingAmount=Number(this.model.ExistingLoanDetail.OutstandingAmount);
      }
      if(this.model.ExistingLoanDetail.BalanceTransferAmount)
      {
        this.model.ExistingLoanDetail.BalanceTransferAmount=Number(this.model.ExistingLoanDetail.BalanceTransferAmount);
      }
      if(this.model.ExistingLoanDetail.RequiredAmount)
      {
        this.model.ExistingLoanDetail.RequiredAmount=Number(this.model.ExistingLoanDetail.RequiredAmount);
      }
      if(this.model.ExistingLoanDetail.Tenure)
      {
        this.model.ExistingLoanDetail.Tenure=Number(this.model.ExistingLoanDetail.Tenure);
      }

      this._balanceTransferService.AddUpdateInternalLead(this.model).subscribe(res => {
        if (res.IsSuccess) {
          this.toast.success(Message.SaveSuccess);

          this._router.navigate([`${Routing_Url.Lead_Module}/${Routing_Url.BT_GoldLoan_List_Url}`]);

        } else {
          this.toast.success(Message.SaveFail);

        }
      });
    }

  }
  onGetDetail() {
    if (this.leadId > 0) {
      let serve = this._balanceTransferService.GetById(this.leadId).subscribe(res => {
        serve.unsubscribe();
        if (res.IsSuccess) {
          let viewData = res.Data as BTGoldLoanLeadViewModel;
          console.log(viewData);

        }
      })
    }
  }

  //#region  <<DropDown>>
  GetDropDowns() {

    this.GetDropDown();
    this.getDDLProducts();
    this.GetDDLJewelleryType();
    this.getDDLDocumentType();
  }
  getDDLProducts() {
    let serve = this._productService.GetProductbyCategory(ProductCategoryEnum.GoldLoan).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlProductModel = res?.Data as DDLProductModel[];
      }
    });
  }
  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlGender, DropDown_key.ddlBank]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        let ddls = res?.Data as DropDownModel;
        this.dropDown.ddlBank = ddls?.ddlBank ?? [];
        this.dropDown.ddlGender = ddls?.ddlGender ?? [];
      }
    });
  }
  getDropDownBranch() {
    this.ddlBranchModel = [];
    let serve = this._bankBranchService.GetBranchesbyBankId(this.BankId.toString()).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlBranchModel = res?.Data as DDLBranchModel[];
      }
    });
  }
  GetDDLJewelleryType() {
    let serve = this._jewelleryTypeService.GetDDLJewelleryType().subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlJewellaryType = res.Data as DDLJewellaryType[];
      }
    });

  }

  getDropDownPinCodeArea(isCorrespond: boolean = false, isRemoveValue = false) {
    let pinCode: string = isCorrespond ? this.CorrespondAeraPincode : this.AeraPincode;

    if (isCorrespond) {
      this.ddlCorespondAreaModel = [];
      this.model.AddressDetail.CorrespondAeraPincodeId = isRemoveValue ? null : this.model.AddressDetail.CorrespondAeraPincodeId;
    } else {
      this.ddlAreaModel = [];
      this.model.AddressDetail.AeraPincodeId = isRemoveValue ? null : this.model.AddressDetail.AeraPincodeId;

    }

    let serve = this._stateDistrictService.GetAreaByPincode(pinCode).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {

        if (isCorrespond) {
          this.ddlCorespondAreaModel = res?.Data as AvailableAreaModel[];
        } else {
          this.ddlAreaModel = res?.Data as AvailableAreaModel[];
        }



      }
    })

  }
  onChangePinCode(PinCode: any) {
    if (this.model.AddressDetail) {
      this.getDropDownPinCodeArea();
      this.getDropDownBranch();
    }
  }
  onSameAddressAssign(eve: any) {

    if (eve.target.checked) {
      this.CorrespondAeraPincode = this.AeraPincode;
      this.model.AddressDetail.CorrespondAddress = this.model.AddressDetail.Address;
      this.getDropDownPinCodeArea(true);
      this.model.AddressDetail.CorrespondAeraPincodeId = this.model.AddressDetail.AeraPincodeId;

    }

  }

  getDDLDocumentType() {
    let serve = this._kycDocumentTypeService.GetDDLDocumentType().subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlDocumentType = res?.Data as DDLDocumentTypeModel[];
        this.ddlDocumentTypePOI = res?.Data as DDLDocumentTypeModel[];
        this.ddlDocumentTypePOA = res?.Data as DDLDocumentTypeModel[];


        this.ddlDocumentTypePOI = this.ddlDocumentTypePOI?.filter(x => this.model?.KYCDetail?.PoadocumentTypeId ? x.Id != this.model?.KYCDetail?.PoadocumentTypeId : true);
        this.ddlDocumentTypePOA = this.ddlDocumentTypePOA?.filter(x => this.model?.KYCDetail?.PoidocumentTypeId ? x.Id != this.model?.KYCDetail?.PoidocumentTypeId : true);


      }
    });
  }

  onCheckDocumentNumberPOI(eve: any, typeId: number) {

    let dataItem = this.ddlDocumentTypePOI?.find(x => x.Id == typeId) as DDLDocumentTypeModel;

    if (dataItem.IsNumeric) {
      return this._commonService.NumberOnly(eve);

    } else {
      return this._commonService.AlphaNumericOnly(eve);

    }
  }

  onCheckDocumentNumberPOA(eve: any, typeId: number) {

    let dataItem = this.ddlDocumentTypePOA?.find(x => x.Id == typeId) as DDLDocumentTypeModel;

    if (dataItem.IsNumeric) {
      return this._commonService.NumberOnly(eve);

    } else {
      return this._commonService.AlphaNumericOnly(eve);

    }
  }

  onChangePOIDocument(value: any) {

    let doc = this.ddlDocumentTypePOI?.find(x => x.Id == value.Id);
    this.f7.PoidocumentNumber.setValidators(Validators.compose([Validators.minLength(doc?.DocumentNumberLength as number), Validators.maxLength(doc?.DocumentNumberLength as number)]));
    this.f7.PoidocumentNumber.updateValueAndValidity();

    this.docPOIMaxChar = doc?.DocumentNumberLength ?? this.docPOIMaxChar;

    this.ddlDocumentTypePOA = this.ddlDocumentType?.filter(x => this.model?.KYCDetail?.PoidocumentTypeId ? x.Id != this.model?.KYCDetail?.PoidocumentTypeId : true);

  }

  onChangePOADocument(value: any) {

    let doc = this.ddlDocumentTypePOA?.find(x => x.Id == value.Id);
    this.f7.PoadocumentNumber.setValidators(Validators.compose([Validators.minLength(doc?.DocumentNumberLength as number), Validators.maxLength(doc?.DocumentNumberLength as number)]));
    this.f7.PoadocumentNumber.updateValueAndValidity();
    this.docPOAMaxChar = doc?.DocumentNumberLength ?? this.docPOAMaxChar;
    this.ddlDocumentTypePOI = this.ddlDocumentType?.filter(x => this.model?.KYCDetail?.PoadocumentTypeId ? x.Id != this.model?.KYCDetail?.PoadocumentTypeId : true);

  }




  //#endregion

}
