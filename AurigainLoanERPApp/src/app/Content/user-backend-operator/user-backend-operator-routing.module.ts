import { BackendOperatorDashboardComponent } from './backend-operator-dashboard/backend-operator-dashboard.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path:'', component: BackendOperatorDashboardComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserBackendOperatorRoutingModule { }
