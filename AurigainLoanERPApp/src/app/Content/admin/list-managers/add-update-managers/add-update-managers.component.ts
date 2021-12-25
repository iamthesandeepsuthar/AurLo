
import { DDLDistrictModel } from './../../../../Shared/Model/master-model/district.model';
import { DDLStateModel } from 'src/app/Shared/Model/master-model/state.model';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { DDLUserRole } from './../../../../Shared/Model/master-model/user-role.model';
import { UserRoleService } from 'src/app/Shared/Services/master-services/user-role.service';
import { UserManagerModel } from 'src/app/Shared/Model/master-model/user-manager-model.model';
import { UserManagerService } from './../../../../Shared/Services/user-manager.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserSettingPostModel } from 'src/app/Shared/Model/User-setting-model/user-setting.model';
import { UserSettingService } from 'src/app/Shared/Services/user-setting-services/user-setting.service';
import { AvailableAreaModel } from 'src/app/Shared/Model/User-setting-model/user-availibility.model';

@Component({
  selector: 'app-add-update-managers',
  templateUrl: './add-update-managers.component.html',
  styleUrls: ['./add-update-managers.component.scss'],
  providers: [UserManagerService,UserRoleService,StateDistrictService,UserSettingService]
})
export class AddUpdateManagersComponent implements OnInit {
  Id: number = 0;
  model = new UserManagerModel();
  areaModel!: AvailableAreaModel[];
  managerFrom!: FormGroup;
  roleModel!: DDLUserRole[];
  stateModel!: DDLStateModel[];
  districtModel!: DDLDistrictModel[];
  get routing_Url() { return Routing_Url }
  get f() { return this.managerFrom.controls; }
  get Model(): UserManagerModel{  return this.model; }
  get DobMaxDate() {
    var date = new Date();
    date.setFullYear(date.getFullYear() - 18);
    return date
  };
  fileData!: File;
  previewUrl: any = null;
  constructor(private readonly fb: FormBuilder,
    private readonly _managerService: UserManagerService,
    private readonly _roleService: UserRoleService,
    private readonly _stateService: StateDistrictService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private readonly toast: ToastrService, private readonly _userSettingService: UserSettingService) {
      this.model.IsActive = true;
if (this._activatedRoute.snapshot.params.id) {
this.Id = this._activatedRoute.snapshot.params.id;
}
}
ngOnInit(): void {
  this.formInit();
  this.getRole();
 // this.getState();
  if (this.Id > 0) {
  this.onGetDetail();
  }
  }
  formInit() {
  this.managerFrom = this.fb.group({
  FullName: [undefined, Validators.required],
  FatherName:[undefined, Validators.required],
  EmailId:[undefined , Validators.required],
  MobileNumber: [undefined , Validators.required ],
  IsActive: [true],
  Description: [undefined],
  DllState:[undefined , Validators.required],
  //DllDistrict: [undefined, Validators.required],
  UserRole: [undefined, Validators.required],
  Pincode: [undefined , Validators.required],
  DateOfBirth:[undefined , Validators.required],
  Address: [undefined],
  IsWhatsApp: [undefined],
  Gender:[undefined]
  });
  }
  getRole(){
    let subscription = this._roleService.GetUserRoleDDL().subscribe(response => {
    subscription.unsubscribe();
    if(response.IsSuccess) {
     this.roleModel = response.Data as DDLUserRole[];
     console.log('Role ----------------------->',this.roleModel);
    } else {
      this.toast.warning(response.Message?.toString(), 'Server Error');
      return;
    }
    });
  }
  getState() {
  let subscription = this._stateService.GetDDLState().subscribe(response => {
    subscription.unsubscribe();
    if(response.IsSuccess){
      this.stateModel = response.Data as DDLStateModel[];
    }  else {
      this.toast.warning(response.Message as string , 'Server Error');
      return;
    }
  });
  }
  getDistrict(id: number) {
   this.districtModel = [];
    let subscription = this._stateService.GetDDLDistrict(id).subscribe(response => {
      subscription.unsubscribe();
      if(response.IsSuccess){
        this.districtModel = response.Data as DDLDistrictModel[];
      }  else {
        this.toast.warning(response.Message as string , 'Server Error');
        return;
      }
    });
  }
  getAreaByPincode(value: any) {
    let subscription = this._stateService.GetAreaByPincode(value).subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        this.areaModel = response.Data as AvailableAreaModel[];
      } else {
        this.toast.warning(response.Message as string, 'Server Error');

      }
    });
  }
  onGetDetail() {
  let subscription = this._managerService.GetManagerById(this.Id).subscribe(res => {
  subscription.unsubscribe();
  if (res.IsSuccess) {
  this.model = res.Data as UserManagerModel;
  this.model.AreaPincodeId = this.model.AreaPincodeId;
  this.getAreaByPincode(this.model.Pincode);
  } else {
  this.toast.warning('Record not found', 'No Record');
  }
  });
  }
  fileProgress(fileInput: any) {
    this.fileData = fileInput.target.files[0] as File;
    this.preview();
  }
  preview() {
    // Show preview
    const mimeType = this.fileData?.type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
    const reader = new FileReader();
    reader.readAsDataURL(this.fileData);
    reader.onload = (event) => {
      this.previewUrl = reader.result;
    };
  }
  onUploadProfileImage() {
    if (this.Id) {
      let profileModel = new UserSettingPostModel();
      profileModel.FileName = this.fileData?.name;
      profileModel.UserId = Number(this.Id);
      profileModel.ProfileBase64 = this.previewUrl;
      this._userSettingService.UpdateUserProfile(profileModel).then(res => {

        if (res.IsSuccess) {
          this.toast.success('profile picture upload successful', 'Upload Status');
        } else {
          this.toast.error(res.Message as string, 'Upload Status');
        }
      });
    }
  }
  onSubmit() {
  this.managerFrom.markAllAsTouched();
  if (this.managerFrom.valid) {
  let subscription = this._managerService.AddUpdateManager(this.Model).subscribe(response => {
  subscription.unsubscribe();
  if(response.IsSuccess) {
    // if(this.fileData != null) {
    //   this.Id = Number(response.Data);
    //   this.onUploadProfileImage();
    // }
   this.toast.success( this.Id ==0 ?'Record save successful' : 'Record update successful' , 'Success');
   this._router.navigate([this.routing_Url.AdminModule+'/' + this.routing_Url.Manager_List_Url]);
  } else {
    this.toast.error(response.Message?.toString(), 'Error');
  }
  })
 }
  }

}
