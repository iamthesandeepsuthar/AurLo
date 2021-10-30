import { ProductCategoryModel } from "./product-category-model.model";

export class ProductModel {
  Id!: number;
  Name!: string;
  Notes!: string;
  ProductCategoryId!: number;
  BankId!: number;
  MinimumAmount!: number;
  MaximumAmount!: number;
  InterestRate!: number;
  ProcessingFee!: number;
  InterestRateApplied!: boolean;
  MinimumTenure!: number ;
  MaximumTenure!: number;
  IsActive!: boolean;
  IsDelete!: boolean;
  CreatedDate!: string;
  CreatedBy!: number;
  ProductCategoryName!:string;
  BankName!: string;
}

export class DDLProductModel {
  Id!: number;
  Name!: string;
}

