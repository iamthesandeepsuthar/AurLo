export class DocumentTypeModel {
  Id!: number;
  DocumentName!: string | null;
  IsActive!: boolean | null;
  IsDelete!: boolean;
  CreatedOn!: Date;
  DocumentNumberLength!: number;
  IsNumeric!:boolean;
  ModifiedOn!:Date | null;
}

export class DDLDocumentTypeModel {
  Id!: number;
  Name!: string | null;
}
