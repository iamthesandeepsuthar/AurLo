import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AgentRoutingModule } from './agent-routing.module';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    AgentRoutingModule,
    SharedModule
  ]
})
export class AgentModule { }
