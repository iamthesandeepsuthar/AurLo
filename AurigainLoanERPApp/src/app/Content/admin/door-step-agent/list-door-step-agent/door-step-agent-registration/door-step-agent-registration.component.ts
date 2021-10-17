import { Component, OnInit, ViewChild } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { DropDownModol, FilterDropDownPostModel } from "src/app/Shared/Helper/common-model";
import { DropDown_key, Routing_Url } from "src/app/Shared/Helper/constants";
import { UserBankDetailSectionComponent } from "src/app/Shared/Helper/shared/UserRegistration/user-bank-detail-section/user-bank-detail-section.component";
import { UserDocumentDetailSectionComponent } from "src/app/Shared/Helper/shared/UserRegistration/user-document-detail-section/user-document-detail-section.component";
import { UserKYCDetailSectionComponent } from "src/app/Shared/Helper/shared/UserRegistration/user-kycdetail-section/user-kycdetail-section.component";
import { UserNomineeDetailSectionComponent } from "src/app/Shared/Helper/shared/UserRegistration/user-nominee-detail-section/user-nominee-detail-section.component";
import { AlertService } from "src/app/Shared/Services/alert.service";
import { CommonService } from "src/app/Shared/Services/common.service";
import { DoorStepAgentService } from "src/app/Shared/Services/door-step-agent-services/door-step-agent.service";
import { DoorStepAgentPostModel, UserPostModel, UserKYCPostModel, UserNomineePostModel, UserBankDetailsPostModel, DocumentPostModel, UserSecurityDepositPostModel } from "../../../../../Shared/Model/doorstep-agent-model/door-step-agent.model";
import { Message } from '../../../../../Shared/Helper/constants';
import { UserSettingService } from "src/app/Shared/Services/user-setting-services/user-setting.service";
import { UserSettingPostModel } from "src/app/Shared/Model/User-setting-model/user-setting.model";
import { FileInfo } from '../../../../Common/file-selector/file-selector.component';

@Component({
  selector: 'app-door-step-agent-registration',
  templateUrl: './door-step-agent-registration.component.html',
  styleUrls: ['./door-step-agent-registration.component.scss'],
  providers: [DoorStepAgentService, UserSettingService]
})
export class DoorStepAgentRegistrationComponent implements OnInit {
  Id: number = 0;
  model = new DoorStepAgentPostModel();
  profileModel = {} as UserSettingPostModel;
  formGroup!: FormGroup;
  dropDown = new DropDownModol();
  get ddlkeys() { return DropDown_key };

  get f() { return this.formGroup.controls; }
  get routing_Url() { return Routing_Url }
  get minDate() { return new Date() };
  get maxDate() { return new Date() };

  @ViewChild(UserBankDetailSectionComponent, { static: false }) _childUserBankDetailSection!: UserBankDetailSectionComponent;
  @ViewChild(UserDocumentDetailSectionComponent, { static: false }) _childUserDocumentDetailSection!: UserDocumentDetailSectionComponent;
  @ViewChild(UserKYCDetailSectionComponent, { static: false }) _childUserKYCDetailSection!: UserKYCDetailSectionComponent;
  @ViewChild(UserNomineeDetailSectionComponent, { static: false }) _childUserNomineeDetailSection!: UserNomineeDetailSectionComponent;

  constructor(private readonly _alertService: AlertService, private readonly fb: FormBuilder,
    private readonly _userDoorStepService: DoorStepAgentService, private _activatedRoute: ActivatedRoute, private _router: Router,
    readonly _commonService: CommonService, private readonly _toast: ToastrService, private readonly _userSettingService: UserSettingService) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;

    }
    this.model.User = new UserPostModel();
    this.model.UserKYC = [] as UserKYCPostModel[];
    this.model.UserNominee = new UserNomineePostModel();
    this.model.BankDetails = new UserBankDetailsPostModel();
    this.model.Documents = [] as DocumentPostModel[];
    //  this.model.SecurityDeposit = new UserSecurityDepositPostModel();

  }

  ngOnInit(): void {

    this.formInit();
    this.GetDropDown();
    if (this.Id > 0) {
      alert
      this.onGetDetail();
    }
  }


  GetDropDown() {
    this._commonService.GetDropDown([DropDown_key.ddlQualification, DropDown_key.ddlState, DropDown_key.ddlGender]).subscribe(res => {
      if (res.IsSuccess) {

        let ddls = res.Data as DropDownModol;
        this.dropDown.ddlState = ddls.ddlState;
        this.dropDown.ddlQualification = ddls.ddlQualification;
        this.dropDown.ddlGender = ddls.ddlGender;


      }
    });
  }

  GetFilterDropDown(key: string, FilterFrom: string, Values: any) {
    debugger
    if (Values) {
      let model = {
        Key: key,
        FileterFromKey: FilterFrom,
        Values: [Values],

      } as FilterDropDownPostModel;
      this._commonService.GetFilterDropDown(model).subscribe(res => {
        if (res.IsSuccess) {
          let ddls = res.Data as DropDownModol;
          if (ddls.ddlDistrict) {
            this.dropDown.ddlDistrict = ddls.ddlDistrict;

          } else {
            this.dropDown.ddlDistrict = [];
          }



        }
      });
    }
  }

  onFrmSubmit() {
    this.formGroup.markAllAsTouched();
    let ChildValid: boolean = this.submitChildData();
    if (this.formGroup.valid && ChildValid) {

      this.model.User.UserName = this.model.User.UserName ? this.model.User.UserName : this.model.User.Email;
      this.model.User.UserRoleId = this.model.User.UserRoleId ? this.model.User.UserRoleId : 1;
      this.model.User.IsApproved = false;
      this.model.SelfFunded = Boolean(this.model.SelfFunded);

      let serv = this._userDoorStepService.AddUpdateDoorStepAgent(this.model).subscribe(res => {
        serv.unsubscribe();
        if (res.IsSuccess) {
          this._alertService.Success(Message.SaveSuccess);
        } else {
          this._alertService.Error(Message.SaveFail);

        }
      });
    }
  }

  submitChildData(): boolean {
    let isValid = true;
    if (this._childUserBankDetailSection) {
      this._childUserBankDetailSection.formGroup.markAllAsTouched();
    }
    if (this._childUserDocumentDetailSection) {
      this._childUserDocumentDetailSection.formGroup.markAllAsTouched();
    }
    if (this._childUserKYCDetailSection) {
      this._childUserKYCDetailSection.formGroup.markAllAsTouched();
    }
    if (this._childUserNomineeDetailSection) {
      this._childUserNomineeDetailSection.formGroup.markAllAsTouched();
    }

    if (this._childUserBankDetailSection && this._childUserBankDetailSection.formGroup.valid) {
      this._childUserBankDetailSection.onFrmSubmit();
    } else {
      isValid = false;
    }
    if (this._childUserDocumentDetailSection && this._childUserDocumentDetailSection.formGroup.valid) {
      this._childUserDocumentDetailSection.onFrmSubmit();
    } else {
      isValid = false;
    }
    if (this._childUserKYCDetailSection && this._childUserKYCDetailSection.formGroup.valid) {
      this._childUserKYCDetailSection.onFrmSubmit();
    } else {
      isValid = false;
    }
    if (this._childUserNomineeDetailSection && this._childUserNomineeDetailSection.formGroup.valid) {
      this._childUserNomineeDetailSection.onFrmSubmit();
    } else {
      isValid = false;
    }

    return isValid;
  }

  bindDocs(docs: DocumentPostModel[]) {

    this.model.Documents = docs;
  }

  formInit() {
    this.formGroup = this.fb.group({
      FullName: [undefined, Validators.required],
      FatherName: [undefined, Validators.required],
      Gender: [undefined, Validators.required],
      Qualification: [undefined, Validators.required],
      Address: [undefined, undefined],
      District: [undefined, Validators.required],
      State: [undefined, Validators.required],
      PinCode: [undefined, Validators.compose([Validators.required, Validators.maxLength(6), Validators.minLength(6)])],
      DateOfBirth: [undefined, Validators.required],
      ProfilePictureUrl: [undefined, Validators.required],
      SelfFunded: [false, Validators.required],
      IsActive: [true, Validators.required],
      Mobile: [undefined, Validators.compose([Validators.required, Validators.maxLength(12), Validators.minLength(10)])],
      Email: [false, Validators.compose([Validators.required, Validators.email])],

    });
  }

  onUpdateProfileImage(file: FileInfo) {
    if (this.Id > 0) {
      this.profileModel.ProfileBase64 = file.FileBase64;
      this.profileModel.FileName = file.Name;
      this.profileModel.UserId = this.Id;
      this._userSettingService.UpdateUserProfile(this.profileModel).subscribe(res => {

      });
    }

  }

  onGetDetail() {
    if (this.Id > 0) {
      this._userDoorStepService.GetDoorStepAgent(this.Id).subscribe(res => {
        if (res.IsSuccess) { }
      });
    }

  }
}
