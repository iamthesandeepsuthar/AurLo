import { StateComponent } from './state/state.component';
import { DetailPaymentModeComponent } from './payment-mode/detail-payment-mode/detail-payment-mode.component';
import { PaymentModeComponent } from './payment-mode/payment-mode.component';
import { AddUpdatePaymentModeComponent } from './payment-mode/add-update-payment-mode/add-update-payment-mode.component';
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
import { AddUpdateDocumentTypeComponent } from './kyc-document-type/add-update-document-type/add-update-document-type.component';
import { KycDocumentTypeComponent } from './kyc-document-type/kyc-document-type.component';
import { DetailDocumentTypeComponent } from './kyc-document-type/detail-document-type/detail-document-type.component';
import { AddUpdateStateComponent } from './state/add-update-state/add-update-state.component';
import { DetailStateComponent } from './state/detail-state/detail-state.component';
import { DistrictComponent } from './district/district.component';
import { AddUpdateDistrictComponent } from './district/add-update-district/add-update-district.component';
import { DetailDistrictComponent } from './district/detail-district/detail-district.component';

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
  },
  {
    path: "kyc-document-type/add-document-type/:id", component: AddUpdateDocumentTypeComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "kyc-document-type", component: KycDocumentTypeComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "kyc-document-type/detail-document-type/:id", component: DetailDocumentTypeComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "payment-modes/add-payment-mode/:id", component: AddUpdatePaymentModeComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "payment-modes", component: PaymentModeComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "payment-modes/detail-payment-mode/:id", component: DetailPaymentModeComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "states/add-state/:id", component: AddUpdateStateComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "states", component: StateComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "states/detail-state/:id", component: DetailStateComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "district/add-district/:id", component: AddUpdateDistrictComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "district", component: DistrictComponent, canActivate: [AuthenticationGuard]
  },
  {
    path: "district/detail-district/:id", component: DetailDistrictComponent, canActivate: [AuthenticationGuard]
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MasterRoutingModule { }
