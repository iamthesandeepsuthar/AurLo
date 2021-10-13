import { DocumentViewModel, DoorStepAgentViewModel, UserBankDetailViewModel, UserKYCViewModel, UserNomineeViewModel, UserSecurityDepositViewModel } from './../../../../../Shared/Model/doorstep-agent-model/door-step-agent.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DoorStepAgentService } from 'src/app/Shared/Services/door-step-agent-services/door-step-agent.service';
import { UserViewModel } from '../../../../../Shared/Model/doorstep-agent-model/door-step-agent.model';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-detail-door-step-agent',
  templateUrl: './detail-door-step-agent.component.html',
  styleUrls: ['./detail-door-step-agent.component.scss'],
  providers: [DoorStepAgentService]
})
export class DetailDoorStepAgentComponent implements OnInit {
  //#region <<Variable>>
  Id: number = 0;
  model = {} as DoorStepAgentViewModel;

  //#endregion

  constructor(private readonly _userDoorStepService: DoorStepAgentService, private _activatedRoute: ActivatedRoute,
    private _router: Router,public domSanitizer: DomSanitizer) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
    }
    this.model.User = {} as UserViewModel;
    this.model.UserKYC = [] as UserKYCViewModel[];
    this.model.UserNominee = {} as UserNomineeViewModel;
    this.model.BankDetails = {} as UserBankDetailViewModel;
    this.model.Documents = [] as DocumentViewModel[];
    this.model.SecurityDeposit = {} as UserSecurityDepositViewModel;

    
  }
  //#region <<Method>>
  ngOnInit(): void {
    this.getDetail();
  }
  getDetail() {
    let serviceCall = this._userDoorStepService.GetDoorStepAgent(this.Id).subscribe(response => {
      serviceCall.unsubscribe();
      debugger
      if (response.IsSuccess) {
        if (response?.Data) {
          this.model = response?.Data as DoorStepAgentViewModel;

        } else {
          this._router.navigate([`../${Routing_Url.DoorStepAgentListUrl}`]);
        }
      }
    });

  }

  //#endregion
}
