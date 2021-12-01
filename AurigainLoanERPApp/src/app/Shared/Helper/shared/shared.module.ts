import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { NgSelectModule } from '@ng-select/ng-select';
import { ToastrModule } from 'ngx-toastr';
import { UserBankDetailSectionComponent } from './UserRegistration/user-bank-detail-section/user-bank-detail-section.component';
import { UserDocumentDetailSectionComponent } from './UserRegistration/user-document-detail-section/user-document-detail-section.component';
import { UserKYCDetailSectionComponent } from './UserRegistration/user-kycdetail-section/user-kycdetail-section.component';
import { UserNomineeDetailSectionComponent } from './UserRegistration/user-nominee-detail-section/user-nominee-detail-section.component';
import { FileSelectorComponent } from 'src/app/Content/Common/file-selector/file-selector.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { UserSecurityDepositComponent } from './UserRegistration/user-security-deposit/user-security-deposit.component';
import { MatStepperModule } from '@angular/material/stepper';
import { UserKYCDocumentDetailComponent } from './UserRegistration/user-kycdocument-detail/user-kycdocument-detail.component';
import { PasswordChangeDialogComponent } from './Dialogs/password-change-dialog/password-change-dialog.component';

const CommonModules = [HttpClientModule, ReactiveFormsModule, FormsModule];

const InstalledModule = [
  BsDatepickerModule.forRoot(),
  MatDialogModule,
  MatSnackBarModule,
  MatTableModule,
  MatSortModule,
  MatDialogModule,
  MatPaginatorModule,
  MatSortModule,
  NgSelectModule,
  ToastrModule.forRoot({
    timeOut: 3000,
    closeButton: true,
    autoDismiss: true,
    maxOpened: 5,
  }),
  MatStepperModule,
];

const SharedComponent = [
  UserNomineeDetailSectionComponent,
  UserKYCDetailSectionComponent,
  UserDocumentDetailSectionComponent,
  UserBankDetailSectionComponent,
  UserSecurityDepositComponent,
  FileSelectorComponent,
  PasswordChangeDialogComponent,
  ,

];
const SharedEntryComponent = [
  PasswordChangeDialogComponent,
   
];
@NgModule({
  declarations: [SharedComponent],
  entryComponents: [SharedEntryComponent],
  imports: [CommonModule, CommonModules, InstalledModule],
  exports: [CommonModules, InstalledModule, SharedComponent],
})
export class SharedModule {}
