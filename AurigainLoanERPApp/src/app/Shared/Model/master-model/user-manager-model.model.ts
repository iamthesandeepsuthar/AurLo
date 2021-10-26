export class UserManagerModel {
        Id!: number;
        FullName!: string;
        DateOfBirth!: Date;
        Gender!: string ;
        FatherName!: string ;
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
        StateName!: string;
        AreaName!: string;
        Pincode!: string ;
        IsActive!: boolean ;
        IsDelete!: boolean;
        ModifiedDate!: Date;
        CreatedBy!: number | null;
        Setting!: string;
}
