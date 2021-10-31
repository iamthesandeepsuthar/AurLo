import { ToastrService } from 'ngx-toastr';
import { BankBranchService } from './../../../../../Shared/Services/master-services/bank-branch.service';
import { Component, OnInit } from '@angular/core';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { BankModel } from 'src/app/Shared/Model/master-model/bank-model.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail-bank',
  templateUrl: './detail-bank.component.html',
  styleUrls: ['./detail-bank.component.scss'],
  providers :[BankBranchService]
})
export class DetailBankComponent implements OnInit {
  Id: number = 0;
  model = new BankModel();
  get routing_Url() { return Routing_Url }
  get Model(): BankModel{  return this.model; }

  constructor(private readonly _bankService: BankBranchService,
              private _activatedRoute: ActivatedRoute ,
              private readonly toast: ToastrService ) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
      this.onGetDetail();
    }
  }
  ngOnInit(): void {
    this.onGetDetail();
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

}
