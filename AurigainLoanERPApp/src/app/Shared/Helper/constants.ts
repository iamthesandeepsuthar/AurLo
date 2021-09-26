import { environment } from "src/environments/environment";

export class API_Url {




    //#region  << API URL>>

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

    //#endregion




}
export class Routing_Url {

    static MasterModule = 'admin/master/'

    static LoginUrl = 'login';
    static DoorStep_Agent_RegistrationUrl = '';

    //#region <User Role >
    static UserRoleListUrl = 'user-role';
    static UserRoleDetailUrl = '/detail/';
    static UserRoleAddUpdateUrl = '/add-update/';


    //#endregion
}
export class Message {

    //#region <<Alert Message >>
    static SaveSucess = "Record Sucessfully saved...!"
    static SaveFail = "Record faild to save...!"
    static UpdateSucess = "Record Sucessfully updated...!"
    static UpdateFail = "Record failed to update...!"
    static ConfirmUpdate = "Are you Sure update this record?"

    //#endregion
}

export class DropDown_key{
static ddlParentUserRole = "ddlParentUserRole";
static ddlUserRole = "ddlUserRole";

}