import { MasterModule } from './master/master.module';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [

  { path: "master", loadChildren: () => import('./master/master.module').then(m => m.MasterModule) },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
