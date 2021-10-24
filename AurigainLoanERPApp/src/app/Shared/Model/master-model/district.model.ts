export class DistrictModel {
  constructor() {
    this.Areas = [] as PincodeAreaModel[];
  }
  Id!: number;
  Name!: string | null;
  StateId!: number;
  Pincode!: string;
  StateName!: string | null;
  IsActive!: boolean | null;
  IsDelete!: boolean;
  CreatedOn!: Date;
  ModifiedOn!: Date | null;
  Areas!: PincodeAreaModel[];
}
export class DDLDistrictModel {
  Id!: number;
  Name!: string | null;
  Pincode!: string | null;
}
export class PincodeAreaModel {
  Id!: number;
  Pincode!: string;
  AreaName!: string;
  IsActive!: boolean | null;
  IsDelete!: boolean;
  DistrictId!: number;
}
