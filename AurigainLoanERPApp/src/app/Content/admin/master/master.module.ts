import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MasterRoutingModule } from './master-routing.module';
import { SharedModule } from 'src/app/Shared/Helper/shared/shared.module';
import { UserRoleComponent } from './user-role/user-role.component';
import { AddUpdateUserRoleComponent } from './user-role/add-update-user-role/add-update-user-role.component';
import { DetailUserRoleComponent } from './user-role/detail-user-role/detail-user-role.component';
import { AddUpdateQualificationComponent } from './qualification/add-update-qualification/add-update-qualification.component';
import { DetailQualificationComponent } from './qualification/detail-qualification/detail-qualification.component';
import { PaymentModeComponent } from './payment-mode/payment-mode.component';
import { AddUpdatePaymentModeComponent } from './payment-mode/add-update-payment-mode/add-update-payment-mode.component';
import { DetailPaymentModeComponent } from './payment-mode/detail-payment-mode/detail-payment-mode.component';
import { StateComponent } from './state/state.component';
import { AddUpdateStateComponent } from './state/add-update-state/add-update-state.component';
import { DetailStateComponent } from './state/detail-state/detail-state.component';
import { DistrictComponent } from './district/district.component';
import { AddUpdateDistrictComponent } from './district/add-update-district/add-update-district.component';
import { DetailDistrictComponent } from './district/detail-district/detail-district.component';
import { KycDocumentTypeComponent } from './kyc-document-type/kyc-document-type.component';
import { AddUpdateDocumentTypeComponent } from './kyc-document-type/add-update-document-type/add-update-document-type.component';
import { DetailDocumentTypeComponent } from './kyc-document-type/detail-document-type/detail-document-type.component';
import { BanksComponent } from './banks/banks.component';
import { DetailBankComponent } from './banks/detail-bank/detail-bank.component';

@NgModule({
  declarations: [
    UserRoleComponent,
    AddUpdateUserRoleComponent,
    DetailUserRoleComponent,
    AddUpdateQualificationComponent,
    DetailQualificationComponent,
    PaymentModeComponent,
    AddUpdatePaymentModeComponent,
    DetailPaymentModeComponent,
    StateComponent,
    AddUpdateStateComponent,
    DetailStateComponent,
    DistrictComponent,
    AddUpdateDistrictComponent,
    DetailDistrictComponent,
    KycDocumentTypeComponent,
    AddUpdateDocumentTypeComponent,
    DetailDocumentTypeComponent,
    BanksComponent,
    DetailBankComponent,
  ],
  imports: [
    CommonModule,
    MasterRoutingModule,
    SharedModule,

  ]
})
export class MasterModule { }
