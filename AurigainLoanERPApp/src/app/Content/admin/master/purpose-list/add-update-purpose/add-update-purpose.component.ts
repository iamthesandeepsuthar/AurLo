import { PurposeService } from './../../../../../Shared/Services/master-services/purpose.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PurposeModel } from 'src/app/Shared/Model/master-model/purpose-model.model';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-update-purpose',
  templateUrl: './add-update-purpose.component.html',
  styles: [  ],
  providers:[PurposeService]
})
export class AddUpdatePurposeComponent implements OnInit {

  Id: number = 0;
  model = new PurposeModel();
  purposeForm!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.purposeForm.controls; }
  get Model(): PurposeModel{  return this.model; }


  constructor(private readonly fb: FormBuilder, private readonly _purposeService: PurposeService,
    private _activatedRoute: ActivatedRoute, private _router: Router,private readonly toast: ToastrService) {
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
    this.purposeForm = this.fb.group({
      Name: [undefined, Validators.required],
      IsActive: [undefined],
    });
  }
  onGetDetail() {
    let subscription = this._purposeService.GetPurpose(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as PurposeModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }
  onSubmit() {
    this.purposeForm.markAllAsTouched();
    if (this.purposeForm.valid) {
      let subscription = this._purposeService.AddUpdatePurpose(this.Model).subscribe(response => {
        subscription.unsubscribe();
        if(response.IsSuccess) {
         this.toast.success( this.Id ==0 ?'Record save successful' : 'Record update successful' , 'Success');
         this._router.navigate([this.routing_Url.AdminModule+'/'+this.routing_Url.MasterModule + this.routing_Url.Purpose_List_Url]);
        } else {
          this.toast.error(response.Message?.toString(), 'Error');
        }
      })
    }
  }

}
