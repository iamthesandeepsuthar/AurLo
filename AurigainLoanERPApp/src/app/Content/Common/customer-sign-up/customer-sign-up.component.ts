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


@Component({
  selector: 'app-customer-sign-up',
  templateUrl: './customer-sign-up.component.html',
  styleUrls: ['./customer-sign-up.component.scss'],
  providers: [CustomerService,KycDocumentTypeService,StateDistrictService]
})
export class CustomerSignUpComponent implements OnInit {
  registrationFrom!: FormGroup;
  documentTypeModel!: DDLDocumentTypeModel[];
  areaModel!: AvailableAreaModel[];
  model!: CustomerRegistrationModel;
  kycDocumentModel!: UserKYCPostModel;
  DocumentCharLength = 0;
  get KycDocumentModel(): UserKYCPostModel {
    return this.kycDocumentModel;
  }
  get Model(): CustomerRegistrationModel{
    return this.model;
  }
  get f() { return this.registrationFrom.controls; }
  get routing_Url() { return Routing_Url }

  constructor(private readonly fb: FormBuilder,private readonly _customerService: CustomerService,
              private readonly _documentType: KycDocumentTypeService,
              private readonly _stateService:StateDistrictService,
              private readonly toast : ToastrService, 
              private _activatedRoute: ActivatedRoute,
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
     if(response.IsSuccess) {
     this.documentTypeModel = response.Data as DDLDocumentTypeModel[];
     } else {
      this.toast.warning(response.Message as string , 'Server Error');
     }
    });
  }
  getAreaByPincode(value: any) {
    let pincode  = value.currentTarget.value;   
    let subscription = this._stateService.GetAreaByPincode(pincode).subscribe(response => {
    subscription.unsubscribe();
    if(response.IsSuccess) {
     this.areaModel = response.Data as AvailableAreaModel[];
    } else {
      this.toast.warning(response.Message as string , 'Server Error');

    }
  });
  }
  formInit() {
    this.registrationFrom = this.fb.group({
    fullName:[undefined],
    fatherName:[undefined],
    email:[undefined],
    mobile:[undefined],
    gender:[undefined],
    dob:[undefined],
    documentType: [undefined],
    documentNumber: [undefined],
    pincode:[undefined],
    address:[undefined],
    area: [undefined],    
    });
  }
  getDocumentTypeText(value: string | number) {
    return this.documentTypeModel?.find(x => x.Id == value)?.Name;
  }
  AddKycDocument(){    
    debugger;
    if(this.kycDocumentModel.KycdocumentTypeId == undefined && this.kycDocumentModel.Kycnumber == undefined) {
      return;
    }
    let exist = this.model.KycDocuments.find(x=>x.KycdocumentTypeId == this.KycDocumentModel.KycdocumentTypeId );
    if(exist) {
      this.toast.warning('Document already added' , 'Duplicate Record');
      return;
    } else {
      this.model.KycDocuments.push(this.kycDocumentModel);
      this.kycDocumentModel = new UserKYCPostModel();
    }    
  }
  RemoveDocument(index: number){    
    this.model.KycDocuments.splice(index,1);
  }
  onChangeDocType(value: number) {
    
    if (value > 0) {
      this.kycDocumentModel.KycdocumentTypeId = value;
    }
    //this.model.Kycnumber = undefined;
    if (this.kycDocumentModel?.KycdocumentTypeId ) {


      let dataItem = this.documentTypeModel?.find(x => x.Id == (this.KycDocumentModel?.KycdocumentTypeId ?? value)) as DDLDocumentTypeModel;
      this.DocumentCharLength = dataItem.DocumentNumberLength;

      if (this.kycDocumentModel?.KycdocumentTypeId> 0) {
        this.registrationFrom.get("documentType")?.setValidators(Validators.required);
        this.registrationFrom.get("KycNumber")?.setValidators(Validators.compose([Validators.required, Validators.maxLength(this.DocumentCharLength), Validators.minLength(this.DocumentCharLength)]));
      }
      else {
        this.registrationFrom.get("documentType")?.setValidators(null);
        this.registrationFrom.get("documentNumber")?.setValidators(null);
      }

    }
    else {
      this.registrationFrom.get("documentType")?.setValidators(null);
      this.registrationFrom.get("documentNumber")?.setValidators(null);
    }

    this.registrationFrom.get("documentType")?.updateValueAndValidity();
    this.registrationFrom.get("documentNumber")?.updateValueAndValidity();
  }
  onSubmit() {   
    if(this.model.KycDocuments.length<1) {
      this.toast.info('Please select kyc document', 'Required');
      return;
    }
    this.registrationFrom.markAllAsTouched();
    if (this.registrationFrom.valid) {    
     let subscription = this._customerService.CustomerRegistration(this.Model).subscribe(response => {
     subscription.unsubscribe();
     if(response.IsSuccess) {
      this.toast.success( 'Registration successful', 'Success');
     // this._router.navigate([this.routing_Url.AdminModule+'/'+this.routing_Url.MasterModule + this.routing_Url.Product_List_Url]);
     } else {
       this.toast.error(response.Message?.toString(), 'Error');
     }
     })
     }
  }
}
