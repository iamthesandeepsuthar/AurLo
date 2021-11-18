export class GoldLoanFreshLeadModel {
  constructor() {
    this.KycDocument = new GoldLoanFreshLeadKycDocumentModel();
    this.AppointmentDetail = new GoldLoanFreshLeadAppointmentDetailModel();
    this.JewelleryDetail = new GoldLoanFreshLeadJewelleryDetailModel();
  }
  Id!: number;
  FullName!: string;
  CustomerUserId!: number;
  FatherName!: string;
  DateOfBirth!: string;
  Gender!: string;
  Email!: string;
  PrimaryMobileNumber!: string;
  SecondaryMobileNumber!: string;
  LoanAmountRequired!: number | null;
  Purpose!: string;
  LeadSourceByUserId!: number;
  ProductId!: number;
  CreatedDate!: string;
  IsActive!: boolean;
  KycDocument!: GoldLoanFreshLeadKycDocumentModel;
  AppointmentDetail!: GoldLoanFreshLeadAppointmentDetailModel;
  JewelleryDetail!: GoldLoanFreshLeadJewelleryDetailModel;
}


export class GoldLoanFreshLeadAppointmentDetailModel {
  Id!: number;
  BankId!: number;
  BranchId!: number;
  AppointmentDate!: string | null;
  AppointmentTime!: string;
  GlfreshLeadId!: number;
  IsActive!: boolean | null;
  CreatedDate!: string;
}

export class GoldLoanFreshLeadJewelleryDetailModel {
  Id!: number;
  PreferredLoanTenure!: number | null;
  JewelleryTypeId!: number;
  Quantity!: number | null;
  Weight!: number | null;
  Karat!: number | null;
  GlfreshLeadId!: number;
  IsActive!: boolean;
  CreatedDate!: string;
}

export class GoldLoanFreshLeadKycDocumentModel {
  Id!: number;
  KycDocumentTypeId!: number;
  DocumentNumber?: string;
  PanNumber!: string;
  PincodeAreaId!: number;
  AddressLine1!: string;
  AddressLine2!: string;
  IsActive!: boolean | null;
  CreatedDate!: string;
  GlfreshLeadId!: number;
}

export class GoldLoanFreshLeadListModel {
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
}

//#region  <<Gold Loan Fresh Lead View Model>>
export class GoldLoanFreshLeadViewModel {
  constructor() {
    this.AppointmentDetail = new GoldLoanFreshLeadAppointmentDetailViewModel();
    this.JewelleryDetail = new GoldLoanFreshLeadJewelleryDetailViewModel();

  }
  Id!: number;
  FullName!: string;
  FatherName!: string;
  DateOfBirth!: string;
  Gender!: string;
  Email!: string;
  CustomerUserId!: number | null;
  PrimaryMobileNumber!: string;
  SecondaryMobileNumber!: string;
  LoanAmountRequired!: number;
  Purpose!: string;
  LeadSourceByUserId!: number;
  LeadSourceUserName!: string;
  ProductId!: number;
  ProductName!: string;
  ProductCategoryName!: string;
  CreatedDate!: string;
  IsActive!: boolean | null;
  KycDocument!: GoldLoanFreshLeadKycDocumentViewModel;
  AppointmentDetail!: GoldLoanFreshLeadAppointmentDetailViewModel;
  JewelleryDetail!: GoldLoanFreshLeadJewelleryDetailViewModel;
}

export class GoldLoanFreshLeadKycDocumentViewModel {
  Id!: number;
  KycDocumentTypeId!: number;
  KycDocumentTypeName!: string;
  DocumentNumber!: string;
  PanNumber!: string;
  PincodeAreaId!: number;
  PincodeAreaName!: string;
  DistrictName!: string;
  StateName!: string;
  Pincode!: string;
  AddressLine1!: string;
  GlfreshLeadId!: number;
}

export class GoldLoanFreshLeadAppointmentDetailViewModel {
  Id!: number;
  BankId!: number;
  BankName!: string;
  BranchId!: number;
  BranchName!: string;
  Pincode!: string;
  IFSC!: string;
  AppointmentDate!: string | null;
  AppointmentTime!: string;
}

export class GoldLoanFreshLeadJewelleryDetailViewModel {
  Id!: number;
  PreferredLoanTenure!: number;
  JewelleryTypeId!: number;
  JewelleryTypeName!: string;
  Quantity!: number;
  Weight!: number | null;
  Karat!: number | null;
}
//#endregion
