import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { DropDownModol } from "src/app/Shared/Helper/common-model";
import { DropDown_key, Routing_Url } from "src/app/Shared/Helper/constants";
import { DoorStepAgentPostModel } from "src/app/Shared/Model/doorstep-agent-model/door-step-agent.model";
import { AlertService } from "src/app/Shared/Services/alert.service";
import { CommonService } from "src/app/Shared/Services/common.service";
import { DoorStepAgentService } from '../../../../Shared/Services/door-step-agent-services/door-step-agent.service';
import { DropDownItem, FilterDropDownPostModel } from '../../../../Shared/Helper/common-model';
import { UserPostModel } from '../../../../Shared/Model/doorstep-agent-model/door-step-agent.model';

@Component({
  selector: 'app-door-step-agent-registration',
  templateUrl: './door-step-agent-registration.component.html',
  styleUrls: ['./door-step-agent-registration.component.scss'],
  providers: [DoorStepAgentService]
})
export class DoorStepAgentRegistrationComponent implements OnInit {
  Id: number = 0;

  model: DoorStepAgentPostModel = {} as DoorStepAgentPostModel;

  formGroup!: FormGroup;
  dropDown = new DropDownModol();
  get ddlkeys() { return DropDown_key };
  get f() { return this.formGroup.controls; }
  get routing_Url() { return Routing_Url }


  constructor(private readonly _alertService: AlertService, private readonly fb: FormBuilder,
    private readonly _userDoorStepService: DoorStepAgentService, private _activatedRoute: ActivatedRoute, private _router: Router,
    private readonly _commonService: CommonService, private readonly _toast: ToastrService) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
    }
    this.model.User = {} as UserPostModel;
    this.dropDown.ddlParentUserRole

  }

  ngOnInit(): void {
    this.GetDropDown()
  }


  GetDropDown() {
    this._commonService.GetDropDown([DropDown_key.ddlQualification, DropDown_key.ddlState]).subscribe(res => {
      if (res.IsSuccess) {
        let ddls = res.Data as DropDownModol;
        this.dropDown.ddlState = ddls.ddlState;
        this.dropDown.ddlQualification = ddls.ddlQualification;


      }
    });
  }

  GetFilterDropDown(key: string, FilterFrom: string, Values: any) {
    debugger
    let model = {
      Key: key,
      FileterFromKey: FilterFrom,
      Values: [Values],

    } as FilterDropDownPostModel;
    this._commonService.GetFilterDropDown(model).subscribe(res => {
      if (res.IsSuccess) {
        let ddls = res.Data as DropDownModol;
        this.dropDown.ddlDistrict = ddls.ddlDistrict;


      }
    });
  }



}
