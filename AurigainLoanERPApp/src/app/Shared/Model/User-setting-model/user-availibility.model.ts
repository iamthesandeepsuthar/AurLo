export class UserAvailibility {
}
export class UserAvailibilityPostModel {
  Id!: number;
  UserId!: number;
  MondayST!: string | null;
  MondayET!: string | null;
  TuesdayST!: string | null;
  TuesdayET!: string | null;
  WednesdayST!: string | null;
  WednesdayET!: string | null;
  ThursdayST!: string | null;
  ThursdayET!: string | null;
  FridayST!: string | null;
  FridayET!: string | null;
  SaturdayST!: string | null;
  SaturdayET!: string | null;
  SundayST!: string | null;
  SundayET!: string | null;
  Capacity!: number;
  PincodeAreaId!: number;

}

export interface AvailableAreaModel {
  Id: number;
  AreaName: string;
  PinCode: string;
}


export interface UserAvailabilityViewModel {
  Id: number;
  UserId: number;
  MondaySt: string | null;
  MondayEt: string | null;
  TuesdaySt: string | null;
  TuesdayEt: string | null;
  WednesdaySt: string | null;
  WednesdayEt: string | null;
  ThursdaySt: string | null;
  ThursdayEt: string | null;
  FridaySt: string | null;
  FridayEt: string | null;
  SaturdaySt: string | null;
  SaturdayEt: string | null;
  SundaySt: string | null;
  SundayEt: string | null;
  Capacity: number | null;
  PincodeAreaId: number | null;
  Area: string;
  IsActive: boolean | null;
  IsDelete: boolean;
}
