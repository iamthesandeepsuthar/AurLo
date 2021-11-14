import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';
import { ProductModel } from '../../Model/master-model/product-model.model';

@Injectable()
export class ProductService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetProductList(model: IndexModel): Observable<ApiResponse<ProductModel[]>> {
    let url = `${this._baseService.API_Url.Product_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetProductById(id: number): Observable<ApiResponse<ProductModel>> {
    let url = `${this._baseService.API_Url.Product_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdateProduct(model: ProductModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Product_AddUpdate_Api}`;

    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<ProductModel[]>> {
    let url = `${this._baseService.API_Url.Product_ActiveStatus_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteProduct(id: number): Observable<ApiResponse<ProductModel[]>> {
    let url = `${this._baseService.API_Url.Product_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }

  GetProductbyCategory(id: number): Observable<ApiResponse<ProductModel[]>> {
    let url = `${this._baseService.API_Url.Product_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
}
