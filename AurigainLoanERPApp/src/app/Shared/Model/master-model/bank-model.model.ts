
export class BankModel {
  Id!: number;
  Name!: string;
  BankLogoUrl!: string;
  ContactNumber!: string;
  WebsiteUrl!: string;
  FaxNumber!: string;
  IsActive!: boolean | null;
  IsDelete!: boolean;
  CreatedDate!: string;
  ModifiedDate!: string | null;
  Branches!: BranchModel[];
}

export class BranchModel {
  Id!: number;
  BranchName!: string;
  BranchCode!: string;
  Ifsc!: string;
  Address!: string;
  ContactNumber!: string;
  BranchEmailId!: string;
  BankId!: number;
  ConfigurationSettingJson!: string;
  IsActive!: boolean | null;
  IsDelete!: boolean;
  CreatedDate!: string;
  ModifiedDate!: string | null;
}

export class DDLBankModel {
  Id!: number;
  Name!: string;
}

export class DDLBranchModel {
  Id!: number;
  BranchName!: string;
  Ifsc!: string;
}
