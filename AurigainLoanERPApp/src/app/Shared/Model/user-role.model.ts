export class UserRoleModel {
    id!: number;
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
    id!: number;
    name!: string;
    parentId!: number | null;
    isActive!: boolean;
}