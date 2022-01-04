import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductCategoryEnum } from 'src/app/Shared/Enum/fixed-value';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key } from 'src/app/Shared/Helper/constants';
import { FreshLeadHLPLCLModel } from 'src/app/Shared/Model/Leads/other-loan-leads.model';
import { DDLProductModel } from 'src/app/Shared/Model/master-model/product-model.model';
import { AvailableAreaModel } from 'src/app/Shared/Model/User-setting-model/user-availibility.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { PersonalHomeCarLoanService } from 'src/app/Shared/Services/Leads/personal-home-car-loan.service';
import { ProductService } from 'src/app/Shared/Services/master-services/product.service';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { UserSettingService } from 'src/app/Shared/Services/user-setting-services/user-setting.service';

@Component({
  selector: 'app-add-fresh-personal-lead',
  templateUrl: './add-fresh-personal-lead.component.html',
  styleUrls: ['./add-fresh-personal-lead.component.scss'],
  providers: [UserSettingService,PersonalHomeCarLoanService,ProductService,StateDistrictService]
})
export class AddFreshPersonalLeadComponent implements OnInit {

  model!:FreshLeadHLPLCLModel;
  FormData!:FormGroup;
  ddlProductModel!: DDLProductModel[];
  ddlAreaModel!: AvailableAreaModel[];
  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };
  Id:number=0;
  get Model(): FreshLeadHLPLCLModel { return this.model; }
  get f1() { return this.FormData.controls; }
  get DobMaxDate() {
    var date = new Date();
    date.setFullYear(date.getFullYear() - 18);
    return date
  };
  LeadType = [
    { Id: 0, Name: 'Salaried' },
    { Id: 1, Name: 'SelfEmployed' },
  ];
  ITR=[{Id:1, Name:'1 year'},
  {Id:2, Name:'2 year'},
  {Id:3, Name:'3 year'},
  {Id:4, Name:'4 year'},
  {Id:5, Name:'5 year'}]

  constructor(private readonly _personalService: PersonalHomeCarLoanService,
    readonly _commonService: CommonService,
    private readonly toast: ToastrService,
    private readonly _userSettingService: UserSettingService,
    private readonly fb: FormBuilder,readonly _router: Router,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _productService: ProductService,
    private readonly _stateDistrictService: StateDistrictService,) {
    this.model= new FreshLeadHLPLCLModel();
  }
  ngOnInit(): void {
    this.getDDLProducts();
    this.formInit();
  }
  GetDropDownGender() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlGender]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        let ddls = res?.Data as DropDownModel;
        this.dropDown.ddlGender = ddls?.ddlGender;
      }
    });
  }
  getDDLProducts() {
    let serve = this._productService.GetProductbyCategory(ProductCategoryEnum.PersonalLoan).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlProductModel = res?.Data as DDLProductModel[];
      }
    });
  }
  getDropDownPinCodeArea() {
    let serve = this._stateDistrictService.GetAreaByPincode(this.model.Pincode as string).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlAreaModel = res?.Data as AvailableAreaModel[];
        this.model.AddressLine2 = this.ddlAreaModel[0].AddressLine2;
      }
    })
  }
  onSubmit():void {
    this.FormData.markAllAsTouched();
    if(this.FormData.valid){
    this.model.LoanAmount = Number(this.model.LoanAmount);
    this.model.LeadType = Boolean(this.model.LeadType);
    alert(this.model.LeadType);
     let subscription = this._personalService.AddUpdate(this.model).subscribe( response => {
       subscription.unsubscribe();
       if(response.IsSuccess) {
        this.toast.success(response.Message as string,'Success');
       } else {
        this.toast.error(response.Message as string,'Server Error');
       }
     });
    } else {
      this.toast.warning('Form validation Invalid','Validation');
      return;
    }
  }
  formInit() {
    this.FormData = this.fb.group({
      Product: [undefined, Validators.required],
      Email: [undefined, Validators.compose([Validators.required, Validators.email])],
      FullName: [undefined, Validators.required],
      FatherName: [undefined, Validators.required],
      Mobile: [undefined, Validators.required],
      LoanAmount: [undefined, Validators.required],
      LeadType:[undefined],
      Designation:[undefined],
      AnnualIncome:[undefined],
      Pincode:[undefined],
      AreaPincode:[undefined],
      EmployeeType:[undefined],
      ITRYear:[undefined],
      Gender:[undefined],
      DOB:[undefined],
      Address:[undefined],
      AddressLine2:[undefined],
      ITRNo:[undefined]
    });
  }
}
