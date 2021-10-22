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
 //#region  << Jewellery >>
 public static Jewellery_List_Api = `${environment.apiEndPoint}JewellaryType/GetList`;
 public static Jewellery_Dropdown_List_Api =`${environment.apiEndPoint}JewellaryType/JewellaryTypes`;
 public static Jewellery_AddUpdate_Api = `${environment.apiEndPoint}JewellaryType/AddUpdate`;
 public static Jewellery_Delete_Api = `${environment.apiEndPoint}JewellaryType/DeleteJewellaryType/`;
 public static Jewellery_ActiveStatus_Api = `${environment.apiEndPoint}JewellaryType/UpdateActiveStatus/`;
 public static Jewellery_Detail_Api = `${environment.apiEndPoint}JewellaryType/GetById/`;
 //#endregion
//#region  << Product Category >>
public static Product_Category_List_Api = `${environment.apiEndPoint}ProductCategory/GetList`;
public static Product_Category_Dropdown_List_Api =`${environment.apiEndPoint}ProductCategory/ProductCategories`;
public static Product_Category_AddUpdate_Api = `${environment.apiEndPoint}ProductCategory/AddUpdate`;
public static Product_Category_Delete_Api = `${environment.apiEndPoint}ProductCategory/DeleteProductCategory/`;
public static Product_Category_ActiveStatus_Api = `${environment.apiEndPoint}ProductCategory/UpdateActiveStatus/`;
public static JProduct_Category_Detail_Api = `${environment.apiEndPoint}ProductCategory/GetById/`;
//#endregion
//#region  << Product  >>
public static Product_List_Api = `${environment.apiEndPoint}Product/GetList`;
public static Product_Dropdown_List_Api =`${environment.apiEndPoint}Product/Products`;
public static Product_AddUpdate_Api = `${environment.apiEndPoint}Product/AddUpdate`;
public static Product_Delete_Api = `${environment.apiEndPoint}Product/DeleteProduct/`;
public static Product_ActiveStatus_Api = `${environment.apiEndPoint}Product/UpdateActiveStatus/`;
public static Product_Detail_Api = `${environment.apiEndPoint}Product/GetById/`;
//#endregion
//#region  << Bank & Branch  >>
public static Bank_List_Api = `${environment.apiEndPoint}Bank/GetList`;
public static Bank_Dropdown_List_Api =`${environment.apiEndPoint}Bank/Products`;
public static Bank_AddUpdate_Api = `${environment.apiEndPoint}Bank/AddUpdate`;
public static Bank_Delete_Api = `${environment.apiEndPoint}Bank/DeleteProduct/`;
public static Bank_ActiveStatus_Api = `${environment.apiEndPoint}Bank/UpdateActiveStatus/`;
public static Bank_Detail_Api = `${environment.apiEndPoint}Bank/GetById/`;

public static Branch_List_Api = `${environment.apiEndPoint}BankBranch/GetList`;
public static Branch_Dropdown_List_Api =`${environment.apiEndPoint}BankBranch/Branches`;
public static Branch_AddUpdate_Api = `${environment.apiEndPoint}BankBranch/AddUpdate`;
public static Branch_Delete_Api = `${environment.apiEndPoint}BankBranch/DeleteBranch/`;
public static Branch_ActiveStatus_Api = `${environment.apiEndPoint}BankBranch/UpdateActiveStatus/`;
public static Branch_Detail_Api = `${environment.apiEndPoint}BankBranch/GetById/`;
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

  //#region  << Product Category URL >>
  static Product_Category_List_Url = '/product-categories';
  static Product_Category_Detail_Url = '/detail-product-category/';
  static Product_Category_AddUpdate_Url = '/add-product-category/';
  //#endregion
  //#region  << Product URL >>
  static Product_List_Url = '/products';
  static Product_Detail_Url = '/detail-product/';
  static Product_AddUpdate_Url = '/add-product/';
  //#endregion

  //#region  << bank URL >>
  static Bank_List_Url = '/banks';
  static Bank_Detail_Url = '/detail-bank/';
  static Bank_AddUpdate_Url = '/add-bank/';
  //#endregion

  //#region  << Bank Branch URL >>
  static Bank_Branch_Detail_Url = '/detail-branch/';
  static Bank_Branch_AddUpdate_Url = '/add-branch/';
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
