export class DistrictModel {
  Id!: number;
  Name!: string | null;
  StateId!: number;
  Pincode!: string | null;
  StateName!: string | null;
  IsActive!: boolean | null;
  IsDelete!: boolean;
  CreatedOn!: Date;
  ModifiedOn!: Date | null;
}
export class DDLDistrictModel {
  Id!: number;
  Name!: string | null;
  Pincode!: string | null;
}
