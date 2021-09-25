import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MasterRoutingModule } from './master-routing.module';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { UserRoleComponent } from './user-role/user-role.component';
import { AddUpdateUserRoleComponent } from './user-role/add-update-user-role/add-update-user-role.component';
import { DetailUserRoleComponent } from './user-role/detail-user-role/detail-user-role.component';


@NgModule({
  declarations: [
    UserRoleComponent,
    AddUpdateUserRoleComponent,
    DetailUserRoleComponent
  ],
  imports: [
    CommonModule,
    MasterRoutingModule,
    SharedModule,

  ]
})
export class MasterModule { }
