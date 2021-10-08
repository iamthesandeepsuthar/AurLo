export interface DoorStepAgentPostModel {
  
  Id: number;
  FullName: string;
  FatherName: string;
  UniqueId: string;
  Gender: string;
  QualificationId: number;
  Address: string;
  DistrictId: number;
  StateId: number;
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

export interface SecurityDepositPostModel {
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

export interface DocumentPostModel {
  Id: number;
  DocumentTypeId: number;
  IsActive: boolean;
  Files: File[];
}

export interface FilePostModel {
  Id: number;
  FileName: string;
  File: string;
  FileType: string;
  IsEditMode: boolean;
}

export interface ReportingPersonPostModel {
  Id: number;
  UserId: number;
  ReportingUserId: number;
}

export interface UserNomineePostModel {
  Id: number;
  NamineeName: string;
  RelationshipWithNominee: string;
  IsActive: boolean;
}

export interface UserKYCPostModel {
  Id: number;
  Kycnumber: string;
  KycdocumentTypeId: number;
  IsActive: boolean;
}

export interface BankDetailsPostModel {
  Id: number;
  BankName: string;
  AccountNumber: string;
  Ifsccode: string;
  Address: string;
  IsActive: boolean;
}

export interface UserPostModel {
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




export interface DoorStepAgentViewModel {
  Id: number;
  UserId: number;
  FullName: string;
  FatherName: string;
  UniqueId: string;
  SelfFunded: boolean;
  Gender: string;
  QualificationId: number;
  QualificationName: string;
  Address: string;
  PinCode: string;
  DistrictId: number;
  DistrictName: string;
  StateName: string;
  DateOfBirth: string;
  ProfilePictureUrl: string;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
  SecurityDepositId: number;
  User: UserViewModel;
  BankDetails: BankDetailViewModel;
  UserKYC: UserKYCViewModel[];
  UserNominee: UserNomineeViewModel;
  ReportingPerson: ReportingPersonViewModel;
  Documents: DocumentViewModel[];
  SecurityDeposit: SecurityDepositViewModel;
}

interface SecurityDepositViewModel {
  Id: number;
  UserId: number;
  PaymentModeId: number;
  TransactionStatus: number;
  Amount: number;
  CreditDate: string;
  ReferanceNumber: string;
  AccountNumber: string;
  BankName: string;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedDate: string;
  PaymentMode: PaymentModeViewModel;
}

interface PaymentModeViewModel {
  Id: number;
  Mode: string;
  MinimumValue: number;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
}

interface DocumentViewModel {
  Id: number;
  DocumentTypeId: number;
  UserId: number;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
  UserDocumentFiles: UserDocumentFileViewModel[];
}

interface UserDocumentFileViewModel {
  Id: number;
  DocumentId: number;
  FileName: string;
  FileType: string;
  Path: string;
}

interface ReportingPersonViewModel {
  Id: number;
  UserId: number;
  ReportingUserId: number;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
  ReportingUser: UserViewModel;
}

interface UserNomineeViewModel {
  Id: number;
  NamineeName: string;
  RelationshipWithNominee: string;
  UserId: number;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
}

interface UserKYCViewModel {
  Id: number;
  Kycnumber: string;
  KycdocumentTypeId: number;
  UserId: number;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
}

interface BankDetailViewModel {
  Id: number;
  BankName: string;
  AccountNumber: string;
  Ifsccode: string;
  Address: string;
  UserId: number;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
}

interface UserViewModel {
  Id: number;
  UserRoleId: number;
  UserRoleName: string;
  UserName: string;
  MPin: string;
  Email: string;
  Mobile: string;
  IsApproved: boolean;
  DeviceToken: string;
  Token: string;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
}
