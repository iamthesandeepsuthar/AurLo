import { ToastrService } from 'ngx-toastr';
import { PaymentModeModel } from './../../../../../Shared/Model/master-model/payment-mode.model';
import { Component, OnInit } from '@angular/core';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute } from '@angular/router';
import { PaymentModeService } from 'src/app/Shared/Services/master-services/payment-mode.service';

@Component({
  selector: 'app-detail-payment-mode',
  templateUrl: './detail-payment-mode.component.html',
  styleUrls: ['./detail-payment-mode.component.scss'],
  providers: [PaymentModeService]
})
export class DetailPaymentModeComponent implements OnInit {

  Id: number = 0;
  model = new PaymentModeModel();
  get routing_Url() { return Routing_Url }
  get Model(): PaymentModeModel{  return this.model; }

  constructor(private readonly _paymentModeService: PaymentModeService,
              private _activatedRoute: ActivatedRoute ,
              private readonly toast: ToastrService) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
    }
  }
  ngOnInit(): void {
    this.onGetDetail();
  }
  onGetDetail() {
    let subscription = this._paymentModeService.GetPaymentMode(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as PaymentModeModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }

}
