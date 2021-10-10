export class DoorStepAgentPostModel {
  constructor() {
    this.User = new UserPostModel();
    this.BankDetails = new UserBankDetailsPostModel();
    this.UserKYC = [];
    this.UserNominee = new UserNomineePostModel();
    //  this.ReportingPerson = new UserReportingPersonPostModel();
    this.Documents = [];
    //  this.SecurityDeposit = new UserSecurityDepositPostModel();
    this.SelfFunded=false;
  }
  Id!: number;
  FullName!: string;
  FatherName!: string;
  UniqueId!: string;
  Gender!: string;
  QualificationId!: number;
  Address!: string;
  DistrictId!: number;
  StateId!: number;
  PinCode!: string;
  DateOfBirth!: string;
  ProfilePictureUrl!: string;
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
    this.Id = 0;
    this.PaymentModeId = 0;
    this.TransactionStatus = 0;
    this.Amount = 0;
  }
  Id!: number;
  PaymentModeId!: number;
  TransactionStatus!: number;
  Amount!: number;
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
