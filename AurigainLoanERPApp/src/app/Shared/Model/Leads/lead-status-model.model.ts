
export class LeadStatusModel {
  LeadId!: number;
  Id!: number;
  LeadStatus!: number| any;
  Remark!: string;
  ActionDate!: Date;
  ActionTakenByUserId!: number;
  ActionTakenByUser!: string;
}
export class LeadStatusActionHistory {
  Id!: number;
  LeadId!: number;
  LeadStatus!: string;
  ActionTakenBy!: string;
  ActionTakenByUserId!: number;
  ActionDate!: Date;
  Remark!: string;
}
