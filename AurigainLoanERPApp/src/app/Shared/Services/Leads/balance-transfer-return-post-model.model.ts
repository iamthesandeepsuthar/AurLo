export class BalanceTransferReturnPostModel {
  BtReturnId!: number;
  LeadId!: number;
  AmountPainToExistingBank!: boolean | null;
  GoldReceived!: boolean | null;
  GoldSubmittedToBank!: boolean | null;
  LoanDisbursment!: boolean | null;
  LoanAccountNumber!: string;
  BankName!: string;
  PaymentMethod!: number | null;
  CustomerName!: string;
  Remarks!: string;
  UtrNumber!: string;
  AmountReturn!: number | null;
  FinalPaymentDate!: string;
  ChequeDetail!: BalanceTransferReturnBankChequeDetail;
}
export interface BalanceTransferReturnBankChequeDetail {
  Id: number;
  ChequeNumber: string;
  ChequeImageUrl: string;
  File: FilePostModel;
}
export class FilePostModel {
  Id!:number;
  FileName!:string;
  File !:string;
  FileType!:string;
  IsEditMode!:boolean;
}
// Lead Detail Model
export class BalanceTransferReturnViewModel {
  Id!: number;
  ProductId!: number;
  ProductCategoryName!: string;
  FullName!: string;
  Mobile!: string;
  LoanAmount!: number;
  LoanCaseNumber!: string;
  LeadSourceByuserId!: number;
  LeadSourceByuserName!: string;
  ProductName!: string;
  IsInternalLead!: boolean;
  ExistingLoanDetail!: BtGoldLoanLeadExistingLoanViewModel;
  JewelleryDetail!: BtGoldLoanLeadJewelleryDetailViewModel;
  BalanceTransferReturn!: BalanceTranferReturnViewModel;
}
export class BalanceTranferReturnViewModel {
  Id!: number;
  LeadId!: number;
  AmountPainToExistingBank!: boolean | null;
  GoldReceived!: boolean | null;
  GoldSubmittedToBank!: boolean | null;
}
export class BtGoldLoanLeadJewelleryDetailViewModel {
  Id!: number;
  JewelleryTypeId!: number | null;
  JewelleryType!: string;
  Quantity!: number | null;
  Weight!: number | null;
  Karats!: number | null;
}
export class BtGoldLoanLeadExistingLoanViewModel {
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
