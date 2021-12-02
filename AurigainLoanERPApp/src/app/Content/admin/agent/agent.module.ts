import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { SharedModule } from "src/app/Shared/Helper/shared/shared.module";
import { AgentRoutingModule } from "./agent-routing.module";
import { AddUpdateAgentComponent } from "./list-agent/add-update-agent/add-update-agent.component";
import { DetailAgentComponent } from "./list-agent/detail-agent/detail-agent.component";
import { ListAgentComponent } from "./list-agent/list-agent.component";
import { AgentAvailabilityComponent } from './agent-availability/agent-availability.component';


@NgModule({
  declarations: [
    DetailAgentComponent,
    AddUpdateAgentComponent,
    ListAgentComponent,
    AgentAvailabilityComponent
  ],
  imports: [
    CommonModule,
    AgentRoutingModule,
    SharedModule,
  ]
})
export class AgentModule { }
