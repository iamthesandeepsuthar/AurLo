import { DDLBankModel } from './../../../../../Shared/Model/master-model/bank-model.model';
import { BankBranchService } from './../../../../../Shared/Services/master-services/bank-branch.service';
import { ProductCategoryService } from './../../../../../Shared/Services/master-services/product-category.service';
import { ProductService } from './../../../../../Shared/Services/master-services/product.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ProductModel } from 'src/app/Shared/Model/master-model/product-model.model';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DllProductCategoryModel } from 'src/app/Shared/Model/master-model/product-category-model.model';
import { validateHorizontalPosition } from '@angular/cdk/overlay';

@Component({
  selector: 'app-add-update-product',
  templateUrl: './add-update-product.component.html',
  styleUrls: ['./add-update-product.component.scss'],
  providers:[ProductService,ProductCategoryService,BankBranchService]
})
export class AddUpdateProductComponent implements OnInit {
  Id: number = 0;
  model = new ProductModel();
  DDLProductCategory!: DllProductCategoryModel[];
  DDLBankModel!: DDLBankModel[];
  productFrom!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.productFrom.controls; }
  get Model(): ProductModel{  return this.model; }

  constructor(private readonly fb: FormBuilder,
              private readonly _categoryService: ProductCategoryService,
              private readonly _productService: ProductService,
              private readonly _bankService: BankBranchService,
              private _activatedRoute: ActivatedRoute,
              private _router: Router,
              private readonly toast: ToastrService) {
if (this._activatedRoute.snapshot.params.id) {
this.Id = this._activatedRoute.snapshot.params.id;
}
  }

 ngOnInit(): void {
  this.formInit();
  this.getBanks();
  this.getProductCategory();
  if (this.Id > 0) {
  this.onGetDetail();
  }
  }
  formInit() {
  this.productFrom = this.fb.group({
  productName: [undefined,Validators.required],
  IsActive: [undefined],
  productCategory: [undefined ,Validators.required],
  bank:[undefined, Validators.required],
  note: [undefined],
  minAmount: [undefined,Validators.required],
  maxAmount:[undefined ,Validators.required],
  interestRate:[undefined ,Validators.required],
  processingFee:[undefined, Validators.required],
  minTenure:[undefined ,Validators.required],
  maxTenure:[undefined,Validators.required],
  isInterest:[undefined]
  });
  }
   getProductCategory() {
    let subscription = this._categoryService.GetDDLProductCategory().subscribe(response => {
      subscription.unsubscribe();
      if(response.IsSuccess) {
       this.DDLProductCategory = response.Data as DllProductCategoryModel[];
      } else {
      this.toast.warning(response.Message as string, 'Server Error');
      }
    });
   }
   getBanks() {
    let subscription = this._bankService.GetDDLBanks().subscribe(response => {
      subscription.unsubscribe();
      if(response.IsSuccess) {
       this.DDLBankModel = response.Data as DDLBankModel[];
      } else {
      this.toast.warning(response.Message as string, 'Server Error');
      }
    });
   }
  onGetDetail() {
  let subscription = this._productService.GetProductById(this.Id).subscribe(res => {
  subscription.unsubscribe();
  if (res.IsSuccess) {
  this.model = res.Data as ProductModel;
 // this.model.IsActive = Boolean (this.model.IsActive );
 // this.model.InterestRateApplied = Boolean(this.model.InterestRateApplied);
  } else {
  this.toast.warning('Record not found', 'No Record');
  }
  });
  }
  onSubmit() {
 this.productFrom.markAllAsTouched();
 if (this.productFrom.valid) {
  this.model.IsActive = Boolean (this.model.IsActive );
  this.model.InterestRateApplied = Boolean(this.model.InterestRateApplied);
  let subscription = this._productService.AddUpdateProduct(this.Model).subscribe(response => {
  subscription.unsubscribe();
  if(response.IsSuccess) {
   this.toast.success( this.Id ==0 ?'Record save successful' : 'Record update successful' , 'Success');
   this._router.navigate([this.routing_Url.AdminModule+'/'+this.routing_Url.MasterModule + this.routing_Url.Product_List_Url]);
  } else {
    this.toast.error(response.Message?.toString(), 'Error');
  }
  })
  }
  }

}
