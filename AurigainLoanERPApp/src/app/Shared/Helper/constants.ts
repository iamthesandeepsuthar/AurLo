import { environment } from 'src/environments/environment';

export class API_Url {

  //#region <<Login>>
  public static Login_Api = `${environment.apiEndPoint}account/login`;
  //#endregion

  //#region <<Common >>
  public static DropDown_Api = `${environment.apiEndPoint}Common/GetDropDown`;
  public static FilterDropDown_Api = `${environment.apiEndPoint}Common/GetFilterDropDown`;
  public static MultipleFilterDropDown_Api = `${environment.apiEndPoint}Common/GetMultipleFilterDropDown`;

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
  public static State_List_Api = `${environment.apiEndPoint}StateAndDistrict/GetStateList`;
  public static State_Detail_Api = `${environment.apiEndPoint}StateAndDistrict/GetStateById/`;
  public static State_Add_Update_Api = `${environment.apiEndPoint}StateAndDistrict/SubmitState`;
  public static State_Active_Status_Api = `${environment.apiEndPoint}StateAndDistrict/ChangeStateActiveStatus/`;
  public static State_Delete_Api = `${environment.apiEndPoint}StateAndDistrict/DeleteState/`;

  public static State_Dropdown_Api = `${environment.apiEndPoint}StateAndDistrict/States`;
  public static District_Dropdown_Api = `${environment.apiEndPoint}StateAndDistrict/Districts/`;
  public static District_List_Api = `${environment.apiEndPoint}StateAndDistrict/GetDistrictList`;
  public static District_Detail_Api = `${environment.apiEndPoint}StateAndDistrict/GetDistrictById/`;
  public static District_Add_Update_Api = `${environment.apiEndPoint}StateAndDistrict/SubmitDistrict`;
  public static District_Active_Status_Api = `${environment.apiEndPoint}StateAndDistrict/ChangeDistrictActiveStatus/`;
  public static District_Delete_Api = `${environment.apiEndPoint}StateAndDistrict/DeleteDistrict/`;
  //#endregion

  //#region <<Qualification>>
  public static Qualification_List_Api = `${environment.apiEndPoint}Qualification/GetList`;
  public static Qualification_Detail_Api = `${environment.apiEndPoint}Qualification/GetQualificationById/`;
  public static Qualification_Add_Update_Api = `${environment.apiEndPoint}Qualification/SubmitQualification`;
  public static Qualification_Active_Status_Api = `${environment.apiEndPoint}Qualification/ChangeActiveStatus/`;
  public static Qualification_Delete_Api = `${environment.apiEndPoint}Qualification/DeleteQualification/`;
  //#endregion

  //#region <<Payment Mode>>
  public static PaymentMode_List_Api = `${environment.apiEndPoint}PaymentMode/GetList`;
  public static PaymentMode_Detail_Api = `${environment.apiEndPoint}PaymentMode/GetPaymentModeById/`;
  public static PaymentMode_Add_Update_Api = `${environment.apiEndPoint}PaymentMode/SubmitPaymentMode`;
  public static PaymentMode_Active_Status_Api = `${environment.apiEndPoint}PaymentMode/ChangeActiveStatus/`;
  public static PaymentMode_Delete_Api = `${environment.apiEndPoint}PaymentMode/DeletePaymentMode/`;
  //#endregion

  //#region <<Kyc Document Type>>
  public static Kyc_Document_Type_Dropdown_Api = `${environment.apiEndPoint}KycDocumentType/DocumentTypes`;
  public static Kyc_Document_Type_List_Api = `${environment.apiEndPoint}KycDocumentType/GetList`;
  public static Kyc_Document_Type_Detail_Api = `${environment.apiEndPoint}KycDocumentType/GetDocumentTypeById/`;
  public static Kyc_Document_Type_Add_Update_Api = `${environment.apiEndPoint}KycDocumentType/SubmitDocumentType`;
  public static Kyc_Document_Type_Active_Status_Api = `${environment.apiEndPoint}KycDocumentType/ChangeActiveStatus/`;
  public static Kyc_Document_Type_Delete_Api = `${environment.apiEndPoint}KycDocumentType/DeleteDocumentType/`;
  //#endregion

  //#region <<Banks>>
  public static Banks_List_Api = `${environment.apiEndPoint}UserRole/Get`;
  public static Bank_Detail_Api = `${environment.apiEndPoint}UserRole/get/`;
  public static Bank_Add_Update_Api = `${environment.apiEndPoint}UserRole/post`;
  public static Bank_Active_Status_Api = `${environment.apiEndPoint}UserRole/ChangeActiveStatus/`;
  public static Bank_Delete_Api = `${environment.apiEndPoint}UserRole/delete/`;
  //#endregion

  //#region <<Doorstep Agent>>
  public static DoorstepAgentListApi = `${environment.apiEndPoint}DoorStepAgent/Get`;
  public static DoorstepAgentAddUpdateApi = `${environment.apiEndPoint}DoorStepAgent/AddUpdate`;
  public static DoorstepAgentDeleteApi = `${environment.apiEndPoint}DoorStepAgent/Delete/`;
  public static DoorstepAgentActiveStatusApi = `${environment.apiEndPoint}DoorStepAgent/UpdatActiveStatus/`;
  public static DoorstepAgentDetailApi = `${environment.apiEndPoint}DoorStepAgent/GetById/`;
  public static DoorstepAgentAvailibilityApi = `${environment.apiEndPoint}DoorStepAgent/SetUserAvailibilty`;
  public static DoorstepAgentDeleteDocumentFileApi = `${environment.apiEndPoint}DoorStepAgent/DeleteDocumentFile/`;

  //#endregion

  //#region << Agent>>
  public static AgentListApi = `${environment.apiEndPoint}Agent/Get`;
  public static AgentAddUpdateApi = `${environment.apiEndPoint}Agent/AddUpdate`;
  public static AgentDeleteApi = `${environment.apiEndPoint}Agent/Delete/`;
  public static AgentActiveStatusApi = `${environment.apiEndPoint}Agent/UpdatActiveStatus/`;
  public static AgentDetailApi = `${environment.apiEndPoint}Agent/GetById/`;
  public static AgentDeleteDocumentFileApi = `${environment.apiEndPoint}DoorStepAgent/DeleteDocumentFile/`;

  //#endregion

  //#region <<User Setting>>
  public static UserUpdateProfileApi = `${environment.apiEndPoint}UserSetting/UpdateProfile`;
  public static UserApproveStatusApi = `${environment.apiEndPoint}UserSetting/UpdateApproveStatus/`;
  //#endregion

  //#region  << Manager's  Above Agent >>
  public static Manager_List_Api = `${environment.apiEndPoint}UserManager/GetList`;
  public static Manager_AddUpdate_Api = `${environment.apiEndPoint}UserManager/AddUpdate`;
  public static Manager_Delete_Api = `${environment.apiEndPoint}UserManager/DeleteManager/`;
  public static Manager_ActiveStatus_Api = `${environment.apiEndPoint}UserManager/UpdateActiveStatus/`;
  public static Manager_Detail_Api = `${environment.apiEndPoint}UserManager/GetById/`;
  //#endregion



}

export class Routing_Url {

  //#region <<Module URL>>
  static AdminModule = 'admin';
  static MasterModule = 'master';
  static DoorStepModule = 'door-step-agent';
  static AgentModule = 'agent';

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
  static Qualification_List_Url = '/qualifications';
  static Qualification_Detail_Url = '/detail-qualification/';
  static Qualification_AddUpdate_Url = '/add-qualification/';
  //#endregion
 //#region <KYC Document Type >
 static Kyc_Document_Type_List_Url = '/kyc-document-type';
 static Kyc_Document_Type_Detail_Url = '/detail-document-type/';
 static Kyc_Document_Type_AddUpdate_Url = '/add-document-type/';
 //#endregion
  //#region <PaymentMode>
  static PaymentMode_List_Url = '/payment-modes';
  static PaymentMode_Detail_Url = '/detail-payment-mode/';
  static PaymentMode_AddUpdate_Url = '/add-payment-mode/';
  //#endregion

  //#region <State >
  static State_List_Url = '/states';
  static State_Detail_Url = '/detail-state/';
  static State_AddUpdate_Url = '/add-state/';

  static District_List_Url = '/district';
  static District_Detail_Url = '/detail-district/';
  static District_AddUpdate_Url = '/add-district/';
  //#endregion



  //#region <DoorStepAgent>
  static DoorStepAgentRegistrationUrl = 'registration';
  static DoorStepAgenDetailUrl = 'detail';
  static DoorStepAgentAvailibilityUrl = 'user-availibility';
  static DoorStepAgentListUrl = 'door-step-agents-list';
  //#endregion

  //#region  <<Agent >>

  static AgentListUrl: 'agent-list';
  static AgentRegistrationUrl: 'agent-AddUpdate';
  static AgenDetailUrl: 'agent-detail';
  static  AgentAvailibilityUrl = 'agent-availibility';

  //#endregion

  //#region  << User manager's URL >>
  static Manager_List_Url = '/managers';
  static Manager_Detail_Url = '/detail-manager/';
  static Manager_AddUpdate_Url = '/add-manager/';
  //#endregion
}

export class Message {
  //#region <<Alert Message >>
  static SaveSuccess = 'Record successfully saved...!';
  static SaveFail = 'Record failed to save...!';
  static UpdateSuccess = 'Record successfully updated...!';
  static UpdateFail = 'Record failed to update...!';
  static ConfirmUpdate = 'Are you Sure update this record?';
  static DeleteConfirmation = 'Are you want to delete record ?';
  //#endregion
}

export class DropDown_key {
  static ddlParentUserRole = 'ddlParentUserRole';
  static ddlUserRole = 'ddlUserRole';
  static ddlState = "ddlState";
  static ddlDistrict = "ddlDistrict";
  static ddlQualification = "ddlQualification";
  static ddlDocumentType = "ddlDocumentType";
  static ddlRelationship = "ddlRelationship";
  static ddlGender = "ddlGender";
  static ddlPaymentMode ="ddlPaymentMode"
}
