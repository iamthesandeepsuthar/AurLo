export class BalanceTransferReturnPostModel {
  constructor() {
  this.ChequeDetail = new BalanceTransferReturnBankChequeDetail();
  }
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
  LoanAmount!:number;
  ChequeDetail!: BalanceTransferReturnBankChequeDetail;
}
export class BalanceTransferReturnBankChequeDetail {
  constructor() {
    this.ChequeImageUrl = new FilePostModel();
  }
  Id!: number;
  ChequeNumber!: string;
  ChequeImageUrl: FilePostModel;
}
export class FilePostModel {
  Id!:number;
  FileName!:string;
  File !:string;
  FileType!:string;
  IsEditMode!:boolean;
}
// Lead Detail Model
export class  BalanceTransferReturnViewModel {
  constructor() {
    this.BalanceTransferReturn = new BalanceTranferReturnViewModel();
  }
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
  constructor() {
    this.AmountPainToExistingBank = false;
    this.GoldReceived = false;
    this.GoldSubmittedToBank = false;
  }
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
