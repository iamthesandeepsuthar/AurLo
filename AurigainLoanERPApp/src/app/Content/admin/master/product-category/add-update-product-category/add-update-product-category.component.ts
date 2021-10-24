import { ProductCategoryService } from './../../../../../Shared/Services/master-services/product-category.service';
import { Component, OnInit } from '@angular/core';
import { ProductCategoryModel } from 'src/app/Shared/Model/master-model/product-category-model.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-update-product-category',
  templateUrl: './add-update-product-category.component.html',
  styleUrls: ['./add-update-product-category.component.scss'],
  providers: [ProductCategoryService]
})
export class AddUpdateProductCategoryComponent implements OnInit {
  Id: number = 0;
  model = new ProductCategoryModel();
  categoryFrom!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.categoryFrom.controls; }
  get Model(): ProductCategoryModel{  return this.model; }

  constructor(private readonly fb: FormBuilder,
    private readonly _categoryService: ProductCategoryService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private readonly toast: ToastrService) {
      this.model.IsActive = true;
if (this._activatedRoute.snapshot.params.id) {
this.Id = this._activatedRoute.snapshot.params.id;
}
}

ngOnInit(): void {
this.formInit();
if (this.Id > 0) {
this.onGetDetail();
}
}

formInit() {
this.categoryFrom = this.fb.group({
Name: [undefined, Validators.required],
IsActive: [true, Validators.required],
Note: [undefined]
});
}
onGetDetail() {
let subscription = this._categoryService.ProductCategoryById(this.Id).subscribe(res => {
subscription.unsubscribe();
if (res.IsSuccess) {
this.model = res.Data as ProductCategoryModel;
} else {
this.toast.warning('Record not found', 'No Record');
}
});
}
onSubmit() {
this.categoryFrom.markAllAsTouched();
if (this.categoryFrom.valid) {
let subscription = this._categoryService.AddUpdateProductCategory(this.Model).subscribe(response => {
subscription.unsubscribe();
if(response.IsSuccess) {
 this.toast.success( this.Id ==0 ?'Record save successful' : 'Record update successful' , 'Success');
 this._router.navigate([this.routing_Url.AdminModule+'/'+this.routing_Url.MasterModule + this.routing_Url.Product_Category_List_Url]);
} else {
  this.toast.error(response.Message?.toString(), 'Error');
}
})
}
}

}
