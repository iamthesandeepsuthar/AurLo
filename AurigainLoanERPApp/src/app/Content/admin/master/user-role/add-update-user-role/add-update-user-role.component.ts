import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key, Message, Routing_Url } from 'src/app/Shared/Helper/constants';
import { UserRolePostModel } from 'src/app/Shared/Model/master-model/user-role.model';

import { CommonService } from 'src/app/Shared/Services/common.service';
import { UserRoleService } from 'src/app/Shared/Services/master-services/user-role.service';


@Component({
  selector: 'app-add-update-user-role',
  templateUrl: './add-update-user-role.component.html',
  styleUrls: ['./add-update-user-role.component.scss'],
  providers: [UserRoleService]
})
export class AddUpdateUserRoleComponent implements OnInit {
  Id: number = 0;
  model = new UserRolePostModel();
  showParent: boolean = true;
  userRoleForm!: FormGroup;
  dropDown = new DropDownModel();

  get f() { return this.userRoleForm.controls; }
  get routing_Url() { return Routing_Url }

  constructor(private readonly fb: FormBuilder, private readonly _userRole: UserRoleService,
    private _activatedRoute: ActivatedRoute, private _router: Router,
    private readonly _commonService: CommonService, private readonly _toast: ToastrService) {

    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
    }


  }

  ngOnInit(): void {
    this.GetDropDown();
    this.formInit();



    if (this.Id > 0) {
      this.onGetDetail();
    }
  }

  formInit() {
    this.userRoleForm = this.fb.group({
      Name: [undefined, Validators.required],
      HasParent: [undefined, null],
      ParentRole: [undefined, null],
      IsActive: [true, Validators.required],

    });
  }
  UpdateValidation() {

    if (this.showParent) {
      this.f.ParentRole.setValidators(Validators.required);

    } else {
      this.f.ParentRole.setValidators(null);

    }

    this.f.ParentRole.updateValueAndValidity();

  }

  onSubmit() {
    this.userRoleForm.markAllAsTouched();
    if (this.userRoleForm.valid) {
      this._userRole.AddUpdateRole(this.model).subscribe(res => {
        if (res.IsSuccess) {

          this._commonService.Success(Message.SaveSuccess)
          this._toast.success('Save successful record');
          this._router.navigate(['/' + this.routing_Url.MasterModule + this.routing_Url.UserRoleListUrl]);

        } else {

        }
      });
    }
  }

  onGetDetail() {
    this._userRole.GetRole(this.Id).subscribe(res => {
      if (res.IsSuccess) {

        let roleDetail = res.Data;
        this.model.Name = roleDetail?.Name as string;
        this.model.Id = roleDetail!.Id
        this.model.ParentId = roleDetail!.ParentId ? String(roleDetail!.ParentId) : null;
        this.model.IsActive = roleDetail!.IsActive as boolean;
        this.showParent = roleDetail!.ParentId ? true : false;
        this.UpdateValidation();
      } else {

      }
    });
  }

  GetDropDown() {
    this._commonService.GetDropDown([DropDown_key.ddlUserRole]).subscribe(res => {
      if (res.IsSuccess) {
        this.dropDown = res.Data;
      }

    });
  }

}
