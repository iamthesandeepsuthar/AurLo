import { JewellaryTypeModel } from './../../../../../Shared/Model/master-model/jewellary-type-model.model';
import { Component, OnInit } from '@angular/core';
import { JewelleryTypeService } from 'src/app/Shared/Services/master-services/jewellery-type.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-jewellary',
  templateUrl: './add-jewellary.component.html',
  styleUrls: ['./add-jewellary.component.scss'],
  providers: [JewelleryTypeService]
})
export class AddJewellaryComponent implements OnInit {

  Id: number = 0;
  model = new JewellaryTypeModel();
  typeFrom!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.typeFrom.controls; }
  get Model(): JewellaryTypeModel{  return this.model; }

  constructor(private readonly fb: FormBuilder,
    private readonly _jewelleryType: JewelleryTypeService,
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
this.typeFrom = this.fb.group({
Name: [undefined, Validators.required],
IsActive: [true],
Description: [undefined]
});
}
onGetDetail() {
let subscription = this._jewelleryType.JewelleryTypeById(this.Id).subscribe(res => {
subscription.unsubscribe();
if (res.IsSuccess) {
this.model = res.Data as JewellaryTypeModel;
} else {
this.toast.warning('Record not found', 'No Record');
}
});
}
onSubmit() {
this.typeFrom.markAllAsTouched();
if (this.typeFrom.valid) {
let subscription = this._jewelleryType.AddUpdateJewelleryType(this.Model).subscribe(response => {
subscription.unsubscribe();
if(response.IsSuccess) {
 this.toast.success( this.Id ==0 ?'Record save successful' : 'Record update successful' , 'Success');
 this._router.navigate([this.routing_Url.AdminModule+'/'+this.routing_Url.MasterModule + this.routing_Url.Jewellery_Type_List_Url]);
} else {
  this.toast.error(response.Message?.toString(), 'Error');
}
})
}
}
}
