import { DoorStepAgentRegistrationComponent } from './list-door-step-agent/door-step-agent-registration/door-step-agent-registration.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { ListDoorStepAgentComponent } from './list-door-step-agent/list-door-step-agent.component';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { DetailDoorStepAgentComponent } from './list-door-step-agent/detail-door-step-agent/detail-door-step-agent.component';
import { DoorStepAgentAvailabilityComponent } from './door-step-agent-availability/door-step-agent-availability.component';

const routes: Routes = [

  {
    path: `${Routing_Url.DoorStepAgentListUrl}`, component: ListDoorStepAgentComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: `${Routing_Url.DoorStepAgentRegistrationUrl}/:id`, component: DoorStepAgentRegistrationComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: `${Routing_Url.DoorStepAgenDetailUrl}/:id`, component: DetailDoorStepAgentComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: `${Routing_Url.DoorStepAgentAvailibilityUrl}/:userId`, component: DoorStepAgentAvailabilityComponent, canActivate: [AuthenticationGuard]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DoorStepAgentRoutingModule { }
