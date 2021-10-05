import { environment } from 'src/environments/environment';

export class API_Url {

  //#region <<Login>>
  public static Login_Api = `${environment.apiEndPoint}account/login`;
  //#endregion

  //#region <<Common >>
  public static DropDown_Api = `${environment.apiEndPoint}Common/GetDropDown`;

  //#endregion

  //#region <<User Role>>
  public static UserRoleList_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static UserRoleDetail_Api = `${environment.apiEndPoint}UserRole/get/`;
  public static UserRoleAddUpdate_Api = `${environment.apiEndPoint}UserRole/post`;
  public static UserRoleCheckRoleExist_Api = `${environment.apiEndPoint}UserRole/CheckRoleExist/`;
  public static UserRoleChangeActiveStatus_Api = `${environment.apiEndPoint}UserRole/ChangeActiveStatus/`;
  public static UserRoleDelete_Api = `${environment.apiEndPoint}UserRole/delete/`;
  //#endregion

  //#region <<State -District>>
  public static State_List_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static State_Detail_Api = `${environment.apiEndPoint}UserRole/get/`;
  public static State_Add_Update_Api = `${environment.apiEndPoint}UserRole/post`;
  public static State_Active_Status_Api = `${environment.apiEndPoint}UserRole/ChangeActiveStatus/`;
  public static State_Delete_Api = `${environment.apiEndPoint}UserRole/delete/`;

  public static State_Dropdown_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static District_Dropdown_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static District_List_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static District_Detail_Api = `${environment.apiEndPoint}UserRole/get/`;
  public static District_Add_Update_Api = `${environment.apiEndPoint}UserRole/post`;
  public static District_Active_Status_Api = `${environment.apiEndPoint}UserRole/ChangeActiveStatus/`;
  public static District_Delete_Api = `${environment.apiEndPoint}UserRole/delete/`;
  //#endregion

  //#region <<Qualification>>
  public static Qualification_List_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static Qualification_Detail_Api = `${environment.apiEndPoint}UserRole/get/`;
  public static Qualification_Add_Update_Api = `${environment.apiEndPoint}UserRole/post`;
  public static Qualification_Active_Status_Api = `${environment.apiEndPoint}UserRole/ChangeActiveStatus/`;
  public static Qualification_Delete_Api = `${environment.apiEndPoint}UserRole/delete/`;
  //#endregion

  //#region <<Payment Mode>>
  public static PaymentMode_List_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static PaymentMode_Detail_Api = `${environment.apiEndPoint}UserRole/get/`;
  public static PaymentMode_Add_Update_Api = `${environment.apiEndPoint}UserRole/post`;
  public static PaymentMode_Active_Status_Api = `${environment.apiEndPoint}UserRole/ChangeActiveStatus/`;
  public static PaymentMode_Delete_Api = `${environment.apiEndPoint}UserRole/delete/`;
  //#endregion

  //#region <<Kyc Document Type>>
  public static Kyc_Document_Type_Dropdown_Api =`${environment.apiEndPoint}UserRole/Get`;
  public static Kyc_Document_Type_List_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static Kyc_Document_Type_Detail_Api = `${environment.apiEndPoint}UserRole/get/`;
  public static Kyc_Document_Type_Add_Update_Api = `${environment.apiEndPoint}UserRole/post`;
  public static Kyc_Document_Type_Active_Status_Api = `${environment.apiEndPoint}UserRole/ChangeActiveStatus/`;
  public static Kyc_Document_Type_Delete_Api = `${environment.apiEndPoint}UserRole/delete/`;
  //#endregion

  //#region <<Banks>>
  public static Banks_List_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static Bank_Detail_Api = `${environment.apiEndPoint}UserRole/get/`;
  public static Bank_Add_Update_Api = `${environment.apiEndPoint}UserRole/post`;
  public static Bank_Active_Status_Api = `${environment.apiEndPoint}UserRole/ChangeActiveStatus/`;
  public static Bank_Delete_Api = `${environment.apiEndPoint}UserRole/delete/`;
  //#endregion

  //#region <<Doorstep Agent>>
  public static Doorstep_Agent_List_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static Doorstep_Agent_Add_Update_Api = `${environment.apiEndPoint}UserRole/get/`;
  public static Doorstep_Agent_Delete_Api = `${environment.apiEndPoint}UserRole/post`;
  public static Doorstep_Agent_Active_Status_Api = `${environment.apiEndPoint}UserRole/ChangeActiveStatus/`;
  public static Doorstep_Agent_Detail_Api = `${environment.apiEndPoint}UserRole/delete/`;
  //#endregion


}

export class Routing_Url {

  //#region <<Module URL>>
  static MasterModule = 'admin/master/';
  static DoorStepModule = 'admin/doorstep-agent';
  //#endregion

  //#region <<Login URL>>
  static LoginUrl = 'login';
  //#endregion

  //#region <User Role >
  static UserRoleListUrl = 'user-role';
  static UserRoleDetailUrl = '/detail/';
  static UserRoleAddUpdateUrl = '/add-update/';
  //#endregion

  //#region <Qualification>
  static Qualification_List_Url = 'user-role';
  static Qualification_Detail_Url = '/detail/';
  static Qualification_AddUpdate_Url = '/add-update/';
  //#endregion

  //#region <PaymentMode>
  static PaymentMode_List_Url = 'user-role';
  static PaymentMode_Detail_Url = '/detail/';
  static PaymentMode_AddUpdate_Url = '/add-update/';
  //#endregion

  //#region <State >
  static State_List_Url = 'user-role';
  static State_Detail_Url = '/detail/';
  static State_AddUpdate_Url = '/add-update/';

  static District_List_Url = 'user-role';
  static District_Detail_Url = '/detail/';
  static District_AddUpdate_Url = '/add-update/';
  //#endregion

  //#region <KYC Document Type >
  static Kyc_Document_Type_List_Url = 'user-role';
  static Kyc_Document_Type_Detail_Url = '/detail/';
  static Kyc_Document_Type_AddUpdate_Url = '/add-update/';
  //#endregion

  //#region <DoorStepAgent>
  static DoorStep_Agent_Registration_Url = '/registration';
  static DoorStep_Agent_List_Url ='/door-step-agents';
  //#endregion
}

export class Message {
  //#region <<Alert Message >>
  static SaveSuccess = 'Record Successfully saved...!';
  static SaveFail = 'Record failed to save...!';
  static UpdateSuccess = 'Record Successfully updated...!';
  static UpdateFail = 'Record failed to update...!';
  static ConfirmUpdate = 'Are you Sure update this record?';
  static DeleteConfirmation = 'Are you want to delete record ?';
  //#endregion
}

export class DropDown_key {
  static ddlParentUserRole = 'ddlParentUserRole';
  static ddlUserRole = 'ddlUserRole';
}
