
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { CustomerRegistrationModel } from 'src/app/Shared/Model/CustomerModel/customer-registration-model.model';
import { UserKYCPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';
import { DDLDocumentTypeModel } from 'src/app/Shared/Model/master-model/document-type.model';
import { AvailableAreaModel } from 'src/app/Shared/Model/User-setting-model/user-availibility.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { CustomerService } from 'src/app/Shared/Services/CustomerService/customer.service';
import { KycDocumentTypeService } from 'src/app/Shared/Services/master-services/kyc-document-type.service';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';

@Component({
  selector: 'app-update-customer-profile',
  templateUrl: './update-customer-profile.component.html',
  styleUrls: ['./update-customer-profile.component.scss'],
  providers: [CustomerService, KycDocumentTypeService, StateDistrictService]

})
export class UpdateCustomerProfileComponent implements OnInit {

  userId!: number;
  registrationFromStepOne!: FormGroup;
  registrationFromStepTwo!: FormGroup;
  registrationFromStepThird!: FormGroup;
  documentTypeModel!: DDLDocumentTypeModel[];
  areaModel!: AvailableAreaModel[];
  model!: CustomerRegistrationModel;
  kycDocumentModel!: UserKYCPostModel;
  DocumentCharLength = 0;
  get KycDocumentModel(): UserKYCPostModel {
    return this.kycDocumentModel;
  }
  get Model(): CustomerRegistrationModel {
    return this.model;
  }
  get f1() { return this.registrationFromStepOne.controls; }
  get f2() { return this.registrationFromStepTwo.controls; }
  get f3() { return this.registrationFromStepThird.controls; }

  get routing_Url() { return Routing_Url }
  get DobMaxDate() {
    var date = new Date();
    date.setFullYear(date.getFullYear() - 18);
    return date
  };
  constructor(private readonly fb: FormBuilder, private readonly _customerService: CustomerService,
    private readonly _documentType: KycDocumentTypeService,
    private readonly _stateService: StateDistrictService,
    private readonly toast: ToastrService,
    private _activatedRoute: ActivatedRoute,
    readonly _commonService: CommonService,
    private _router: Router) {
    this.model = new CustomerRegistrationModel();
    this.model.IsActive = true;
    this.kycDocumentModel = new UserKYCPostModel();
    if (this._activatedRoute.snapshot.params.id) {
      this.userId = this._activatedRoute.snapshot.params.id;
      this.getUserDetail();
    }
  }
  ngOnInit(): void {
    this.getDocumentType();
    this.formInit();
  }
  getUserDetail() {
    let serve = this._customerService.GetCustomerGetById(this.userId).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.model = res.Data as CustomerRegistrationModel;

        if (this.model.DateOfBirth) {
          this.model.DateOfBirth = new Date(this.model.DateOfBirth as Date);
        }
        if (this.model.PinCode) {
          this.getAreaByPincode(this.model.PinCode);
        }

      }
      else {
        this._router.navigate([`/${Routing_Url.CustomersModule}`])
      }
    });

  }
  getDocumentType() {
    this.documentTypeModel = [];
    let subscription = this._documentType.GetDDLDocumentType().subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        this.documentTypeModel = response.Data as DDLDocumentTypeModel[];
      } else {
        this.toast.warning(response.Message as string, 'Server Error');
      }
    });
  }
  getAreaByPincode(value: any) {
    // this.areaModel = [];
    if (value) {
      debugger
      let subscription = this._stateService.GetAreaByPincode(value).subscribe(response => {
        subscription.unsubscribe();
        debugger
        if (response.IsSuccess) {
          this.areaModel = response.Data as AvailableAreaModel[];
        } else {
          this.toast.warning(response.Message as string, 'Server Error');
        }
        if (!this.areaModel || this.areaModel.length == 0) {
          this.model.PincodeAreaId = null;
         }
      });
    }
  }
  formInit() {
    this.registrationFromStepOne = this.fb.group({
      fullName: [undefined, Validators.required],
      fatherName: [undefined, Validators.required],
      email: [undefined, Validators.required],
      mobile: [undefined, Validators.required],
      gender: [undefined, undefined],
      dob: [undefined, undefined],

    });
    this.registrationFromStepTwo = this.fb.group({
      documentType: [undefined],
      documentNumber: [undefined],
    });
    this.registrationFromStepThird = this.fb.group({
      pincode: [undefined],
      address: [undefined],
      area: [undefined, Validators.required],
    });



  }
  getDocumentTypeText(value: string | number) {
    return this.documentTypeModel?.find(x => x.Id == value)?.Name;
  }

  AddKycDocument() {
    if (this.kycDocumentModel.KycdocumentTypeId == undefined && this.kycDocumentModel.Kycnumber == undefined) {
      return;
    }
    let exist = this.model.KycDocuments.find(x => x.KycdocumentTypeId == this.KycDocumentModel.KycdocumentTypeId);
    if (exist) {
      this.toast.warning('Document already added', 'Duplicate Record');
      return;
    } else {
      this.model.KycDocuments.push(this.kycDocumentModel);
      this.kycDocumentModel = new UserKYCPostModel();
    }
  }
  RemoveDocument(index: number) {
    this.model.KycDocuments.splice(index, 1);
  }
  onChangeDocType(value: number) {

    if (value > 0) {
      this.kycDocumentModel.KycdocumentTypeId = value;
    }
    if (this.kycDocumentModel?.KycdocumentTypeId) {
      let dataItem = this.documentTypeModel?.find(x => x.Id == (this.KycDocumentModel?.KycdocumentTypeId ?? value)) as DDLDocumentTypeModel;
      this.DocumentCharLength = dataItem.DocumentNumberLength;

      if (this.kycDocumentModel?.KycdocumentTypeId > 0) {
        this.registrationFromStepTwo.get("documentType")?.setValidators(Validators.required);
        this.registrationFromStepTwo.get("KycNumber")?.setValidators(Validators.compose([Validators.required, Validators.maxLength(this.DocumentCharLength), Validators.minLength(this.DocumentCharLength)]));
      }
      else {
        this.registrationFromStepTwo.get("documentType")?.setValidators(null);
        this.registrationFromStepTwo.get("documentNumber")?.setValidators(null);
      }
    }
    else {
      this.registrationFromStepTwo.get("documentType")?.setValidators(null);
      this.registrationFromStepTwo.get("documentNumber")?.setValidators(null);
    }

    this.registrationFromStepTwo.get("documentType")?.updateValueAndValidity();
    this.registrationFromStepTwo.get("documentNumber")?.updateValueAndValidity();
  }
  onSubmit() {
    if (this.model.KycDocuments.length < 1) {
      this.toast.info('Please select kyc document', 'Required');
      return;
    }

    this.registrationFromStepOne.markAllAsTouched();
    this.registrationFromStepTwo.markAllAsTouched();
    this.registrationFromStepThird.markAllAsTouched();
    if (this.registrationFromStepOne.valid && this.registrationFromStepTwo.valid && this.registrationFromStepThird.valid) {
      let subscription = this._customerService.CustomerRegistration(this.Model).subscribe(response => {
        subscription.unsubscribe();
        if (response.IsSuccess) {
          this.toast.success('Registration successful , Check you email where we share login credential', 'Success');
          this._router.navigate([this.routing_Url.UserCustomerModule]);
        } else {
          this.toast.error(response.Message?.toString(), 'Error');
        }
      })
    }
  }

  onCheckValidInput(val: any) {
    let dataItem = this.documentTypeModel.find(x => x.Id == this.KycDocumentModel.KycdocumentTypeId) as DDLDocumentTypeModel;
    if (dataItem.IsNumeric) {
      return this._commonService.NumberOnly(val);
    } else {
      return this._commonService.AlphaNumericOnly(val);
    }
  }
}
