import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';
import { AgentListModel, AgentPostModel } from '../../Model/Agent/agent.model';
import { UserAvailibilityPostModel } from '../../Model/User-setting-model/user-availibility.model';

@Injectable( )
export class AgentService {


  constructor(private readonly _baseService: BaseAPIService) { }


  GetAgentList(model: IndexModel): Observable<ApiResponse<AgentListModel[]>> {
    let url = `${this._baseService.API_Url.AgentListApi}`;
    return this._baseService.post(url, model);
  }
  ChangeActiveStatus(id: number, status?: string): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.AgentActiveStatusApi}${id}`;
    return this._baseService.get(url);
  }
  DeleteAgent(id: number): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.AgentDeleteApi}${id}`;
    return this._baseService.Delete(url);
  }


  AddUpdateDoorStepAgent(model: AgentPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.AgentAddUpdateApi}`;
    return this._baseService.post(url, model);
  }



  DeleteDocumentFile(id: number,documentId:number): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.DoorstepAgentDeleteDocumentFileApi}${id}/${documentId}`;
    return this._baseService.Delete(url);
  }

  SetUserAvailibility(model: UserAvailibilityPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.DoorstepAgentAvailibilityApi}`;
    return this._baseService.post(url, model);
  }


}
