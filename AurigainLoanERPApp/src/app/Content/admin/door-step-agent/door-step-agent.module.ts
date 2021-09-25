import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DoorStepAgentRoutingModule } from './door-step-agent-routing.module';
import { DoorStepAgentRegistrationComponent } from './door-step-agent-registration/door-step-agent-registration.component';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';


@NgModule({
  declarations: [
    DoorStepAgentRegistrationComponent
  ],
  imports: [
    CommonModule,
    DoorStepAgentRoutingModule,
    SharedModule,
  ]
})
export class DoorStepAgentModule { }
