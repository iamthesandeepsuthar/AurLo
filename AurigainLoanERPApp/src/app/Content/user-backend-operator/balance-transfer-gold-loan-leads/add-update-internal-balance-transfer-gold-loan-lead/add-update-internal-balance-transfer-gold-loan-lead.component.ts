import { Component, OnInit } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { ProductCategoryEnum } from "src/app/Shared/Enum/fixed-value";
import { AuthService } from "src/app/Shared/Helper/auth.service";
import { DropDownModel } from "src/app/Shared/Helper/common-model";
import { DropDown_key, Message, Routing_Url } from "src/app/Shared/Helper/constants";
import { BTGoldLoanLeadPostModel, BTGoldLoanLeadViewModel } from "src/app/Shared/Model/Leads/btgold-loan-lead-post-model.model";
import { DDLBranchModel } from "src/app/Shared/Model/master-model/bank-model.model";
import { DDLProductModel } from "src/app/Shared/Model/master-model/product-model.model";
import { AvailableAreaModel } from "src/app/Shared/Model/User-setting-model/user-availibility.model";
import { CommonService } from "src/app/Shared/Services/common.service";
import { BalanceTransferGoldLoanLeadsService } from "src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service";
import { BankBranchService } from "src/app/Shared/Services/master-services/bank-branch.service";
import { JewelleryTypeService } from "src/app/Shared/Services/master-services/jewellery-type.service";
import { KycDocumentTypeService } from "src/app/Shared/Services/master-services/kyc-document-type.service";
import { ProductService } from "src/app/Shared/Services/master-services/product.service";
import { StateDistrictService } from "src/app/Shared/Services/master-services/state-district.service";

@Component({
  selector: 'app-add-update-internal-balance-transfer-gold-loan-lead',
  templateUrl: './add-update-internal-balance-transfer-gold-loan-lead.component.html',
  styleUrls: ['./add-update-internal-balance-transfer-gold-loan-lead.component.scss'],
  providers: [ProductService, KycDocumentTypeService, StateDistrictService, BankBranchService, JewelleryTypeService, BalanceTransferGoldLoanLeadsService]

})
export class AddUpdateInternalBalanceTransferGoldLoanLeadComponent implements OnInit {

  leadId: number = 0;
  model = new BTGoldLoanLeadPostModel();
  maxDate = new Date();
  AeraPincode!: string | any;
  CorrespondAeraPincode!: string | any;
  BankId!: number;
  leadFromPersonalDetail!: FormGroup;
  leadFromAddressDetail!: FormGroup;
  leadFromAppointmentDetail!: FormGroup;
  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };
  ddlProductModel!: DDLProductModel[];
  ddlBranchModel!: DDLBranchModel[];
  ddlAreaModel!: AvailableAreaModel[];
  ddlCorespondAreaModel!: AvailableAreaModel[];
  UserId = 0;
  isSameAddress = false;
  get f1() { return this.leadFromPersonalDetail.controls; }
  get f2() { return this.leadFromAddressDetail.controls; }
  get f3() { return this.leadFromAppointmentDetail.controls; }


  constructor(private readonly fb: FormBuilder, readonly _commonService: CommonService,
    private readonly _productService: ProductService, private readonly _stateDistrictService: StateDistrictService,
    readonly _router: Router, private readonly _bankBranchService: BankBranchService,
    private readonly _activatedRoute: ActivatedRoute, private readonly _auth: AuthService,
    private readonly _balanceTransferService: BalanceTransferGoldLoanLeadsService, private readonly toast: ToastrService) {
    this.UserId = this._auth.GetUserDetail()?.UserId as number;
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
      AccountNumber: [undefined, Validators.required],
      Purpose: [undefined, Validators.required]
    });

    this.leadFromAddressDetail = this.fb.group({

      Pincode: [undefined, Validators.compose([Validators.minLength(6), Validators.maxLength(6)])],
      Area: [undefined],
      Address: [undefined],
      CorrespondPincode: [undefined, Validators.compose([Validators.minLength(6), Validators.maxLength(6)])],
      CorrespondArea: [undefined],
      CorrespondAddress: [undefined],

    });


    this.leadFromAppointmentDetail = this.fb.group({
      Bank: [undefined, undefined],
      Branch: [undefined, undefined],
      DateofAppointment: [undefined, undefined],
      TimeofAppointment: [undefined, undefined],
    });



  }

  onSubmit() {
    this.leadFromPersonalDetail.markAllAsTouched();
    this.leadFromAddressDetail.markAllAsTouched();

    this.leadFromAppointmentDetail.markAllAsTouched();
    if (this.leadFromPersonalDetail.valid && this.leadFromAddressDetail.valid && this.leadFromAppointmentDetail.valid) {

      if (this.UserId > 0) {
        this.model.LeadSourceByuserId = this._auth.GetUserDetail()?.UserId as number;

      }

      if(this.model.LoanAmount){
        this.model.LoanAmount =Number(this.model.LoanAmount);

      }else {
        this.model.LoanAmount =0;
      }

      this._balanceTransferService.AddUpdateInternalLead(this.model).subscribe(res => {
        if (res.IsSuccess) {
          this.toast.success(Message.SaveSuccess);
          this.leadFromPersonalDetail.reset();
          this.leadFromAddressDetail.reset();

          this.leadFromAppointmentDetail.reset();
          this.model = new BTGoldLoanLeadPostModel();
          this._router.navigate([`${Routing_Url.UserBackendOperatorModule}/${Routing_Url.BackEnd_BT_GoldLoan_List_Url}`]);

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

        }
      })
    }
  }
  //#region  <<DropDown>>
  GetDropDowns() {

    this.GetDropDown();
    this.getDDLProducts();
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

  //#endregion

}
