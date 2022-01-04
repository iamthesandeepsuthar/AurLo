import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ProductCategoryEnum } from 'src/app/Shared/Enum/fixed-value';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key } from 'src/app/Shared/Helper/constants';
import { FreshLeadHLPLCLModel } from 'src/app/Shared/Model/Leads/other-loan-leads.model';
import { DDLProductModel } from 'src/app/Shared/Model/master-model/product-model.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { PersonalHomeCarLoanService } from 'src/app/Shared/Services/Leads/personal-home-car-loan.service';
import { ProductService } from 'src/app/Shared/Services/master-services/product.service';
import { UserSettingService } from 'src/app/Shared/Services/user-setting-services/user-setting.service';

@Component({
  selector: 'app-add-fresh-vehicle-lead',
  templateUrl: './add-fresh-vehicle-lead.component.html',
  styleUrls: ['./add-fresh-vehicle-lead.component.scss'],
  providers: [UserSettingService,PersonalHomeCarLoanService,ProductService]
})
export class AddFreshVehicleLeadComponent implements OnInit {

  model!:FreshLeadHLPLCLModel;
  vehicleForm!:FormGroup;
  ddlProductModel!: DDLProductModel[];
  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };
  Id:number=0;
  get Model(): FreshLeadHLPLCLModel { return this.model; }
  get f1() { return this.vehicleForm.controls; }
  get DobMaxDate() {
    var date = new Date();
    date.setFullYear(date.getFullYear() - 18);
    return date
  };
  constructor(private readonly _vehicleService: PersonalHomeCarLoanService,
    readonly _commonService: CommonService,
    private readonly toast: ToastrService,
    private readonly _userSettingService: UserSettingService,
    private readonly fb: FormBuilder,readonly _router: Router,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _productService: ProductService,) {
    this.model= new FreshLeadHLPLCLModel();
  }
  ngOnInit(): void {
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
    let serve = this._productService.GetProductbyCategory(ProductCategoryEnum.GoldLoan).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlProductModel = res?.Data as DDLProductModel[];
      }
    });
  }
  onSubmit():void {
  }
  formInit() {
    this.vehicleForm = this.fb.group({
      Product: [undefined, Validators.required],
      Email: [undefined, Validators.compose([Validators.required, Validators.email])],
      FullName: [undefined, Validators.required],
      FatherName: [undefined, Validators.required], 
      Mobile: [undefined, Validators.required],
      LoanAmount: [undefined, Validators.required],
      LeadType:[undefined],
      AnnualIncome:[undefined],
      Pincode:[undefined],
      AreaPincode:[undefined],
      EmployeeType:[undefined],
      ITRYear:[undefined]
    });
  }
}
