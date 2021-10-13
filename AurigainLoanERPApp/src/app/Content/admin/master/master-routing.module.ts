import { DetailQualificationComponent } from './qualification/detail-qualification/detail-qualification.component';
import { QualificationComponent } from './qualification/qualification.component';
import { AddUpdateQualificationComponent } from './qualification/add-update-qualification/add-update-qualification.component';
import { DetailUserRoleComponent } from './user-role/detail-user-role/detail-user-role.component';
import { AddUpdateUserRoleComponent } from './user-role/add-update-user-role/add-update-user-role.component';
import { Routing_Url } from './../../../Shared/Helper/constants';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { AuthenticationGuard } from 'src/app/Shared/Helper/authentication.guard';
import { UserRoleComponent } from './user-role/user-role.component';

const routes: Routes = [
  {
    path: `${Routing_Url.UserRoleListUrl}`, component: UserRoleComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: `${Routing_Url.UserRoleListUrl+Routing_Url.UserRoleAddUpdateUrl}:id`, component: AddUpdateUserRoleComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: `${Routing_Url.UserRoleListUrl+Routing_Url.UserRoleDetailUrl}:id`, component: DetailUserRoleComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "qualifications/add-qualification/:id", component: AddUpdateQualificationComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "qualifications", component: QualificationComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "qualifications/detail-qualification/:id", component: DetailQualificationComponent, canActivate: [AuthenticationGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
