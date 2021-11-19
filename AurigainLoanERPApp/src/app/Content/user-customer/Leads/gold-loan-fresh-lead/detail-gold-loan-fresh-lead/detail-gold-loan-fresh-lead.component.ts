import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { GoldLoanFreshLeadAppointmentDetailViewModel, GoldLoanFreshLeadJewelleryDetailViewModel, GoldLoanFreshLeadKycDocumentViewModel, GoldLoanFreshLeadViewModel } from 'src/app/Shared/Model/Leads/gold-loan-fresh-lead.model';
import { GoldLoanLeadsService } from 'src/app/Shared/Services/Leads/gold-loan-leads.service';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail-gold-loan-fresh-lead',
  templateUrl: './detail-gold-loan-fresh-lead.component.html',
  styleUrls: ['./detail-gold-loan-fresh-lead.component.scss'],
  providers:[GoldLoanLeadsService]
})
export class DetailGoldLoanFreshLeadComponent implements OnInit {
  Id: number = 0;
  model!:GoldLoanFreshLeadViewModel;
  get routing_Url() {
    return Routing_Url;
  }
  constructor(private readonly _freshService: GoldLoanLeadsService,
              private readonly toast: ToastrService,
              private _activatedRoute: ActivatedRoute,) {
                this.model = new GoldLoanFreshLeadViewModel();
                if (this._activatedRoute.snapshot.params.id) {
                  this.Id = this._activatedRoute.snapshot.params.id;
                }
               }

  ngOnInit(): void {
    this.GetGoldLoanFreshLead();
  }
  GetGoldLoanFreshLead() {
    let subscription = this._freshService.GetGoldLoanFreshLeadDetailById(this.Id).subscribe(response => {
    subscription.unsubscribe();
    if(response.IsSuccess) {
    this.model = response.Data as GoldLoanFreshLeadViewModel;
    console.log(this.model);
    } else {
    this.toast.warning('Record Not Found','Server Error');
    return;
    }
    });
  }

}
