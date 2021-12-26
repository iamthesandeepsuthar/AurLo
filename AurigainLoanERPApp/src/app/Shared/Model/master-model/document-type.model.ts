export class DocumentTypeModel {
  Id!: number;
  DocumentName!: string | null;
  IsActive!: boolean | null;
  IsDelete!: boolean;
  CreatedOn!: Date;
  DocumentNumberLength!: number;
  IsNumeric!: boolean;
  ModifiedOn!: Date | null;
  IsKyc!: boolean;
  IsFreshLeadKyc!: boolean;
  IsBtleadKyc!: boolean;

  RequiredFileCount!: number;
}

export class DDLDocumentTypeModel {
  Id!: number;
  Name!: string | null;
  DocumentNumberLength!: number;
  IsNumeric!: boolean;
  IsKyc!: boolean;
  IsBtleadKyc!: boolean;
  IsFreshLeadKyc!: boolean;
  RequiredFileCount!: number;
}
