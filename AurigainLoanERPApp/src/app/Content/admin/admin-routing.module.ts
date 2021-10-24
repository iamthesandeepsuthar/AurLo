import { DetailManagersComponent } from './list-managers/detail-managers/detail-managers.component';
import { AddUpdateManagersComponent } from './list-managers/add-update-managers/add-update-managers.component';
import { ListManagersComponent } from './list-managers/list-managers.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';


const routes: Routes = [

  { path: "master", loadChildren: () => import('./master/master.module').then(m => m.MasterModule) },
  {path: 'door-step-agent',loadChildren: () => import('./door-step-agent/door-step-agent.module').then(m=>m.DoorStepAgentModule)},
  {path: 'agent',loadChildren: () => import('./agent/agent.module').then(m=>m.AgentModule)},
  {path: 'managers', component:ListManagersComponent , canActivate: [AuthenticationGuard]},
  {path:'managers/add-manager/:id', component:AddUpdateManagersComponent, canActivate:[AuthenticationGuard]},
  {path:'managers/detail-manager/:id', component:DetailManagersComponent,canActivate:[AuthenticationGuard]}

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
