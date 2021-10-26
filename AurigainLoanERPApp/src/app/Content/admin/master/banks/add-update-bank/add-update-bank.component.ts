import { BankBranchService } from './../../../../../Shared/Services/master-services/bank-branch.service';
import { BankModel, BranchModel } from './../../../../../Shared/Model/master-model/bank-model.model';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { retryWhen } from 'rxjs/operators';

@Component({
  selector: 'app-add-update-bank',
  templateUrl: './add-update-bank.component.html',
  styleUrls: ['./add-update-bank.component.scss'],
  providers: [BankBranchService]
})
export class AddUpdateBankComponent implements OnInit {
  Id: number = 0;
  IsBranchUpdate: boolean = false;
  model = new BankModel();
  branchModel = new BranchModel();
  bankForm!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.bankForm.controls; }
  get Model(): BankModel{  return this.model; }
  set Model(value: BankModel) {  this.model = value; }
  get BranchModel(): BranchModel {return this.branchModel;}
  set BranchModel(value: BranchModel) {   this.branchModel = value; }

  constructor(private readonly fb: FormBuilder,
    private readonly _bankService: BankBranchService,
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private readonly toast: ToastrService) {
    this.model.IsActive = true;
if (this._activatedRoute.snapshot.params.id) {
this.Id = this._activatedRoute.snapshot.params.id;
}}
ngOnInit(): void {
  this.formInit();
  if (this.Id > 0) {
  this.onGetDetail();
  }
  }
  formInit() {
    this.bankForm = this.fb.group({
    Name: [undefined, Validators.required],
    IsActive: [true, Validators.required],
    Website: [undefined],
    Contact: [undefined , Validators.required],
    FaxNumber: [undefined],
    BranchName: [undefined],
    BranchCode:[undefined],
    BranchEmail:[undefined],
    BranchIfsc:[undefined],
    BranchAddress:[undefined],
    BranchContact:[undefined],
    BranchIsActive:[undefined]
    });
  }
  onGetDetail() {
    let subscription = this._bankService.BankById(this.Id).subscribe(res => {
    subscription.unsubscribe();
    if (res.IsSuccess) {
    this.model = res.Data as BankModel;
    } else {
    this.toast.warning('Record not found', 'No Record');
    }
    });
  }

  addBranch() {
  this.branchModel = new BranchModel();
  this.branchModel.IsActive = true;
  }

  addToBranchList() {

    if(this.branchModel.BranchName == undefined && this.BranchModel.Ifsc == undefined && this.branchModel.BranchCode == undefined) {
      this.toast.info('Please enter required * field of branch', 'Required');
      return;
    } else {
      if(this.model.Branches.find(x=>x.BranchName == this.branchModel.BranchName && x.Ifsc == this.branchModel.Ifsc)) {
        this.toast.warning('branch already exist into list', 'Duplicate');
        return;
      } else {
        this.model.Branches.push(this.branchModel);
        this.branchModel = new BranchModel();
        this.BranchModel.IsActive = true;
        this.IsBranchUpdate = false;
      }
    }

  }

  removeToBranchList(index: number) {
    this.model.Branches.splice(index,1);
  }
  getFromBranchList(index: number) {
    this.branchModel =  this.model.Branches[index] as BranchModel;
    this.model.Branches.splice(index,1);
    this.IsBranchUpdate = true;
  }

  onSubmit() {
    if(this.model.Branches.length<1) {
     this.toast.warning('Add at least one branch with bank', 'Branch Required');
     return;
    }
    this.bankForm.markAllAsTouched();
    if (this.bankForm.valid) {
    let subscription = this._bankService.AddUpdateBank(this.Model).subscribe(response => {
    subscription.unsubscribe();
    if(response.IsSuccess) {
     this.toast.success( this.Id ==0 ?'Record save successful' : 'Record update successful' , 'Success');
     this._router.navigate([this.routing_Url.AdminModule+'/'+this.routing_Url.MasterModule + this.routing_Url.Bank_List_Url]);
    } else {
      this.toast.error(response.Message?.toString(), 'Error');
    }
    })
    }
  }

}
