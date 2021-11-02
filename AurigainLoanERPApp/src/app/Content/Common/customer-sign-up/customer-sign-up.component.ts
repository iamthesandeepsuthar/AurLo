import { AvailableAreaModel } from 'src/app/Shared/Model/User-setting-model/user-availibility.model';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { DDLDocumentTypeModel } from 'src/app/Shared/Model/master-model/document-type.model';
import { ToastrService } from 'ngx-toastr';
import { KycDocumentTypeService } from 'src/app/Shared/Services/master-services/kyc-document-type.service';
import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/Shared/Services/CustomerService/customer.service';
import { CustomerRegistrationModel } from 'src/app/Shared/Model/CustomerModel/customer-registration-model.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { UserKYCPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';
import { CommonService } from 'src/app/Shared/Services/common.service';


@Component({
  selector: 'app-customer-sign-up',
  templateUrl: './customer-sign-up.component.html',
  styleUrls: ['./customer-sign-up.component.scss'],
  providers: [CustomerService, KycDocumentTypeService, StateDistrictService]
})
export class CustomerSignUpComponent implements OnInit {

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
  }

  ngOnInit(): void {
    this.getDocumentType();
    this.formInit();
  }
  getDocumentType() {
    let subscription = this._documentType.GetDDLDocumentType().subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        debugger
        this.documentTypeModel = response.Data as DDLDocumentTypeModel[];
      } else {
        this.toast.warning(response.Message as string, 'Server Error');
      }
    });
  }
  getAreaByPincode(value: any) {
    let pincode = value.currentTarget.value;
    let subscription = this._stateService.GetAreaByPincode(pincode).subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        this.areaModel = response.Data as AvailableAreaModel[];
      } else {
        this.toast.warning(response.Message as string, 'Server Error');

      }
    });
  }
  formInit() {
    this.registrationFromStepOne = this.fb.group({
      fullName: [undefined, Validators.required],
      fatherName: [undefined, Validators.required],
      email: [undefined, Validators.required],
      mobile: [undefined, Validators.required],
      gender: [undefined, undefined],
      dob: [undefined,undefined],

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
    //this.model.Kycnumber = undefined;
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
          this._router.navigate([this.routing_Url.LoginUrl]);
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
