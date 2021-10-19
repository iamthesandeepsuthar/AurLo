export class DoorStepAgentPostModel {
  constructor() {
    this.User = new UserPostModel();
    this.BankDetails = new UserBankDetailsPostModel();
    this.UserKYC = [];
    this.UserNominee = new UserNomineePostModel();
    //  this.ReportingPerson = new UserReportingPersonPostModel();
    this.Documents = [];
    //  this.SecurityDeposit = new UserSecurityDepositPostModel();
    this.SelfFunded = false;
  }
  Id!: number;
  FullName!: string;
  FatherName!: string;
  Gender!: string;
  QualificationId!: number;
  Address!: string;
  DistrictId!: number;
  StateId!: number;
  PinCode!: string;
  DateOfBirth!: string;
  SelfFunded: boolean;
  User!: UserPostModel;
  BankDetails!: UserBankDetailsPostModel;
  UserKYC: UserKYCPostModel[] = [];
  UserNominee!: UserNomineePostModel;
  ReportingPerson!: UserReportingPersonPostModel;
  Documents: DocumentPostModel[] = [];
  SecurityDeposit!: UserSecurityDepositPostModel;
}

export class UserSecurityDepositPostModel {
  constructor() {

  }
  Id!: number;
  PaymentModeId!: number;
  Amount!: string;
  CreditDate!: Date;
  ReferanceNumber!: string;
  AccountNumber!: string;
  BankName!: string;
}

export class DocumentPostModel {
  constructor() {
    // this.Id = 0;
    this.DocumentTypeId = 0;
    this.Files = [] as FilePostModel[];
  }
  Id!: number;
  DocumentTypeId: number;
  Files: FilePostModel[] = [];
}

export class FilePostModel {
  constructor() {
    // this.Id = 0;
    this.IsEditMode = false;
  }
  Id!: number;
  FileName!: string;
  File!: string;
  FileType!: string;
  IsEditMode: boolean;
}

export class UserReportingPersonPostModel {

  Id!: number;
  UserId!: number;
  ReportingUserId!: number;
}

export class UserNomineePostModel {
  constructor() {
    // this.Id = 0;
    this.IsSelfDeclaration = true;
  }
  Id!: number;
  NamineeName!: string;
  RelationshipWithNominee!: string;
  IsSelfDeclaration: boolean;
}

export class UserKYCPostModel {
  constructor() {
    // this.Id = 0;
  }
  Id!: number;
  Kycnumber!: string;
  KycdocumentTypeId!: number;
}

export class UserBankDetailsPostModel {
  constructor() {
    // this.Id = 0;
  }
  Id!: number;
  BankName!: string;
  AccountNumber!: string;
  Ifsccode!: string;
  Address!: string;
}

export class UserPostModel {
  constructor() {
    // this.Id = 0;
    this.UserRoleId = 0;
    this.IsApproved = false;

  }
  Id!: number;
  UserRoleId!: number;
  UserName!: string;
  Email!: string;
  Mobile!: string;
  IsApproved!: boolean;
  DeviceToken!: string;
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
  StateId: number;
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
  BankDetails: UserBankDetailViewModel;
  UserKYC: UserKYCViewModel[];
  UserNominee: UserNomineeViewModel;
  ReportingPerson: ReportingPersonViewModel;
  Documents: DocumentViewModel[];
  SecurityDeposit: UserSecurityDepositViewModel;
}

export interface UserSecurityDepositViewModel {
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

export interface PaymentModeViewModel {
  Id: number;
  Mode: string;
  MinimumValue: number;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
}

export interface DocumentViewModel {
  Id: number;
  DocumentTypeId: number;
  DocumentTypeName: string;

  UserId: number;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
  UserDocumentFiles: UserDocumentFileViewModel[];
}

export interface UserDocumentFileViewModel {
  Id: number;
  DocumentId: number;
  FileName: string;
  FileType: string;
  Path: string;
}

export interface ReportingPersonViewModel {
  Id: number;
  UserId: number;
  ReportingUserId: number;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
  ReportingUser: UserViewModel;
}

export interface UserNomineeViewModel {
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

export interface UserKYCViewModel {
  Id: number;
  Kycnumber: string;
  KycdocumentTypeId: number;
  KycdocumentTypeName: string;
  UserId: number;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
}

export interface UserBankDetailViewModel {
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

export interface UserViewModel {
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
  ProfilePath: string;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
}

export interface DoorStepAgentListModel {
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
  IsApproved: boolean;
  IsActive: boolean;
  IsDelete: boolean;
  CreatedOn: string;
  ModifiedOn: string;
  CreatedBy: number;
  ModifiedBy: number;
}
