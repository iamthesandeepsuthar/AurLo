import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BTGoldLoanLeadViewModel } from 'src/app/Shared/Model/Leads/btgold-loan-lead-post-model.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { BalanceTransferGoldLoanLeadsService } from 'src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail-balance-transfer-lead',
  templateUrl: './detail-balance-transfer-lead.component.html',
  styleUrls: ['./detail-balance-transfer-lead.component.scss'],
  providers: [BalanceTransferGoldLoanLeadsService]
})
export class DetailBalanceTransferLeadComponent implements OnInit {
  LeadId = 0;
  model = {} as BTGoldLoanLeadViewModel;
  constructor(private readonly _freshLeadService: BalanceTransferGoldLoanLeadsService,
    private readonly _commonService: CommonService,
    private readonly toast: ToastrService, private readonly _activatedRoute: ActivatedRoute) {
    if (_activatedRoute.snapshot.params.id) {
      this.LeadId = _activatedRoute.snapshot.params.id;
    }
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
