import { mode } from 'crypto-js';
import { Component, OnInit } from '@angular/core';
import { DoorStepAgentPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';
import { AlertService } from 'src/app/Shared/Services/alert.service';

@Component({
  selector: 'app-door-step-agent-registration',
  templateUrl: './door-step-agent-registration.component.html',
  styleUrls: ['./door-step-agent-registration.component.scss']
})
export class DoorStepAgentRegistrationComponent implements OnInit {

  model :DoorStepAgentPostModel = {} as DoorStepAgentPostModel;
  constructor(private readonly _alertService: AlertService) { }

  ngOnInit(): void {
    
  }

  Cancel() {
    this._alertService.Warning('hey Aakash hope you doing well !');
  }

}
