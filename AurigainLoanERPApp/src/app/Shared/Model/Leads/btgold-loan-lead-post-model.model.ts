
export class BTGoldLoanLeadPostModel {
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
