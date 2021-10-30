import { BankBranchService } from './../../../../../Shared/Services/master-services/bank-branch.service';
import {
  BankModel,
  BranchModel,
} from './../../../../../Shared/Model/master-model/bank-model.model';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-add-update-bank',
  templateUrl: './add-update-bank.component.html',
  styleUrls: ['./add-update-bank.component.scss'],
  providers: [BankBranchService],
})
export class AddUpdateBankComponent implements OnInit {
  Id: number = 0;
  IsBranchUpdate: boolean = false;
  model = new BankModel();
  branchModel = new BranchModel();
  bankForm!: FormGroup;
  branchForm!: FormGroup;
  get routing_Url() {
    return Routing_Url;
  }
  get f() {
    return this.bankForm.controls;
  }
  get branch() {
    return this.f.Branches as FormArray;
  }
  get Model(): BankModel {
    return this.model;
  }
  set Model(value: BankModel) {
    this.model = value;
  }
  get BranchModel(): BranchModel {
    return this.branchModel;
  }
  set BranchModel(value: BranchModel) {
    this.branchModel = value;
  }
  display:string = 'none';

  constructor(
    private readonly fb: FormBuilder,
    private readonly _bankService: BankBranchService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private readonly toast: ToastrService
  ) {
    this.model.IsActive = true;
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
    }
  }
  ngOnInit(): void {
    this.formInit();
    this.formInitBranch();
    if (this.Id > 0) {
      this.onGetDetail();
    }
  }
  formInitBranch() {
    this.branchForm = this.fb.group({
      branchName:[undefined],
      branchCode:[undefined],
      branchEmail: [undefined],
      branchIfsc: [undefined],
      branchAddress: [undefined],
      branchPincode: [undefined],
      branchContact: [undefined],
      branchIsActive: [undefined],
    })
  }
  formInit() {
    this.bankForm = this.fb.group({
      Name: [undefined, Validators.required],
      IsActive: [undefined],
      Website: [undefined, Validators.required],
      Contact: [undefined, Validators.required],
      FaxNumber: [undefined],
      BankLogoUrl:[undefined],
    });
  }

  onGetDetail() {
    let subscription = this._bankService.BankById(this.Id).subscribe((res) => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
        this.model = res.Data as BankModel;
      } else {
        this.toast.warning('Record not found', 'No Record');
      }
    });
  }

  addBranch() {
    this.display = 'block';
    this.branchModel = new BranchModel();
    this.branchModel.IsActive = true;
  }

  addToBranchList() {
   // this.branchModel.BranchName == undefined ?  this.toast.info('Please enter required * field of branch', 'Required') : null ;
    if (
      this.branchModel.BranchName == undefined &&
      this.BranchModel.Ifsc == undefined &&
      this.branchModel.BranchCode == undefined && this.branchModel.Pincode == undefined
    ) {
      this.toast.info('Please enter required * field of branch', 'Required');
      return;
    } else {
      if (
        this.model.Branches.find(
          (x) =>
            x.BranchName == this.branchModel.BranchName &&
            x.Ifsc == this.branchModel.Ifsc
        )
      ) {
        this.toast.warning('branch already exist into list', 'Duplicate');
        return;
      } else {
        this.model.Branches.push(this.branchModel);
        this.branchModel = new BranchModel();
        this.BranchModel.IsActive = true;
        this.IsBranchUpdate = false;
        this.display ='none';
      }
    }
  }

  removeToBranchList(index: number) {
    this.model.Branches.splice(index, 1);
  }
  getFromBranchList(index: number) {
    this.branchModel = this.model.Branches[index] as BranchModel;
    this.model.Branches.splice(index, 1);
    this.IsBranchUpdate = true;
  }
  onSubmit() {
    if (this.model.Branches.length < 1) {
      this.toast.warning(
        'Add at least one branch with bank',
        'Branch Required'
      );
      return;
    }
    this.bankForm.markAllAsTouched();
    if (this.bankForm.valid) {
      let subscription = this._bankService
        .AddUpdateBank(this.Model)
        .subscribe((response) => {
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
                this.routing_Url.Bank_List_Url,
            ]);
          } else {
            this.toast.error(response.Message?.toString(), 'Error');
          }
        });
    }
  }
}
