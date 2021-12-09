import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component'; 
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { DashboardComponent } from './dashboard.component';
import { AgentDashboardComponent } from './agent-dashboard/agent-dashboard.component';
import { DoorStepAgentDashboardComponent } from './door-step-agent-dashboard/door-step-agent-dashboard.component';
import { WebOperatorDashboardComponent } from './web-operator-dashboard/web-operator-dashboard.component';
import { CustomerDashboardComponent } from './customer-dashboard/customer-dashboard.component';


@NgModule({
  declarations: [
    
    AdminDashboardComponent,
         DashboardComponent,
         AgentDashboardComponent,
         DoorStepAgentDashboardComponent,
         WebOperatorDashboardComponent,
         CustomerDashboardComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SharedModule
  ]
})
export class DashboardModule { }
