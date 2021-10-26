import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { UserRoleEnum } from "src/app/Shared/Enum/fixed-value";
import { UserAvailibilityPostModel, AvailableAreaModel } from "src/app/Shared/Model/User-setting-model/user-availibility.model";
import { AlertService } from "src/app/Shared/Services/alert.service";
import { CommonService } from "src/app/Shared/Services/common.service";
import { DoorStepAgentService } from "src/app/Shared/Services/door-step-agent-services/door-step-agent.service";
import { UserSettingService } from '../../../../Shared/Services/user-setting-services/user-setting.service';

@Component({
  selector: 'app-door-step-agent-availability',
  templateUrl: './door-step-agent-availability.component.html',
  styleUrls: ['./door-step-agent-availability.component.scss'],
  providers: [UserSettingService]
})
export class DoorStepAgentAvailabilityComponent implements OnInit {
  model = {} as UserAvailibilityPostModel;
  availibleAreaModel = [] as AvailableAreaModel[];
  userRoleEnum = UserRoleEnum;
  formGroup!: FormGroup;
  get f() { return this.formGroup.controls; }
  get capacityValues() { return Array.from({ length: 10 }, (val, i) => i + 1) }
  PinCode!: string;
  constructor(private readonly _alertService: AlertService, private readonly fb: FormBuilder,
    private readonly _userSettingService: UserSettingService, private _activatedRoute: ActivatedRoute, private _router: Router,
    readonly _commonService: CommonService, private readonly _toast: ToastrService,) { }

  ngOnInit(): void {
    this.formInit();

  }

  formInit() {
    this.formGroup = this.fb.group({

      MondayST: [undefined, undefined],
      MondayET: [undefined, undefined],
      TuesdayST: [undefined, undefined],
      TuesdayET: [undefined, undefined],
      WednesdayST: [undefined, undefined],
      WednesdayET: [undefined, undefined],
      ThursdayST: [undefined, undefined],
      ThursdayET: [undefined, undefined],
      FridayST: [undefined, undefined],
      FridayET: [undefined, undefined],
      SaturdayST: [undefined, undefined],
      SaturdayET: [undefined, undefined],
      SundayST: [undefined, undefined],
      SundayET: [undefined, undefined],
      Capacity: [undefined, undefined],
      PinCode: [undefined, undefined],
      PincodeAreaId: [undefined, undefined]
    });
  }

  setFieldValidation(startTimeFC: string, endTimeFC: string, setRequired: any) {
    debugger
    let startTimeField = this.formGroup.get(startTimeFC);
    let endTimeField = this.formGroup.get(endTimeFC);

    if (setRequired.target.checked) {
      startTimeField?.setValidators(Validators.required);
      endTimeField?.setValidators(Validators.required);
    } else {
      startTimeField?.setValidators(null);
      endTimeField?.setValidators(null);
    }


    startTimeField?.updateValueAndValidity();
    endTimeField?.updateValueAndValidity();
  }

  onFrmSubmit() {
    debugger
    this.formGroup.markAllAsTouched();
    if (this.formGroup.valid) {
      
      this._userSettingService.SetUserAvailibility(this.model).subscribe(res => {
        if (res.IsSuccess) {

        }
      });
    }
  }

  GetAvailableArea() {
    let serve = this._userSettingService.GetAvailableAreaForRolebyPinCode(this.PinCode, UserRoleEnum.DoorStepAgent).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.availibleAreaModel = res.Data as AvailableAreaModel[];
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
