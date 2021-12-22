import { PincodeAreaModel } from './../../../../../Shared/Model/master-model/district.model';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { DistrictModel } from 'src/app/Shared/Model/master-model/district.model';
import { DDLStateModel } from 'src/app/Shared/Model/master-model/state.model';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { CommonService } from 'src/app/Shared/Services/common.service';

@Component({
  selector: 'app-add-update-district',
  templateUrl: './add-update-district.component.html',
  styleUrls: ['./add-update-district.component.scss'],
  providers: [StateDistrictService],
})
export class AddUpdateDistrictComponent implements OnInit {
  Id: number = 0;
  model = new DistrictModel();
  showParent: boolean = true;
  districtForm!: FormGroup;
  state_dropDown!: DDLStateModel[];
  isArea: boolean = false;
  isUpdate: boolean = false;
  Areas!: PincodeAreaModel[];
  pincodeAreaModel = new PincodeAreaModel();
  get State_DropDown(): DDLStateModel[] {
    return this.state_dropDown;
  }
  get f() {
    return this.districtForm.controls;
  }
  get routing_Url() {
    return Routing_Url;
  }

  constructor(
    private readonly fb: FormBuilder,
    private readonly _districtService: StateDistrictService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private readonly toast: ToastrService,
    readonly _commonService: CommonService
  ) {
    this.Areas = [];
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
    }
  }
  @ViewChild('HideAddUpdateModel') HideAddUpdateModel!: ElementRef;
  ngOnInit(): void {
    this.formInit();
    this.getStates();
    if (this.Id > 0) {
      this.onGetDetail();
    }
  }
  onGetDetail() {
    let subscription = this._districtService.GetDistrict(this.Id).subscribe((res) => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
        this.model = res.Data as DistrictModel;
      } else {
        this.toast.warning('Record not found', 'No Record');
      }
    });
  }
  getStates() {
    let subscription = this._districtService.GetDDLState().subscribe((res) => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
        this.state_dropDown = res.Data as DDLStateModel[];
      } else {
      }
    });
  }
  formInit() {
    this.districtForm = this.fb.group({
      Name: [undefined, Validators.required],
      DllState: [undefined, Validators.required],
      Pin: [undefined, undefined],
      IsActive: [undefined, undefined],
      areaName: [undefined, undefined],
    });
  }
  onSubmit() {
    // if (this.pincodeAreaModel.AreaName != undefined && this.pincodeAreaModel.Pincode != undefined) {
    //   this.toast.warning('Please submit Pincode and Area first...!', 'Warning');
    //   return;
    // }

    this.districtForm.markAllAsTouched();
    if (this.districtForm.valid) {

      let subscription = this._districtService.AddUpdateDistrict(this.model).subscribe((response) => {
        subscription.unsubscribe();
        if (response.IsSuccess) {
          this.toast.success(
            this.Id == 0
              ? 'Record save successful'
              : 'Record update successful',
            'Success'
          );
          this._router.navigate([
            this.routing_Url.AdminModule +
            '/' +
            this.routing_Url.MasterModule +
            this.routing_Url.District_List_Url,
          ]);
        } else {
          this.toast.error(response.Message?.toString(), 'Error');
        }
      });
    }
  }
  addToList() {
    this.districtForm.get("Pin")?.setValidators(Validators.required);
    this.districtForm.get("areaName")?.setValidators(Validators.required);
    this.districtForm.get("Pin")?.updateValueAndValidity();
    this.districtForm.get("areaName")?.updateValueAndValidity();
    this.districtForm.markAllAsTouched();
    if (this.pincodeAreaModel.AreaName != undefined && this.pincodeAreaModel.Pincode != undefined) {
      this.model.Areas.push(this.pincodeAreaModel);
      this.clearPincodeArea();
      this.HideAddUpdateModel.nativeElement.click();
    } else {
      this.toast.warning('Pincode and Area name cannot be blank', 'Required');
    }
  }
  getPincode(index: number) {
    this.pincodeAreaModel = Object.assign(this.model.Areas[index]);
    this.model.Areas.splice(index, 1);
    this.isArea = true;
    this.isUpdate = true;
  }
  removeArea(index: number) {
    this.pincodeAreaModel = Object.assign(this.model.Areas[index]);
    if (this.model.Areas[index].Id == 0 || !this.model.Areas[index].Id) {
      this.model.Areas.splice(index, 1);
    }
  }
  updatePincode() {
    this.isUpdate = false;
    this.clearPincodeArea();
  }
  clearPincodeArea() {
    this.pincodeAreaModel = new PincodeAreaModel();
    this.districtForm.get("Pin")?.setValidators(null);
    this.districtForm.get("areaName")?.setValidators(null);
    this.districtForm.get("Pin")?.updateValueAndValidity();
    this.districtForm.get("areaName")?.updateValueAndValidity();
  }
}
