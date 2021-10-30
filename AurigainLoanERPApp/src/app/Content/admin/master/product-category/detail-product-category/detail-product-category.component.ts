import { ProductCategoryService } from './../../../../../Shared/Services/master-services/product-category.service';
import { ProductCategoryModel } from './../../../../../Shared/Model/master-model/product-category-model.model';
import { Component, OnInit } from '@angular/core';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-detail-product-category',
  templateUrl: './detail-product-category.component.html',
  styleUrls: ['./detail-product-category.component.scss'],
  providers: [ProductCategoryService]
})
export class DetailProductCategoryComponent implements OnInit {

  Id: number = 0;
  model = new ProductCategoryModel();
  get routing_Url() { return Routing_Url }
  get Model(): ProductCategoryModel{  return this.model; }

  constructor(private readonly _categoryService: ProductCategoryService,
              private _activatedRoute: ActivatedRoute ,
              private readonly toast: ToastrService) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
    }
  }
  ngOnInit(): void {
    this.onGetDetail();
  }
  onGetDetail() {
    let subscription = this._categoryService.ProductCategoryById (this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as ProductCategoryModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }

}
