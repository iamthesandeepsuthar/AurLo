import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { AgentRoutingModule } from "./agent-routing.module";
import { AddUpdateAgentComponent } from "./list-agent/add-update-agent/add-update-agent.component";
import { DetailAgentComponent } from "./list-agent/detail-agent/detail-agent.component";
import { ListAgentComponent } from "./list-agent/list-agent.component";


@NgModule({
  declarations: [
    DetailAgentComponent,
    AddUpdateAgentComponent,
    ListAgentComponent
  ],
  imports: [
    CommonModule,
    AgentRoutingModule
  ]
})
export class AgentModule { }
