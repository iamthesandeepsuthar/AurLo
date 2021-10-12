import { MasterModule } from './master/master.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { ListManagersComponent } from './list-managers/list-managers.component';
import { AddUpdateManagersComponent } from './list-managers/add-update-managers/add-update-managers.component';
import { DetailManagersComponent } from './list-managers/detail-managers/detail-managers.component';


@NgModule({
  declarations: [
    ListManagersComponent,
    AddUpdateManagersComponent,
    DetailManagersComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule,
    MasterModule
  ]
})
export class AdminModule { }
