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
