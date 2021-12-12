import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { BTGoldLoanLeadViewModel } from 'src/app/Shared/Model/Leads/btgold-loan-lead-post-model.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { BalanceTransferGoldLoanLeadsService } from 'src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service';

@Component({
  selector: 'app-cudetail-balance-transfer-gold-loan-lead',
  templateUrl: './cudetail-balance-transfer-gold-loan-lead.component.html',
  styleUrls: ['./cudetail-balance-transfer-gold-loan-lead.component.scss'],
  providers: [BalanceTransferGoldLoanLeadsService]
})
export class CUDetailBalanceTransferGoldLoanLeadComponent implements OnInit {

  LeadId = 0;
  model = {} as BTGoldLoanLeadViewModel;
  get routing_Url() { return Routing_Url };


  constructor(private readonly _freshLeadService: BalanceTransferGoldLoanLeadsService,
    private readonly _commonService: CommonService,
    private readonly toast: ToastrService, private readonly _activatedRoute: ActivatedRoute) {
    if (_activatedRoute.snapshot.params.id) {
      this.LeadId = _activatedRoute.snapshot.params.id;
    }
  }
  get Model(): BTGoldLoanLeadViewModel {
    return this.model;
  }

  ngOnInit(): void {
    if (this.LeadId > 0) {
      this.GetById();
    }
  }

  GetById(): void {
    let serve = this._freshLeadService.GetById(this.LeadId).subscribe(response => {
      serve.unsubscribe();
      if (response.IsSuccess) {
        this.model = response.Data as BTGoldLoanLeadViewModel;
      }
    }, errors => {

    });
  }
}