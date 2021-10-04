export class UserRoleModel {
    Id!: number;
    Name!: string;
    ParentId!: number | null;
    IsActive!: boolean | null;
    IsDelete!: boolean;
    CreatedOn!: string;
    ModifiedOn!: string | null;
    CreatedBy!: number;
    ModifiedBy!: number | null;
}
export class UserRolePostModel {
    Id!: number | string;
    Name!: string;
    ParentId!: number | string|null;
    IsActive!: boolean;
}