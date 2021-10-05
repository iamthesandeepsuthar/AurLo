export class PaymentModeModel {
  Id!: number;
        Mode!: string | null;
        MinimumValue!: number| null;
        IsActive!: boolean | null;
        IsDelete!: boolean;
        CreatedOn!: Date;
        ModifiedOn!: Date | null;
}

export class DDLPaymentModeModel {
  Id!: number;
  Mode!: string | null;
}
