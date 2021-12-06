import { UserSettingService } from 'src/app/Shared/Services/user-setting-services/user-setting.service';
import { Component, OnInit } from '@angular/core';
import { GetOtpModel, GetOtpResponseModel, OptVerifiedModel, UserChangePassword } from 'src/app/Shared/Model/User-setting-model/user-change-password.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/Shared/Helper/auth.service';

@Component({
  selector: 'app-change-password-popup',
  templateUrl: './change-password-popup.component.html',
  styleUrls: ['./change-password-popup.component.scss'],
  providers:[UserSettingService]
})
export class ChangePasswordPopupComponent implements OnInit {
  changePasswordFrom!: FormGroup;
  getOtpModel!: GetOtpModel;
  changePasswordModel!: UserChangePassword;
  verifiedOtpModel!: OptVerifiedModel;
  otpResponseModel!: GetOtpResponseModel;

  get OtpModel(): GetOtpModel {
    return this.getOtpModel;
  }
  get VerifiedOtp(): OptVerifiedModel {
    return this.verifiedOtpModel;
  }
  get ChangePasswordModel(): UserChangePassword {
   return this.changePasswordModel;
  }

  IsGetOtp: boolean = true;
  IsVerified:boolean = false;
  IsConfirmPassword: boolean = false;
  constructor(private readonly fb: FormBuilder,
              private readonly _settingService: UserSettingService,
              private readonly toast: ToastrService ,
              readonly _authService: AuthService) {
              this.getOtpModel = new GetOtpModel();
              this.getOtpModel.IsResendOtp = false;
  }
  ngOnInit(): void {
  this.formInit();
  }
  getOtp() {
  if(this.getOtpModel.MobileNumber == undefined || this.getOtpModel.MobileNumber == null ) {
    this.toast.warning('Please enter mobile number !','Required');
    return ;
  } else {
    let subscription = this._settingService.GetOtp(this.OtpModel).subscribe(response => {
     subscription.unsubscribe();
     if(response.IsSuccess) {
      this.otpResponseModel = response.Data as GetOtpResponseModel;
      this.IsGetOtp = false;
      this.IsVerified = true;
      this.verifiedOtpModel = new OptVerifiedModel();
      this.verifiedOtpModel.MobileNumber = this.getOtpModel.MobileNumber;
     } else {
       this.toast.error(response.Message as string,'Server Error');
     }
    });
  }
  }
  resendOtp() {
  this.getOtpModel.IsResendOtp = true;
  let subscription = this._settingService.GetOtp(this.getOtpModel).subscribe(response => {
    subscription.unsubscribe();
    if(response.IsSuccess) {
      this.IsVerified = true;
      this.verifiedOtpModel = new OptVerifiedModel();
    } else {
    this.toast.info(response.Message as string , 'Information');
    return;
    }
  });
  }
  verifiedOtp(){
   if (this.VerifiedOtp.Otp == undefined || this.VerifiedOtp.Otp == null) {
   this.toast.warning('Please enter otp','Required');
   return ;
   } else {
    let subscription = this._settingService.VerifiedOtpp(this.verifiedOtpModel).subscribe(response => {
      subscription.unsubscribe();
      if(response.IsSuccess) {
        this.IsGetOtp = false;
        this.IsVerified = false;
        this.IsConfirmPassword = true;
        this.changePasswordModel = new UserChangePassword();
        this.changePasswordModel.UserId = this._authService.GetUserDetail()?.UserId as number ;
      } else {
      this.toast.error('User not verified successful','Unverified');
      return;
      }
    });
   }

  }
  changePassword() {
  if(this.ChangePasswordModel.Password === this.ChangePasswordModel.ConfirmPassword) {
    let subscription = this._settingService.ChangePassword(this.changePasswordModel).subscribe(response => {
      subscription.unsubscribe();
      if(response.IsSuccess) {
      this.toast.success('Password change successful', 'Success');
      this.changePasswordModel = new UserChangePassword();
      return;
      } else {
      this.toast.error(response.Message as string , 'Server Error');
      }
    });
  } else {
    this.toast.warning('Password and Confirm Password Mis Matched', 'Inccorect Password');
    return;
  }
  }
  formInit() {
    this.changePasswordFrom = this.fb.group({
    mobileNumber: [undefined, Validators.required],
    verifiedOtp: [undefined],
    newPassword: [undefined],
    confirmPassword: [undefined]
    });
  }
}