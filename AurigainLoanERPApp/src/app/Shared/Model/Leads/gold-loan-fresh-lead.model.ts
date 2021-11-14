export class GoldLoanFreshLeadModel {
  Id!: number;
  FullName!: string;
  FatherName!: string;
  DateOfBirth!: string;
  Gender!: string;
  Email!: string;
  PrimaryMobileNumber!: string;
  SecondaryMobileNumber!: string;
  LoanAmountRequired!: number;
  Purpose!: string;
  LeadSourceByUserId!: number;
  ProductId!: number;
  CreatedDate!: string;
  IsActive!: boolean | null;
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
  PreferredLoanTenure!: number;
  JewelleryTypeId!: number;
  Quantity!: number;
  Weight!: number | null;
  Karat!: number | null;
  GlfreshLeadId!: number;
  IsActive!: boolean | null;
  CreatedDate!: string;
}

export class GoldLoanFreshLeadKycDocumentModel {
  Id!: number;
  KycDocumentTypeId!: number;
  DocumentNumber!: string;
  PanNumber!: string;
  PincodeAreaId!: number;
  AddressLine1!: string;
  AddressLine2!: string;
  IsActive!: boolean | null;
  CreatedDate!: string;
  GlfreshLeadId!: number;
}

export interface GoldLoanFreshLeadListModel {
  Id: number;
  FullName: string;
  FatherName: string;
  DateOfBirth: string;
  Gender: string;
  PrimaryMobileNumber: string;
  LeadSourceByUserId: number;
  LeadSourceByUserName: string;
  LoanAmountRequired: number;
  ProductId: number;
  ProductName: string;
  CreatedDate: string;
  Pincode: string;
  IsActive: boolean;
}
