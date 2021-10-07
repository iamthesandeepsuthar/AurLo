export class StateModel {
  Id!: number;
        Name!: string | null;
        IsActive!: boolean | null;
        IsDelete!: boolean;
        CreatedOn!: Date;
        ModifiedOn!: Date | null;
}
 export class DDLStateModel {
  Id!: number;
  Name!: string | null;
}
