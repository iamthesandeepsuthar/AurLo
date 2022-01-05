export enum FixedValueEnum {
}

export enum DocumentTypeEnum {
  AadhaarCard = 1,
  PANCard = 2,
  CancelledCheque = 3,
}

export enum UserRoleEnum {
  SuperAdmin = 1,
  Admin = 2,
  ZonalManager = 3,
  Supervisor = 4,
  Agent = 5,
  DoorStepAgent = 6,
  Customer = 7,
  Operator = 8,
}


export enum ProductCategoryEnum {
  GoldLoan = 1,
  PersonalLoan = 2,
  CarLoan = 3,
  HomeLoan = 4,
}

export enum PaymentMethod {
  NEFT=2,
  RTGS=1,
  IMPS=3,
  CHEQUE=4,
}
export enum LeadStatusEnum {
  New = 1,
  Pending = 2,
  AmountTransfer = 3,
  GoldReached = 4,
  BTReturnReady = 5,
  Completed = 6,
  Rejected=7
}
export enum LeadApprovalStatusEnum {
  Pending=1,
  Approved=2,
  Rejected=3
}
// Personal , Home , vehicle Loan Fresh Lead 
export enum LeadTypeEnum{
  Salaried=0,
  SelfEmployed =1
}
