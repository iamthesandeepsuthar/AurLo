import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserAvailibilityPostModel } from 'src/app/Shared/Model/User-setting-model/user-availibility.model';
import { AlertService } from 'src/app/Shared/Services/alert.service';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { DoorStepAgentService } from 'src/app/Shared/Services/door-step-agent-services/door-step-agent.service';

@Component({
  selector: 'app-door-step-agent-availability',
  templateUrl: './door-step-agent-availability.component.html',
  styleUrls: ['./door-step-agent-availability.component.scss'],
  providers: [DoorStepAgentService]
})
export class DoorStepAgentAvailabilityComponent implements OnInit {
  model = {} as UserAvailibilityPostModel;
  constructor(private readonly _alertService: AlertService, private readonly fb: FormBuilder,
    private readonly _userDoorStepService: DoorStepAgentService, private _activatedRoute: ActivatedRoute, private _router: Router,
    readonly _commonService: CommonService, private readonly _toast: ToastrService,) { }

  ngOnInit(): void {
  }

  SetUserAvailibility() {

    this._userDoorStepService.SetUserAvailibility(this.model).subscribe(res => {
      if (res.IsSuccess) {

      }
    });
  }

  al(){
    debugger
    console.log(this.model)
  }
}
