import { UserPostModel, UserNomineePostModel, UserReportingPersonPostModel, DocumentPostModel, UserBankDetailsPostModel, UserKYCPostModel, DocumentViewModel, ReportingPersonViewModel, UserBankDetailViewModel, UserKYCViewModel, UserNomineeViewModel, UserSecurityDepositViewModel, UserViewModel } from "../doorstep-agent-model/door-step-agent.model";

export class Agent {
}
export interface AgentListModel {
  Id: number;
  UserId: number;
  RoleId: number;
  Role: string;
  Email: string;
  Mobile: string;
  FullName: string;
  FatherName: string;
  UniqueId: string;
  Gender: string;
  QualificationName: string;
  Address: string;
  DistrictName: string;
  StateName: string;
  PinCode: string;
  DateOfBirth: string;
  ProfilePictureUrl: string;
  IsActive: boolean;
  IsApproved: boolean;
  Mpin: string;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
  ReportingPersonName:string;
  ReportingPersonUserId:number;
}


export class AgentPostModel {
  Id!: number;
  FullName!: string;
  FatherName!: string;
  Gender!: string;
  QualificationId!: number;
  Address!: string;
  AddressLine2!:string;
  StateId!:number;
  DistrictId!: number | null;
  PinCode!: string;
  AreaPincodeId!: number;
  DateOfBirth!: string | null;
  IsActive!: boolean | null;
  User!: UserPostModel;
  Documents: DocumentPostModel[] = [];
  BankDetails!: UserBankDetailsPostModel;
  UserKYC: UserKYCPostModel[] = [];
  UserNominee!: UserNomineePostModel;
  ReportingPerson!: UserReportingPersonPostModel;
}


export interface AgentViewModel {
  Id: number;
  FullName: string;
  FatherName: string;
  UniqueId: string;
  Gender: string;
  QualificationId: number;
  QualificationName: string;
  Address: string;
  DistrictId: number | null;
  DistrictName: string;
  StateId : number ;
  StateName: string;
  PinCode: string;
  AreaPincodeId: number;
  DateOfBirth: string | null;
  ProfilePictureUrl: string;
  IsActive: boolean | null;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string | null;
  CreatedBy: number | null;
  ModifiedBy: number | null;
  Documents: DocumentViewModel[] | null;
  User: UserViewModel;
  BankDetails: UserBankDetailViewModel | null;
  UserKYC: UserKYCViewModel[] | null;
  UserNominee: UserNomineeViewModel | null;
  ReportingPerson: ReportingPersonViewModel | null;
}
