
export class BTGoldLoanLeadPostModel {
  constructor() {
    this.AddressDetail = new BtGoldLoanLeadAddressPostModel();
    this.AppointmentDetail = new BtGoldLoanLeadAppointmentPostModel();
    this.DocumentDetail = new BtGoldLoanLeadDocumentPostModel();
    this.ExistingLoanDetail = new BtGoldLoanLeadExistingLoanPostModel();
    this.JewelleryDetail = new BtGoldLoanLeadJewelleryDetailPostModel();
    this.KYCDetail = new BtGoldLoanLeadKYCDetailPostModel();
  }
  Id!: number;
  ProductId!: number;
  FullName!: string;
  FatherName!: string;
  Gender!: string;
  DateOfBirth!: string;
  Profession!: string;
  Mobile!: string;
  EmailId!: string;
  CustomerUserId!: number;
  SecondaryMobile!: string;
  Purpose!: string;
  LoanAmount!: number;
  LoanAccountNumber!: string;
  LeadSourceByuserId!: number;
  AddressDetail!: BtGoldLoanLeadAddressPostModel;
  AppointmentDetail!: BtGoldLoanLeadAppointmentPostModel;
  DocumentDetail!: BtGoldLoanLeadDocumentPostModel;
  ExistingLoanDetail!: BtGoldLoanLeadExistingLoanPostModel;
  JewelleryDetail!: BtGoldLoanLeadJewelleryDetailPostModel;
  KYCDetail!: BtGoldLoanLeadKYCDetailPostModel;
}

export class BtGoldLoanLeadAddressPostModel {
  Id!: number;
  Address!: string;
  AeraPincodeId!: number | null;
  CorrespondAddress!: string;
  CorrespondAeraPincodeId!: number | null;
}

export class BtGoldLoanLeadAppointmentPostModel {
  Id!: number;
  BranchId!: number | null;
  AppointmentDate!: string | null;
  AppointmentTime!: string;
}

export class BtGoldLoanLeadDocumentPostModel {
  Id!: number;
  CustomerPhoto!: FileModel | null;
  KycDocumentPoi!: FileModel | null;
  KycDocumentPoa!: FileModel | null;
  BlankCheque1!: FileModel | null;
  BlankCheque2!: FileModel | null;
  LoanDocument!: FileModel | null;
  AggrementLastPage!: FileModel | null;
  PromissoryNote!: FileModel | null;
  AtmwithdrawalSlip!: FileModel | null;
  ForeClosureLetter!: FileModel | null;
}

export class BtGoldLoanLeadExistingLoanPostModel {
  Id!: number;
  BankName!: string;
  Amount!: number | null;
  Date!: string | null;
  JewelleryValuation!: number | null;
  OutstandingAmount!: number | null;
  BalanceTransferAmount!: number | null;
  RequiredAmount!: number | null;
  Tenure!: number | null;
}

export class BtGoldLoanLeadJewelleryDetailPostModel {
  Id!: number;
  JewelleryTypeId!: number | null;
  Quantity!: number | null;
  Weight!: number | null;
  Karats!: number | null;
}

export class BtGoldLoanLeadKYCDetailPostModel {
  Id!: number;
  PoidocumentTypeId!: number;
  PoidocumentNumber!: string;
  PoadocumentTypeId!: number;
  PoadocumentNumber!: string;
}

export class FileModel {
  FileName!: string;
  File!: string;
  FileType!: string;
  IsEditMode!: boolean;
}
export class BTGoldLoanLeadListModel {
  Id!: number;
  FullName!: string;
  FatherName!: string;
  DateOfBirth!: string;
  Gender!: string;
  Email!: string;
  PrimaryMobileNumber!: string;
  LeadSourceByUserId!: number;
  LeadSourceByUserName!: string;
  LoanAmountRequired!: number;
  ProductId!: number;
  ProductName!: string;
  CreatedDate!: string;
  Pincode!: string;
  IsActive!: boolean;
  IsDelete!: boolean;
}
export interface BTGoldLoanLeadViewModel {
  Id: number;
  ProductId: number;
  FullName: string;
  FatherName: string;
  Gender: string;
  DateOfBirth: string;
  Profession: string;
  Mobile: string;
  EmailId: string;
  CustomerUserId: number;
  SecondaryMobile: string;
  Purpose: string;
  LoanAmount: number;
  LoanAccountNumber: string;
  LeadSourceByuserId: number;
  AddressDetail: AddressDetail;
  AppointmentDetail: AppointmentDetail;
  DocumentDetail: DocumentDetail;
  ExistingLoanDetail: ExistingLoanDetail;
  JewelleryDetail: JewelleryDetail;
  KYCDetail: KYCDetail;
  LeadSourceByuserName: string;
  ProductName: string;
  CustomerUserName: string;
}

export interface KYCDetail {
  Id: number;
  PoidocumentTypeId: number;
  PoidocumentType: string;
  PoidocumentNumber: string;
  PoadocumentTypeId: number;
  PoadocumentNumber: string;
  PoadocumentType: string;
}

export interface JewelleryDetail {
  Id: number;
  JewelleryTypeId: number;
  JewelleryType: string;
  Quantity: number;
  Weight: number;
  Karats: number;
}

export interface ExistingLoanDetail {
  Id: number;
  BankName: string;
  Amount: number;
  Date: string;
  JewelleryValuation: number;
  OutstandingAmount: number;
  BalanceTransferAmount: number;
  RequiredAmount: number;
  Tenure: number;
}

export interface DocumentDetail {
  Id: number;
  CustomerPhoto: string;
  KycDocumentPoi: string;
  KycDocumentPoa: string;
  BlankCheque1: string;
  BlankCheque2: string;
  LoanDocument: string;
  AggrementLastPage: string;
  PromissoryNote: string;
  AtmwithdrawalSlip: string;
  ForeClosureLetter: string;
}

export interface AppointmentDetail {
  Id: number;
  BranchId: number;
  BranchName: string;
  BankName: string;
  AppointmentDate: string;
  AppointmentTime: string;
}

export interface AddressDetail {
  Id: number;
  Address: string;
  AeraPincodeId: number;
  AreaName: string;
  PinCode: string;
  State: string;
  District: string;
  CorrespondAddress: string;
  CorrespondAeraPincodeId: number;
  CorrespondAreaName: string;
  CorrespondPinCode: string;
  CorrespondState: string;
  CorrespondDistrict: string;
}
