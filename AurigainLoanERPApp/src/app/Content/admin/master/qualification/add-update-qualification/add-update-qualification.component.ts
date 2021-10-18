import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { QualificationModel } from 'src/app/Shared/Model/master-model/qualification.model';
import { QualificationService } from 'src/app/Shared/Services/master-services/qualification.service';

@Component({
  selector: 'app-add-update-qualification',
  templateUrl: './add-update-qualification.component.html',
  styleUrls: ['./add-update-qualification.component.scss'],
  providers:[QualificationService]
})
export class AddUpdateQualificationComponent implements OnInit {
  Id: number = 0;
  model = new QualificationModel();
  qualificationForm!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.qualificationForm.controls; }
  get Model(): QualificationModel{  return this.model; }


  constructor(private readonly fb: FormBuilder, private readonly _qualificationService: QualificationService,
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
    this.qualificationForm = this.fb.group({
      Name: [undefined, Validators.required],
      IsActive: [true, Validators.required],
    });
  }
  onGetDetail() {
    let subscription = this._qualificationService.GetQualification(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as QualificationModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }
  onSubmit() {
    this.qualificationForm.markAllAsTouched();
    if (this.qualificationForm.valid) {
      let subscription = this._qualificationService.AddUpdateQualification(this.Model).subscribe(response => {
        subscription.unsubscribe();
        if(response.IsSuccess) {
         this.toast.success( this.Id ==0 ?'Record save successful' : 'Record update successful' , 'Success');
         this._router.navigate([this.routing_Url.AdminModule+'/'+this.routing_Url.MasterModule + this.routing_Url.Qualification_List_Url]);
        } else {
          this.toast.error(response.Message?.toString(), 'Error');
        }
      })
    }
  }
}
