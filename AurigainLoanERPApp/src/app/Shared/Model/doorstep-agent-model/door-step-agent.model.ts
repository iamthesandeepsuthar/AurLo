export interface DoorStepAgentPostModel   {
  Id: number;
  FullName: string;
  FatherName: string;
  UniqueId: string;
  Gender: string;
  QualificationId: number;
  Address: string;
  DistrictId: number;
  PinCode: string;
  DateOfBirth: string;
  ProfilePictureUrl: string;
  SelfFunded: boolean;
  IsActive: boolean;
  User: UserPostModel;
  BankDetails: BankDetailsPostModel;
  UserKYC: UserKYCPostModel[];
  UserNominee: UserNomineePostModel;
  ReportingPerson: ReportingPersonPostModel;
  Documents: DocumentPostModel[];
  SecurityDeposit: SecurityDepositPostModel;
}

interface SecurityDepositPostModel {
  Id: number;
  PaymentModeId: number;
  TransactionStatus: number;
  Amount: number;
  CreditDate: string;
  ReferanceNumber: string;
  AccountNumber: string;
  BankName: string;
  IsActive: boolean;
}

interface DocumentPostModel {
  Id: number;
  DocumentTypeId: number;
  IsActive: boolean;
  Files: File[];
}

interface FilePostModel {
  Id: number;
  FileName: string;
  File: string;
  FileType: string;
  IsEditMode: boolean;
}

interface ReportingPersonPostModel {
  Id: number;
  UserId: number;
  ReportingUserId: number;
}

interface UserNomineePostModel {
  Id: number;
  NamineeName: string;
  RelationshipWithNominee: string;
  IsActive: boolean;
}

interface UserKYCPostModel {
  Id: number;
  Kycnumber: string;
  KycdocumentTypeId: number;
  IsActive: boolean;
}

interface BankDetailsPostModel {
  Id: number;
  BankName: string;
  AccountNumber: string;
  Ifsccode: string;
  Address: string;
  IsActive: boolean;
}

interface UserPostModel {
  Id: number;
  UserRoleId: number;
  UserName: string;
  Password: string;
  Email: string;
  Mobile: string;
  IsApproved: boolean;
  DeviceToken: string;
  Token: string;
  IsActive: boolean;
}

