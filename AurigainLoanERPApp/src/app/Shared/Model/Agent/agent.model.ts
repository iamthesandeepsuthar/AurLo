import { UserPostModel, UserNomineePostModel, UserReportingPersonPostModel, DocumentPostModel, UserBankDetailsPostModel, UserKYCPostModel } from "../doorstep-agent-model/door-step-agent.model";

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
}


export class AgentPostModel {
  Id!: number;
  FullName!: string;
  FatherName!: string;
  Gender!: string;
  QualificationId!: number;
  Address!: string;
  StateId!:number;
  DistrictId!: number | null;
  PinCode!: string;
  DateOfBirth!: string | null;
  IsActive!: boolean | null;
  User!: UserPostModel;
  Documents: DocumentPostModel[] = [];
  BankDetails!: UserBankDetailsPostModel;
  UserKYC: UserKYCPostModel[] = [];
  UserNominee!: UserNomineePostModel;
  ReportingPerson!: UserReportingPersonPostModel;


}
