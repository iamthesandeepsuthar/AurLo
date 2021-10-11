import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { SharedModule } from "src/app/Shared/Helper/shared/shared.module";
import { DoorStepAgentRoutingModule } from "./door-step-agent-routing.module";
import { DetailDoorStepAgentComponent } from "./list-door-step-agent/detail-door-step-agent/detail-door-step-agent.component";
import { DoorStepAgentRegistrationComponent } from "./list-door-step-agent/door-step-agent-registration/door-step-agent-registration.component";
import { ListDoorStepAgentComponent } from "./list-door-step-agent/list-door-step-agent.component";
import { DoorStepAgentAvailabilityComponent } from './door-step-agent-availability/door-step-agent-availability.component';

@NgModule({
  declarations: [
    DoorStepAgentRegistrationComponent,
    ListDoorStepAgentComponent,
    DetailDoorStepAgentComponent,
    DoorStepAgentAvailabilityComponent
  ],
  imports: [
    CommonModule,
    DoorStepAgentRoutingModule,
    SharedModule,
  ]
})
export class DoorStepAgentModule { }
