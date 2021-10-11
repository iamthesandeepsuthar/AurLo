import { DoorStepAgentRegistrationComponent } from './list-door-step-agent/door-step-agent-registration/door-step-agent-registration.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { ListDoorStepAgentComponent } from './list-door-step-agent/list-door-step-agent.component';
import { Routing_Url } from 'src/app/Shared/Helper/constants';

const routes: Routes = [
 
  {
    path: `${Routing_Url.DoorStepAgentListUrl}`, component: ListDoorStepAgentComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: `${Routing_Url.DoorStepAgentRegistrationUrl}/:id`, component: DoorStepAgentRegistrationComponent, canActivate: [AuthenticationGuard]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DoorStepAgentRoutingModule { }
