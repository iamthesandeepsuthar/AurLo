import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { UserRoleEnum } from "src/app/Shared/Enum/fixed-value";
import { UserAvailibilityPostModel, AvailableAreaModel, UserAvailabilityViewModel } from "src/app/Shared/Model/User-setting-model/user-availibility.model";
import { AlertService } from "src/app/Shared/Services/alert.service";
import { CommonService } from "src/app/Shared/Services/common.service";
import { DoorStepAgentService } from "src/app/Shared/Services/door-step-agent-services/door-step-agent.service";
import { UserSettingService } from '../../../../Shared/Services/user-setting-services/user-setting.service';
import { UserViewModel } from '../../../../Shared/Model/doorstep-agent-model/door-step-agent.model';
import { Message } from "src/app/Shared/Helper/constants";

@Component({
  selector: 'app-door-step-agent-availability',
  templateUrl: './door-step-agent-availability.component.html',
  styleUrls: ['./door-step-agent-availability.component.scss'],
  providers: [UserSettingService]
})
export class DoorStepAgentAvailabilityComponent implements OnInit {
  userId: number = 0;
  userModel = {} as UserViewModel;
  model = {} as UserAvailibilityPostModel;
  ddlavailibleAreaModel = [] as AvailableAreaModel[];
  dataModel = [] as UserAvailabilityViewModel[];

  userRoleEnum = UserRoleEnum;
  formGroup!: FormGroup;
  get f() { return this.formGroup.controls; }
  get capacityValues() { return Array.from({ length: 10 }, (val, i) => i + 1) }
  PinCode!: string;
  constructor(private readonly _alertService: AlertService, private readonly fb: FormBuilder,
    private readonly _userSettingService: UserSettingService, private _activatedRoute: ActivatedRoute, private _router: Router,
    readonly _commonService: CommonService, private readonly _toast: ToastrService,) {

    this.userId = this._activatedRoute.snapshot.params.userId;
  }

  ngOnInit(): void {
    this.formInit();
    if (this.userId) {
      this.getUserDetail();
    }

  }

  formInit() {
    this.formGroup = this.fb.group({

      MondayST: [undefined, Validators.required],
      MondayET: [undefined, Validators.required],
      TuesdayST: [undefined, Validators.required],
      TuesdayET: [undefined, Validators.required],
      WednesdayST: [undefined, Validators.required],
      WednesdayET: [undefined, Validators.required],
      ThursdayST: [undefined, Validators.required],
      ThursdayET: [undefined, Validators.required],
      FridayST: [undefined, Validators.required],
      FridayET: [undefined, Validators.required],
      SaturdayST: [undefined, Validators.required],
      SaturdayET: [undefined, Validators.required],
      SundayST: [undefined, Validators.required],
      SundayET: [undefined, Validators.required],
      Capacity: [undefined, Validators.required],
      PinCode: [undefined, undefined],
      PincodeAreaId: [undefined, Validators.required]
    });
  }

  getUserDetail() {
    var ser = this._userSettingService.GetUserProfile(this.userId).subscribe(res => {
      ser.unsubscribe();
      if (res.IsSuccess) {
        
        this.userModel = res.Data as UserViewModel;
        this.getUserAvailibiltyList();
      }
    })
  }

  getUserAvailibiltyList() {
    let serv = this._userSettingService.GetUserAvailibilityList(this.userId).subscribe(res => {
      serv.unsubscribe();
      if (res.IsSuccess) {
        debugger
        this.dataModel = res?.Data as UserAvailabilityViewModel[];

      }
    });
  }

  setFieldValidation(startTimeFC: string, endTimeFC: string, setRequired: any) {

    let startTimeField = this.formGroup.get(startTimeFC);
    let endTimeField = this.formGroup.get(endTimeFC);

    if (setRequired.target.checked) {
      startTimeField?.setValidators(null);
      endTimeField?.setValidators(null);
    } else {

      startTimeField?.setValidators(Validators.required);
      endTimeField?.setValidators(Validators.required);
    }


    startTimeField?.updateValueAndValidity();
    endTimeField?.updateValueAndValidity();
  }

  onFrmSubmit() {
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid && this.userId > 0) {
      this.model.UserId = Number(this.userId);
      this.model.Capacity = this.model.Capacity ? Number(this.model.Capacity) : 0;

      this._userSettingService.SetUserAvailibility(this.model).subscribe(res => {
        if (res.IsSuccess) {
          this._toast.success(Message.SaveSuccess, 'Success');
          this.getUserAvailibiltyList();
          this.model = {} as UserAvailibilityPostModel;
          this.formGroup.reset();
        }
      });
    }
  }
  onBindDetail(idx: number) {
    let data = this.dataModel[idx]


    this.model.Id = data.Id
    this.model.UserId = data.UserId
    this.model.MondayST = data.MondaySt;
    this.model.MondayET = data.MondayEt;
    this.model.TuesdayST = data.TuesdaySt;
    this.model.TuesdayET = data.TuesdayEt;
    this.model.WednesdayST = data.WednesdaySt;
    this.model.WednesdayET = data.WednesdayEt;
    this.model.ThursdayST = data.ThursdaySt;
    this.model.ThursdayET = data.ThursdayEt;
    this.model.FridayST = data.FridaySt;
    this.model.FridayET = data.FridayEt;
    this.model.SaturdayST = data.SaturdaySt;
    this.model.SaturdayET = data.SaturdayEt;
    this.model.SundayST = data.SundaySt;
    this.model.SundayET = data.SundayEt;
    this.model.Capacity = data.Capacity as number;
    this.model.PincodeAreaId = data.PincodeAreaId as number;
    this.PinCode = data.PinCode as string;
    this.GetAvailableArea(this.model.Id);

  }
  GetAvailableArea(id = 0) {
    let serve = this._userSettingService.GetAvailableAreaForRolebyPinCode(this.PinCode, UserRoleEnum.DoorStepAgent, id).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlavailibleAreaModel = res.Data as AvailableAreaModel[];
      }
    });
  }

  checkCapacityValue(event: any) {

    if (Number(this.model.Capacity) > 10 || Number(this.model.Capacity) < 0) {
      return false;
    } else {
      return true;
    }


  }
}

