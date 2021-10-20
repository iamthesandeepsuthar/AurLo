export class UserManagerModel {
        Id!: number;
        FullName!: string;
        DateOfBirth!: Date;
        Gender!: string ;
        FatherName!: string ;
        EmailId!: string ;
        MobileNumber!: string;
        Password!: string;
        IsApproved!: boolean;
        Address!: string ;
        UserId!:number;
        DistrictId!: number;
        Pincode!: string ;
        IsActive!: boolean ;
        IsDelete!: boolean;
        ModifiedDate!: Date;
        CreatedBy!: number | null;
        Setting!: string;
}
