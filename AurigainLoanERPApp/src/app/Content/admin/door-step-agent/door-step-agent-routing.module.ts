import { DoorStepAgentRegistrationComponent } from './door-step-agent-registration/door-step-agent-registration.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';

const routes: Routes = [
  {
    path: 'registration', component: DoorStepAgentRegistrationComponent, canActivate: [AuthenticationGuard]
  },
  // {
  //   path: 'user-role/add', component: UserRoleComponent, canActivate: [AuthenticationGuard]
  // },
  // {
  //   path: 'user-role/update:/id', component: UserRoleComponent, canActivate: [AuthenticationGuard]
  // },
  // {
  //   path: 'user-role/detail/:id', component: UserRoleComponent, canActivate: [AuthenticationGuard]
  // }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DoorStepAgentRoutingModule { }
