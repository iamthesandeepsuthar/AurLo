import { UserKYCPostModel, UserPostModel } from "../doorstep-agent-model/door-step-agent.model";


export class CustomerRegistrationModel {
  constructor() {
    this.User = new UserPostModel();
    this.KycDocuments = [];
  }
  Id!: number;
  FullName!: string;
  FatherName!: string;
  Gender!: string;
  UserId!: number;
  PincodeAreaId!: number | null;
  Address!: string;
  DateOfBirth!: string | null;
  IsActive!: boolean | null;
  IsDelete!: boolean;
  CreatedOn!: string;
  ModifiedOn!: string | null;
  CreatedBy!: number | null;
  ModifiedBy!: number | null;
  User!: UserPostModel;
  KycDocuments!:UserKYCPostModel[];
}

export class CustomerRegistrationViewModel {
  Id!: number;
  FullName!: string;
  FatherName!: string;
  Gender!: string;
  UserId!: number;
  EmailId!: string;
  Mobile!: string;
  Pincode!: string;
  State!: string;
  District!: string;
  AreaName!: string;
  Address!: string;
  DateOfBirth!: string | null;
  IsActive!: boolean | null;
  CreatedOn!: string;
  KycDocuments!: CustomerKycViewModel[];
}

export class CustomerKycViewModel {
  Id!: number;
  Kycnumber!: string;
  KycdocumentTypeId!: number;
  KycdocumentTypeName!: string;
  UserId!: number;
}

export class CustomerListModel {
  Id!: number;
  FullName!: string;
  FatherName!: string;
  EmailId!: string;
  Mobile!: string;
  IsActive!: boolean | null;
  IsApproved!: boolean;
  UserId!: number;
  Mpin!: string;
  ProfileImageUrl!: string;
  Gender!: string;
}
