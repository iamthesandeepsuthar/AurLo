import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';
import { DDLDocumentTypeModel, DocumentTypeModel } from '../../Model/master-model/document-type.model';

@Injectable()
export class KycDocumentTypeService {
  constructor(private readonly _baseService: BaseAPIService) { }
  GetDocumentTypeList(model: IndexModel): Observable<ApiResponse<DocumentTypeModel[]>> {
    let url = `${this._baseService.API_Url.Kyc_Document_Type_List_Api}`;
    return this._baseService.post(url, model);
  }
  GetDocumentType(id: number): Observable<ApiResponse<DocumentTypeModel>> {
    let url = `${this._baseService.API_Url.Kyc_Document_Type_Detail_Api}${id}`;
    return this._baseService.get(url);
  }
  GetDDLDocumentType(): Observable<ApiResponse<DDLDocumentTypeModel>> {
    let url = `${this._baseService.API_Url.Kyc_Document_Type_Dropdown_Api}`;
    return this._baseService.get(url);
  }
  AddUpdateDocumentType(model: DocumentTypeModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.Kyc_Document_Type_Add_Update_Api}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id:number, status? :string): Observable<ApiResponse<DocumentTypeModel[]>> {
    let url = `${this._baseService.API_Url.Kyc_Document_Type_Active_Status_Api}${id}`;
    return this._baseService.get(url);
  }
  DeleteDocumentType(id: number): Observable<ApiResponse<DocumentTypeModel[]>> {
    let url = `${this._baseService.API_Url.Kyc_Document_Type_Delete_Api}${id}/${status}`;
    return this._baseService.Delete(url);
  }
}
