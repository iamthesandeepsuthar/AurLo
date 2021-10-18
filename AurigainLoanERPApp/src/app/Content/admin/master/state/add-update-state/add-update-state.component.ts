import { StateDistrictService } from './../../../../../Shared/Services/master-services/state-district.service';
import { Component, OnInit } from '@angular/core';
import { StateModel } from 'src/app/Shared/Model/master-model/state.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-update-state',
  templateUrl: './add-update-state.component.html',
  styleUrls: ['./add-update-state.component.scss'],
  providers: [StateDistrictService]
})
export class AddUpdateStateComponent implements OnInit {

  Id: number = 0;
  model = new StateModel();
  stateForm!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.stateForm.controls; }
  get Model(): StateModel{  return this.model; }


  constructor(private readonly fb: FormBuilder,
              private readonly _stateService: StateDistrictService,
              private _activatedRoute: ActivatedRoute, private _router: Router,
              private readonly toast: ToastrService) {
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
    this.stateForm = this.fb.group({
      Name: [undefined, Validators.required],
      IsActive: [true, Validators.required],
    });
  }
  onGetDetail() {
    let subscription = this._stateService.GetState(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as StateModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }
  onSubmit() {
    this.stateForm.markAllAsTouched();
    if (this.stateForm.valid) {
      let subscription = this._stateService.AddUpdateState(this.Model).subscribe(response => {
        subscription.unsubscribe();
        if(response.IsSuccess) {
         this.toast.success( this.Id ==0 ?'Record save successful' : 'Record update successful' , 'Success');
         this._router.navigate([this.routing_Url.AdminModule+'/'+this.routing_Url.MasterModule + this.routing_Url.State_List_Url]);
        } else {
          this.toast.error(response.Message?.toString(), 'Error');
        }
      })
    }
  }

}
