import { ProductService } from 'src/app/Shared/Services/master-services/product.service';
import { Component, OnInit } from '@angular/core';
import { ProductModel } from 'src/app/Shared/Model/master-model/product-model.model';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-detail-product',
  templateUrl: './detail-product.component.html',
  styleUrls: ['./detail-product.component.scss'],
  providers:[ProductService]
})
export class DetailProductComponent implements OnInit {
  Id: number = 0;
  model = new ProductModel();
  get routing_Url() { return Routing_Url }
  get Model(): ProductModel{  return this.model; }

  constructor(private readonly _productService: ProductService,
              private _activatedRoute: ActivatedRoute ,
              private readonly toast: ToastrService) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
      this.onGetDetail();
    }
  }
  ngOnInit(): void {
    this.onGetDetail();
  }
  onGetDetail() {
    let subscription = this._productService.GetProductById(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as ProductModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }

}
