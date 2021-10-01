import { DoorStepAgentRegistrationComponent } from './door-step-agent-registration/door-step-agent-registration.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { ListDoorStepAgentComponent } from './list-door-step-agent/list-door-step-agent.component';

const routes: Routes = [
  {
    path: 'registration', component: DoorStepAgentRegistrationComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: 'door-step-agents', component: ListDoorStepAgentComponent, canActivate: [AuthenticationGuard]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DoorStepAgentRoutingModule { }
