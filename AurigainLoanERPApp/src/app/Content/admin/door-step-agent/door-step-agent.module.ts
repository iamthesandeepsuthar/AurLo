import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DoorStepAgentRoutingModule } from './door-step-agent-routing.module';
import { DoorStepAgentRegistrationComponent } from './door-step-agent-registration/door-step-agent-registration.component';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { ListDoorStepAgentComponent } from './list-door-step-agent/list-door-step-agent.component';


@NgModule({
  declarations: [
    DoorStepAgentRegistrationComponent,
    ListDoorStepAgentComponent
  ],
  imports: [
    CommonModule,
    DoorStepAgentRoutingModule,
    SharedModule,
  ]
})
export class DoorStepAgentModule { }
