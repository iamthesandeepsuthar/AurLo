import { MasterModule } from './master/master.module';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [

  { path: "master", loadChildren: () => import('./master/master.module').then(m => m.MasterModule) },
  {path: 'door-step-agent',loadChildren: () => import('./door-step-agent/door-step-agent.module').then(m=>m.DoorStepAgentModule)},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
