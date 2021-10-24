import { UserManagerModel } from 'src/app/Shared/Model/master-model/user-manager-model.model';
import { UserManagerService } from './../../../../Shared/Services/user-manager.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-update-managers',
  templateUrl: './add-update-managers.component.html',
  styleUrls: ['./add-update-managers.component.scss'],
  providers: [UserManagerService]
})
export class AddUpdateManagersComponent implements OnInit {
  Id: number = 0;
  model = new UserManagerModel();
  managerFrom!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.managerFrom.controls; }
  get Model(): UserManagerModel{  return this.model; }
  constructor(private readonly fb: FormBuilder,
    private readonly _managerService: UserManagerService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private readonly toast: ToastrService) {
      this.model.IsActive = true;
if (this._activatedRoute.snapshot.params.id) {
this.Id = this._activatedRoute.snapshot.params.id;
}
}
ngOnInit(): void {
  this.formInit();
  if (this.Id > 0) {
  this.onGetDetail();
  }
  }
  formInit() {
  this.managerFrom = this.fb.group({
  FullName: [undefined, Validators.required],
  FatherName:[undefined, Validators.required],
  EmailId:[undefined, Validators.required,Validators.email],
  MobileNumber: [undefined , Validators.required],
  IsActive: [true, Validators.required],
  Description: [undefined]
  });
  }
  onGetDetail() {
  let subscription = this._managerService.GetManagerById(this.Id).subscribe(res => {
  subscription.unsubscribe();
  if (res.IsSuccess) {
  this.model = res.Data as UserManagerModel;
  } else {
  this.toast.warning('Record not found', 'No Record');
  }
  });
  }
  onSubmit() {
  this.managerFrom.markAllAsTouched();
  if (this.managerFrom.valid) {
  let subscription = this._managerService.AddUpdateManager(this.Model).subscribe(response => {
  subscription.unsubscribe();
  if(response.IsSuccess) {
   this.toast.success( this.Id ==0 ?'Record save successful' : 'Record update successful' , 'Success');
   this._router.navigate([this.routing_Url.AdminModule+'/'+this.routing_Url.MasterModule + this.routing_Url.Manager_List_Url]);
  } else {
    this.toast.error(response.Message?.toString(), 'Error');
  }
  })
  }
  }

}
