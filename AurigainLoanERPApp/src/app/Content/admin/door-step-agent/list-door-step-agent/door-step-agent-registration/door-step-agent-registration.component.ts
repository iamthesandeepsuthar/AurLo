import { AfterContentChecked, ChangeDetectorRef, Component, OnInit, ViewChild } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { DropDownModel, FilterDropDownPostModel } from "src/app/Shared/Helper/common-model";
import { DropDown_key, Routing_Url } from "src/app/Shared/Helper/constants";
import { UserBankDetailSectionComponent } from "src/app/Shared/Helper/shared/UserRegistration/user-bank-detail-section/user-bank-detail-section.component";
import { UserDocumentDetailSectionComponent } from "src/app/Shared/Helper/shared/UserRegistration/user-document-detail-section/user-document-detail-section.component";
import { UserKYCDetailSectionComponent } from "src/app/Shared/Helper/shared/UserRegistration/user-kycdetail-section/user-kycdetail-section.component";
import { UserNomineeDetailSectionComponent } from "src/app/Shared/Helper/shared/UserRegistration/user-nominee-detail-section/user-nominee-detail-section.component";
import { AlertService } from "src/app/Shared/Services/alert.service";
import { CommonService } from "src/app/Shared/Services/common.service";
import { DoorStepAgentService } from "src/app/Shared/Services/door-step-agent-services/door-step-agent.service";
import { DoorStepAgentPostModel, UserPostModel, UserKYCPostModel, UserNomineePostModel, UserBankDetailsPostModel, DocumentPostModel, UserSecurityDepositPostModel, DoorStepAgentViewModel, FilePostModel } from '../../../../../Shared/Model/doorstep-agent-model/door-step-agent.model';
import { Message } from '../../../../../Shared/Helper/constants';
import { UserSettingService } from "src/app/Shared/Services/user-setting-services/user-setting.service";
import { UserSettingPostModel } from "src/app/Shared/Model/User-setting-model/user-setting.model";
import { FileInfo } from '../../../../Common/file-selector/file-selector.component';
import { UserSecurityDepositComponent } from '../../../../../Shared/Helper/shared/UserRegistration/user-security-deposit/user-security-deposit.component';
import { UserRoleEnum } from '../../../../../Shared/Enum/fixed-value';
import { UserKYCDocumentDetailComponent } from '../../../../../Shared/Helper/shared/UserRegistration/user-kycdocument-detail/user-kycdocument-detail.component';
import { StateDistrictService } from "src/app/Shared/Services/master-services/state-district.service";
import { AvailableAreaModel } from "src/app/Shared/Model/User-setting-model/user-availibility.model";


@Component({
  selector: 'app-door-step-agent-registration',
  templateUrl: './door-step-agent-registration.component.html',
  styleUrls: ['./door-step-agent-registration.component.scss'],
  providers: [DoorStepAgentService, UserSettingService,StateDistrictService]
})
export class DoorStepAgentRegistrationComponent implements OnInit, AfterContentChecked {
  Id: number = 0;
  model = new DoorStepAgentPostModel();
  areaModel!: AvailableAreaModel[];
  profileModel = {} as UserSettingPostModel;
  formGroup!: FormGroup;
  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };

  get f() { return this.formGroup.controls; }
  get routing_Url() { return Routing_Url }
  get minDate() { return new Date() };
  get maxDate() { return new Date() };


  fileData!: File;
  previewUrl: any = null;

  @ViewChild(UserBankDetailSectionComponent, { static: false }) _childUserBankDetailSection!: UserBankDetailSectionComponent;
  @ViewChild(UserDocumentDetailSectionComponent, { static: false }) _childUserDocumentDetailSection!: UserDocumentDetailSectionComponent;
  @ViewChild(UserKYCDetailSectionComponent, { static: false }) _childUserKYCDetailSection!: UserKYCDetailSectionComponent;
  @ViewChild(UserNomineeDetailSectionComponent, { static: false }) _childUserNomineeDetailSection!: UserNomineeDetailSectionComponent;
  @ViewChild(UserSecurityDepositComponent, { static: false }) _childUserSecurityDepositSection!: UserSecurityDepositComponent;
  @ViewChild(UserKYCDocumentDetailComponent, { static: false }) _childUserKYCDocumentSection!: UserKYCDocumentDetailComponent;

  constructor(private cdr: ChangeDetectorRef, private readonly _alertService: AlertService, private readonly fb: FormBuilder,
    private readonly _userDoorStepService: DoorStepAgentService, private _activatedRoute: ActivatedRoute, private _router: Router,
    readonly _commonService: CommonService, private readonly _toast: ToastrService,
     private readonly _userSettingService: UserSettingService, private readonly _locationService: StateDistrictService) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;

    }
    this.model.User = new UserPostModel();
    this.model.UserKYC = [] as UserKYCPostModel[];
    this.model.UserNominee = new UserNomineePostModel();
    this.model.BankDetails = new UserBankDetailsPostModel();
    this.model.Documents = [] as DocumentPostModel[];
    // this.model.SecurityDeposit = new UserSecurityDepositPostModel();

  }

  ngOnInit(): void {

    this.formInit();
    this.GetDropDown();
    if (this.Id > 0) {
      this.onGetDetail();
    }
  }
  ngAfterContentChecked() {

    this.cdr.detectChanges();
  }

  GetDropDown() {
    this._commonService.GetDropDown([DropDown_key.ddlQualification, DropDown_key.ddlState, DropDown_key.ddlGender]).subscribe(res => {
      if (res.IsSuccess) {
        let ddls = res.Data as DropDownModel;
        this.dropDown.ddlState = ddls.ddlState;
        this.dropDown.ddlQualification = ddls.ddlQualification;
        this.dropDown.ddlGender = ddls.ddlGender;
      }
    });
  }
  getAreaByPincode(value: any) {
    let subscription = this._locationService.GetAreaByPincode(value).subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        this.areaModel = response.Data as AvailableAreaModel[];
      } else {
        this._toast.warning(response.Message as string, 'Server Error');

      }
    });
  }

  GetFilterDropDown(key: string, FilterFrom: string, Values: any) {

    if (Values) {
      let model = {
        Key: key,
        FileterFromKey: FilterFrom,
        Values: [Values],

      } as FilterDropDownPostModel;
      this._commonService.GetFilterDropDown(model).subscribe(res => {
        if (res.IsSuccess) {
          let ddls = res.Data as DropDownModel;
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

      if (!this.model.Id || this.model.Id == 0) {
        this.model.User.IsApproved = false;
        this.model.User.UserRoleId = this.model.User.UserRoleId ? this.model.User.UserRoleId : UserRoleEnum.DoorStepAgent;
        this.model.User.UserName = this.model.User.UserName ? this.model.User.UserName : this.model.User.Email;

      }
      this.model.SelfFunded = Boolean(this.model.SelfFunded);

      let serv = this._userDoorStepService.AddUpdateDoorStepAgent(this.model).subscribe(res => {
        serv.unsubscribe();
        if (res.IsSuccess) {
          if (this.Id == 0) {
            this.Id = Number(res.Data);
            this.onUploadProfileImage();
          }

          this._toast.success('Doorstep agent added successful', 'Success');
          this._router.navigate([this.routing_Url.AdminModule + '/' + this.routing_Url.DoorStepModule + '/' + this.routing_Url.DoorStepAgentListUrl]);
        } else {
          this._toast.error(Message.SaveFail, 'Error');
          return;
        }
      });
    }
  }
  onFrmReset() {
    this.formGroup.reset();
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
    if (this._childUserSecurityDepositSection) {
      this._childUserSecurityDepositSection.formGroup.markAllAsTouched();
    }
    if (this._childUserKYCDocumentSection) {
      this._childUserKYCDocumentSection.frmGroup.markAllAsTouched();
    }



    if (this._childUserBankDetailSection && this._childUserBankDetailSection.formGroup.valid) {
      this._childUserBankDetailSection.onFrmSubmit();
    } else if (isValid && this._childUserBankDetailSection && !this._childUserBankDetailSection.formGroup.valid) {
      isValid = false;
    }

    if (isValid && this._childUserDocumentDetailSection && this._childUserDocumentDetailSection.formGroup.valid) {
      this._childUserDocumentDetailSection.onFrmSubmit();
    }
    else if (isValid && this._childUserDocumentDetailSection && !this._childUserDocumentDetailSection.formGroup.valid) {
      isValid = false;
    }

    if (isValid && this._childUserKYCDetailSection && this._childUserKYCDetailSection.formGroup.valid) {
      this._childUserKYCDetailSection.onFrmSubmit();
    }
    else if (isValid && this._childUserKYCDetailSection && !this._childUserKYCDetailSection.formGroup.valid) {
      isValid = false;
    }

    if (isValid && this._childUserSecurityDepositSection && this._childUserSecurityDepositSection.formGroup.valid) {
      this._childUserSecurityDepositSection.onFrmSubmit();
    }
    else if (isValid && this._childUserSecurityDepositSection && !this._childUserSecurityDepositSection.formGroup.valid) {
      isValid = false;
    }

    if (isValid && this._childUserNomineeDetailSection && this._childUserNomineeDetailSection.formGroup.valid) {
      this._childUserNomineeDetailSection.onFrmSubmit();
    }
    else if (isValid && this._childUserNomineeDetailSection && !this._childUserNomineeDetailSection.formGroup.valid) {
      isValid = false;
    }

    if (isValid && this._childUserKYCDocumentSection && this._childUserKYCDocumentSection.frmGroup.valid) {
      this._childUserKYCDocumentSection.onFrmSubmit();
    }
    else if (isValid && this._childUserKYCDocumentSection && !this._childUserKYCDocumentSection.frmGroup.valid) {
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
     // District: [undefined, Validators.required],
      State: [undefined, Validators.required],
      PinCode: [undefined, Validators.compose([Validators.required, Validators.maxLength(6), Validators.minLength(6)])],
      DateOfBirth: [undefined, Validators.required],
      ProfilePictureUrl: [undefined, undefined],
      SelfFunded: [false, Validators.required],
      IsActive: [true, Validators.required],
      IsWhatsApp: [false, Validators.required],
      Mobile: [undefined, Validators.compose([Validators.required, Validators.maxLength(12), Validators.minLength(10)])],
      Email: [false, Validators.compose([Validators.required, Validators.email])],

    });
  }

  // onUpdateProfileImage(file: FileInfo) {
  //   if (this.Id > 0) {
  //     this.profileModel.ProfileBase64 = file.FileBase64;
  //     this.profileModel.FileName = file.Name;
  //     this.profileModel.UserId = this.Id;
  //      this._userSettingService.UpdateUserProfile(this.profileModel).then(res => {

  //       if (res.IsSuccess) {
  //         this._toast.success('profile picture upload successful', 'Upload Status');
  //       } else {
  //         this._toast.error(res.Message as string, 'Upload Status');
  //       }
  //     });
  //   }

  // }

  onGetDetail() {
    if (this.Id > 0) {
      this._userDoorStepService.GetDoorStepAgent(this.Id).subscribe(res => {
        if (res.IsSuccess) {
          let data = res.Data as DoorStepAgentViewModel;
          if (data) {

            this.model.Id = data?.Id;
            this.model.FullName = data?.FullName;
            this.model.FatherName = data?.FatherName;
            this.model.Gender = data?.Gender;
            this.model.QualificationId = data?.QualificationId;
            this.model.Address = data?.Address;
            this.model.DistrictId = data?.DistrictId;
            this.model.StateId = data?.StateId;
            this.model.PinCode = data?.PinCode;
            this.model.AreaPincodeId = data?.AreaPincodeId;
            this.model.DateOfBirth = data?.DateOfBirth;
            this.previewUrl = data?.User.ProfilePath;
            this.model.SelfFunded = data?.SelfFunded;
            this.getAreaByPincode(this.model.PinCode);

            if (data?.User) {
              this.model.User.Email = data?.User?.Email;
              this.model.User.Mobile = data?.User?.Mobile;
              this.model.User.Id = data?.User?.Id;
              this.model.User.UserRoleId = data?.User?.UserRoleId;
              this.model.User.UserName = data?.User?.UserName;
              this.model.User.IsApproved = data?.User?.IsApproved;
              this.model.User.DeviceToken = data?.User?.DeviceToken;
              this.model.User.IsWhatsApp = data?.User?.IsWhatsApp ?? false;
            }

            if (data?.BankDetails) {
              this.model.BankDetails.Id = data?.BankDetails?.Id;
              this.model.BankDetails.BankName = data?.BankDetails?.BankName;
              this.model.BankDetails.AccountNumber = data?.BankDetails?.AccountNumber;
              this.model.BankDetails.Ifsccode = data?.BankDetails?.Ifsccode;
              this.model.BankDetails.Address = data?.BankDetails?.Address;

            }

            if (data?.UserKYC) {

              this.model.UserKYC = data?.UserKYC?.map(user => {
                return {
                  Id: user.Id,
                  Kycnumber: user.Kycnumber,
                  KycdocumentTypeId: user.KycdocumentTypeId
                } as UserKYCPostModel;

              });
              this.model.UserKYC = this.model.UserKYC.sort(x => x.KycdocumentTypeId);

            }

            if (data?.UserNominee) {
              this.model.UserNominee.Id = data?.UserNominee?.Id;
              this.model.UserNominee.NamineeName = data?.UserNominee?.NamineeName;
              this.model.UserNominee.RelationshipWithNominee = data?.UserNominee?.RelationshipWithNominee;
            }

            if (data?.ReportingPerson) {
              this.model.ReportingPerson.Id = data?.ReportingPerson?.Id;
              this.model.ReportingPerson.UserId = data?.ReportingPerson?.UserId;
              this.model.ReportingPerson.ReportingUserId = data?.ReportingPerson?.ReportingUserId;
            }

            if (data?.SecurityDeposit) {
              if (!this.model.SecurityDeposit) {
                this.model.SecurityDeposit = new UserSecurityDepositPostModel();
              }
              this.model.SecurityDeposit.Id = data?.SecurityDeposit?.Id;
              this.model.SecurityDeposit.PaymentModeId = data?.SecurityDeposit?.PaymentModeId;
              this.model.SecurityDeposit.Amount = data?.SecurityDeposit?.Amount;
              this.model.SecurityDeposit.CreditDate = data?.SecurityDeposit?.CreditDate as Date;
              this.model.SecurityDeposit.ReferanceNumber = data?.SecurityDeposit?.ReferanceNumber;
              this.model.SecurityDeposit.AccountNumber = data?.SecurityDeposit?.AccountNumber;
              this.model.SecurityDeposit.BankName = data?.SecurityDeposit?.BankName;

            }
            if (data?.Documents) {

              this.model.Documents = data?.Documents?.map(doc => {
                return {
                  Id: doc?.Id,
                  DocumentTypeId: doc?.DocumentTypeId,
                  Files: doc?.UserDocumentFiles.map(file => {
                    return {
                      Id: file?.Id,
                      FileName: file?.FileName,
                      File: file?.Path,
                      FileType: file?.FileType,
                    } as FilePostModel;
                  }),
                } as DocumentPostModel;

              });
            }

          }
        }
      });

    }
  }
  fileProgress(fileInput: any) {
    this.fileData = fileInput.target.files[0] as File;
    this.preview();
  }
  preview() {
    // Show preview
    const mimeType = this.fileData?.type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
    const reader = new FileReader();
    reader.readAsDataURL(this.fileData);
    reader.onload = (event) => {
      this.previewUrl = reader.result;
    };
  }
  onUploadProfileImage() {
    if (this.Id > 0) {
      let profileModel = new UserSettingPostModel();
      profileModel.FileName = this.fileData?.name;
      profileModel.UserId = Number(this.Id);
      profileModel.ProfileBase64 = this.previewUrl;
      this._userSettingService.UpdateUserProfile(profileModel).then(res => {

        if (res.IsSuccess) {
          this._toast.success('profile picture upload successful', 'File Upload');
        } else {
          this._toast.error(res.Message as string, 'Upload Status');
        }
      });
    }
  }

}
