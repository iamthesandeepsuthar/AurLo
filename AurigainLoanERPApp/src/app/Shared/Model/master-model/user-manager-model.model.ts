export class UserManagerModel {
        Id!: number;
        FullName!: string;
        DateOfBirth!: Date;
        Gender!: string ;
        FatherName!: string ;
        UniqueId!:string;
        RoleId!: number;
        EmailId!: string ;
        Mobile!: string;
        IsWhatsApp!: boolean;
        Password!: string;
        IsApproved!: boolean;
        Address!: string ;
        UserId!:number;
        DistrictId!: number;
        DistrictName!: string;
        StateId!: number;
        StateName!: string;
        AreaPincodeId!:number;
        AreaName!: string;
        Pincode!: string ;
        IsActive!: boolean ;
        IsDelete!: boolean;
        ModifiedDate!: Date;
        CreatedBy!: number | null;
        Setting!: string;
        Mpin!:string;
        RoleName!: string;
}

export class ReportingUser {
  UserId!: number;
  Name!: string;
  RoleId!: number;
  RoleName!: string;
}

