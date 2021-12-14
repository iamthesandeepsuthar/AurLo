
export class UserSettingPostModel {
  UserId!: number;
  ProfileBase64!: string;
  FileName!: string;
}

export interface LoginPostModel {
  MobileNumber: string;
  Password: string;
  Plateform: string;
}

export interface LoginResponseModel {
  UserId: number;
  RoleId: number;
  RoleLevel: number;
  RoleName: string;
  Token: string;
  UserName: string;

}
