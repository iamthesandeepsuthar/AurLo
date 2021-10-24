import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { DllProductCategoryModel, ProductCategoryModel } from './../../Model/master-model/product-category-model.model';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse } from '../../Helper/common-model';

@Injectable()
export class ProductCategoryService {

  constructor(private readonly _baseService: BaseAPIService) { }

  GetProductCategoryList(model: IndexModel): Observable<ApiResponse<ProductCategoryModel[]>> {
    let url = `${this._baseService.API_Url.Product_Category_List_Api}`;
    return this._baseService.post(url, model);
  }
  ProductCategoryById(id: number): Observable<ApiResponse<ProductCategoryModel>> {
    let url = `${this._baseService.API_Url.Product_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  AddUpdateProductCategory(model: ProductCategoryModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Product_Category_AddUpdate_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<ProductCategoryModel[]>> {
    let url = `${this._baseService.API_Url.Qualification_Active_Status_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteProductCategory(id: number): Observable<ApiResponse<ProductCategoryModel[]>> {
    let url = `${this._baseService.API_Url.Product_Category_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
  GetDDLProductCategory(): Observable<ApiResponse<DllProductCategoryModel[]>> {
    let url = `${this._baseService.API_Url.Product_Category_Dropdown_List_Api}`;
    return this._baseService.get(url);
  }
}
