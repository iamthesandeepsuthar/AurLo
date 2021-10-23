
export class JewellaryTypeModel {
  Id!: number;
  Name!: string;
  Description!: string;
  IsActive!: boolean | null;
  IsDelete!: boolean | null;
  CreatedOn!: string;
  ModifiedOn!: string | null;
  CreatedBy!: number | null;
}

export class DDLJewellaryType {
  Name!: string;
  Id!: number;
}
