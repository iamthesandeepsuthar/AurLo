import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { DetailAgentComponent } from './list-agent/detail-agent/detail-agent.component';
import { ListAgentComponent } from './list-agent/list-agent.component';
import { AddUpdateAgentComponent } from './list-agent/add-update-agent/add-update-agent.component';

const routes: Routes = [
  { path: '', component: ListAgentComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.AgentListUrl}`, component: ListAgentComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.AgenDetailUrl}/:id`, component: DetailAgentComponent, canActivate: [AuthenticationGuard] },
  { path: `${Routing_Url.AgentRegistrationUrl}/:id`, component: AddUpdateAgentComponent, canActivate: [AuthenticationGuard] },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AgentRoutingModule { }
