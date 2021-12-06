export class UserChangePassword {
 UserId!:number;
 Password!: string;
 ConfirmPassword!: string;
}
export class OptVerifiedModel {
  MobileNumber!: string;
  Otp!:string;
}
export class GetOtpModel{
 MobileNumber!: string;
 IsResendOtp!: boolean;
}
export class GetOtpResponseModel {
  SessionStartOn!: Date;
  ExpireOn!:Date;
}
