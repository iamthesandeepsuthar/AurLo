
export class BTGoldLoanLeadPostModel {
  constructor() {
    this.AddressDetail = new BtGoldLoanLeadAddressPostModel();
    this.AppointmentDetail = new BtGoldLoanLeadAppointmentPostModel();
    this.DocumentDetail = new BtGoldLoanLeadDocumentPostModel();
    this.ExistingLoanDetail = new BtGoldLoanLeadExistingLoanPostModel();
    this.JewelleryDetail = [];
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
  PurposeId!: number;
  LoanAmount!: number | null;
  LoanAccountNumber!: string;
  LeadSourceByuserId!: number;
  AddressDetail!: BtGoldLoanLeadAddressPostModel;
  AppointmentDetail!: BtGoldLoanLeadAppointmentPostModel;
  DocumentDetail!: BtGoldLoanLeadDocumentPostModel;
  ExistingLoanDetail!: BtGoldLoanLeadExistingLoanPostModel;
  JewelleryDetail!: BtGoldLoanLeadJewelleryDetailPostModel[];
  KYCDetail!: BtGoldLoanLeadKYCDetailPostModel;
}

export class BtGoldLoanLeadAddressPostModel {
  Id!: number;
  Address!: string;
  AddressLine2!: string;
  AeraPincodeId!: number | null;
  CorrespondAddress!: string;
  CorrespondAddressLine2!: string;
  CorrespondAeraPincodeId!: number | null;
}

export class BtGoldLoanLeadAppointmentPostModel {
  Id!: number;
  BranchId!: number;
  AppointmentDate!: string | null;
  AppointmentTime!: string;
  LeadId!: number;
}

export class BtGoldLoanLeadDocumentPostModel {
  Id!: number;
  CustomerPhoto!: FileModel | null;
  KycDocumentPoi!: FileModel[] | null;
  KycDocumentPoa!: FileModel[] | null;
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
  PoidocumentNumber!: string | null;
  PoadocumentTypeId!: number;
  PoadocumentNumber!: string | null;
  PANNumber !: string | null;
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
  IsInternalLead!: boolean;
  ProductId!: number;
  ProductName!: string;
  CreatedDate!: string;
  Pincode!: string;
  IsActive!: boolean;
  IsDelete!: boolean;
  IsStatusCompleted!: boolean;
  ApprovedStage!: number;
  LeadStatus!: string;
  ApprovalStatus!: string;
  LoanCaseNumber!: string;
  LeadStatusId!: number;
  ApprovalStatusId!:number;
}
export class BTGoldLoanLeadViewModel {
  Id!: number;
  ProductId!: number;
  ProductCategoryName!: string;
  LoanCaseNumber!: string;
  FullName!: string;
  FatherName!: string;
  Gender!: string;
  DateOfBirth!: string;
  Profession!: string;
  Mobile!: string;
  EmailId!: string;
  IsInternalLead!: boolean;
  CustomerUserId!: number;
  SecondaryMobile!: string;
  Purpose!: string;
  LoanAmount!: number;
  LoanAccountNumber!: string;
  LeadSourceByuserId!: number;
  DetailAddress!: AddressDetail;
  AppointmentDetail!: AppointmentDetail;
  DocumentDetail!: DocumentDetail;
  ExistingLoanDetail!: ExistingLoanDetail;
  JewelleryDetail!: JewelleryDetail[];
  KYCDetail!: KYCDetail;
  LeadSourceByuserName!: string;
  ProductName!: string;
  CustomerUserName!: string;
  PurposeId!: number;
  PurposeName!: string;
}

export class KYCDetail {
  Id!: number;
  PoidocumentTypeId!: number;
  PoidocumentType!: string;
  PoidocumentNumber!: string;
  PoadocumentTypeId!: number;
  PoadocumentNumber!: string;
  PoadocumentType!: string;
  PANNumber!: string;
}

export class JewelleryDetail {
  Id!: number;
  JewelleryTypeId!: number;
  JewelleryType!: string;
  Quantity!: number;
  Weight!: number;
  Karats!: number;
}

export class ExistingLoanDetail {
  Id!: number;
  BankName!: string;
  Amount!: number;
  Date!: string;
  JewelleryValuation!: number;
  OutstandingAmount!: number;
  BalanceTransferAmount!: number;
  RequiredAmount!: number;
  Tenure!: number;
}

export class DocumentDetail {
  Id!: number;
  CustomerPhoto!: string;
  KycDocumentPoi!: string[];
  KycDocumentPoa!: string[];
  BlankCheque1!: string;
  BlankCheque2!: string;
  LoanDocument!: string;
  AggrementLastPage!: string;
  PromissoryNote!: string;
  AtmwithdrawalSlip!: string;
  ForeClosureLetter!: string;
}

export class AppointmentDetail {
  Id!: number;
  BranchId!: number;
  BranchName!: string;
  BankId!: number;
  BankName!: string;
  Ifsc!: string;
  Pincode!: string;
  AppointmentDate!: string;
  AppointmentTime!: string;
}

export class AddressDetail {
  Id!: number;
  Address!: string;
  AeraPincodeId!: number;
  AreaName!: string;
  PinCode!: string;
  State!: string;
  District!: string;
  CorrespondAddress!: string;
  CorrespondAeraPincodeId!: number;
  CorrespondAreaName!: string;
  CorrespondPinCode!: string;
  CorrespondState!: string;
  CorrespondDistrict!: string;
}
export class BtGoldLoanLeadApprovalStagePostModel {
  LeadId!: number;
  ApprovalStatus!: number | any;
  Remarks!: string;
}

export class BtGoldLoanLeadApprovalStageViewModel {
  Id!: number;
  LeadApproveByUser!: string;
  LeadApproveByUserId!: number;
  LeadId!: number;
  ApprovalStatus!: number | null;
  Remarks!: string;
  ActionDate!: Date;
}

