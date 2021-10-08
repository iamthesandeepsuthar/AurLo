import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseAPIService } from '../../Helper/base-api.service';
import { ApiResponse, IndexModel } from '../../Helper/common-model';
import { DoorStepAgentPostModel, DoorStepAgentViewModel } from '../../Model/doorstep-agent-model/door-step-agent.model';
import { UserRoleModel } from '../../Model/master-model/user-role.model';

@Injectable()
export class DoorStepAgentService {

  constructor(private readonly _baseService: BaseAPIService) { }


  GetDoorStepAgentList(model: IndexModel): Observable<ApiResponse<DoorStepAgentViewModel[]>> {
    let url = `${this._baseService.API_Url.DoorstepAgentListApi}`;
    return this._baseService.post(url, model);
  }

  GetDoorStepAgent(id: number): Observable<ApiResponse<DoorStepAgentViewModel>> {
    let url = `${this._baseService.API_Url.DoorstepAgentDetailApi}${id}`;
    return this._baseService.get(url);
  }

  AddUpdateDoorStepAgent(model: DoorStepAgentPostModel): Observable<ApiResponse<string>> {
    let url = `${this._baseService.API_Url.DoorstepAgentAddUpdateApi}`;
    return this._baseService.post(url, model);
  }

  ChangeActiveStatus(id: number, status?: string): Observable<ApiResponse<DoorStepAgentViewModel[]>> {
    let url = `${this._baseService.API_Url.DoorstepAgentActiveStatusApi}${id}`;
    return this._baseService.get(url);
  }
  DeleteDoorStepAgent(id: number): Observable<ApiResponse<DoorStepAgentViewModel[]>> {
    let url = `${this._baseService.API_Url.DoorstepAgentDeleteApi}${id}`;
    return this._baseService.Delete(url);
  }
}