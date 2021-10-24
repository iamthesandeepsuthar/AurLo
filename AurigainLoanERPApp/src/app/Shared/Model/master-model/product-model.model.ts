import { ProductCategoryModel } from "./product-category-model.model";

export class ProductModel {
  Id!: number;
  Name!: string;
  Notes!: string;
  ProductCategoryId!: number;
  MinimumAmount!: number | null;
  MaximumAmount!: number | null;
  InterestRate!: number | null;
  ProcessingFee!: number | null;
  InterestRateApplied!: boolean | null;
  MinimumTenure!: number | null;
  MaximumTenure!: number | null;
  IsActive!: boolean;
  IsDelete!: boolean;
  CreatedDate!: string;
  CreatedBy!: number | null;
  Category!: ProductCategoryModel;
}

export class DDLProductModel {
  Id!: number;
  Name!: string;
}
