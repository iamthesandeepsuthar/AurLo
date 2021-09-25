import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { UserRoleComponent } from './user-role/user-role.component';

const routes: Routes = [
  {
    path: 'user-role', component: UserRoleComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: 'user-role/add', component: UserRoleComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: 'user-role/update:/id', component: UserRoleComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: 'user-role/detail/:id', component: UserRoleComponent, canActivate: [AuthenticationGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
