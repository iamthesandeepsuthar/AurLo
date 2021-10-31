import { AvailableAreaModel } from 'src/app/Shared/Model/User-setting-model/user-availibility.model';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { DDLDocumentTypeModel } from 'src/app/Shared/Model/master-model/document-type.model';
import { ToastrService } from 'ngx-toastr';
import { KycDocumentTypeService } from 'src/app/Shared/Services/master-services/kyc-document-type.service';
import { Component, OnInit } from '@angular/core';
import { CustomerService } from 'src/app/Shared/Services/CustomerService/customer.service';

@Component({
  selector: 'app-customer-sign-up',
  templateUrl: './customer-sign-up.component.html',
  styleUrls: ['./customer-sign-up.component.scss'],
  providers: [CustomerService,KycDocumentTypeService,StateDistrictService]
})
export class CustomerSignUpComponent implements OnInit {

  documentTypeModel!: DDLDocumentTypeModel[];
  areaModel!: AvailableAreaModel[];

  constructor(private readonly _customerService: CustomerService,
              private readonly _documentType: KycDocumentTypeService,
              private readonly _stateService:StateDistrictService,
              private readonly toast : ToastrService) {  }

  ngOnInit(): void {
    this.getDocumentType();
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
  getAreaByPincode(pincode: string) {
  let subscription = this._stateService.GetAreaByPincode(pincode).subscribe(response => {
    subscription.unsubscribe();
    if(response.IsSuccess) {
     this.areaModel = response.Data as AvailableAreaModel[];
    } else {
      this.toast.warning(response.Message as string , 'Server Error');

    }
  });
  }

}
