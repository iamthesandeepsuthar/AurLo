import { ToastrService } from 'ngx-toastr';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserReportingPersonPostModel } from 'src/app/Shared/Model/doorstep-agent-model/door-step-agent.model';
import { ReportingUser } from 'src/app/Shared/Model/master-model/user-manager-model.model';
import { UserManagerService } from 'src/app/Shared/Services/user-manager.service';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { AuthService } from '../../../auth.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-reporting-person-popup',
  templateUrl: './reporting-person-popup.component.html',
  styleUrls: ['./reporting-person-popup.component.scss'],
  providers:[UserManagerService]
})
export class ReportingPersonPopupComponent implements OnInit {
  formgrp!: FormGroup;
  reportingUsers!: ReportingUser[];
  model =  new UserReportingPersonPostModel;
  get f() { return this.formgrp.controls; }
  constructor(private readonly _userManagerService: UserManagerService ,
              private readonly toast: ToastrService,
              private readonly fb: FormBuilder,
              private readonly _commonService: CommonService,
              private  readonly _authService: AuthService,
              public dialogRef: MatDialogRef<ReportingPersonPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { Id: number, Type: string ,Heading: string }
    ) {
     }

  ngOnInit(): void {
    this.formInit();
    this.getReportingUser();
  }
  formInit() {
    this.formgrp = this.fb.group({
      reportingUser: [undefined, Validators.required],
    });
  }
  getReportingUser() {
    let serve = this._userManagerService.GetReportingPersonList().subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.reportingUsers = res.Data as ReportingUser[];
      } else {
        this.toast.error(res.Message as string ,'Record Not Found');
        return;
      }
    });
  }
  onSubmit() {
    this.formgrp.markAllAsTouched();
    if (this.formgrp.valid) {
      this.model.UserId = this.data.Id;
      this.model.ReportingUserId =Number(this.model.ReportingUserId);
      let subscription = this._userManagerService.AssignReportingPerson(this.model).subscribe(response => {
        subscription.unsubscribe();
        if (response.IsSuccess) {
          this.onClose();
        } else {
          this.toast.error(response.Message as string, 'Server Error');
          this.dialogRef.close(false);
          return;
        }
      });
    }
  }
  onClose(){
    this.dialogRef.close(true);
  }
}
