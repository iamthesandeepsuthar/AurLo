import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { CustomerRegistrationViewModel } from 'src/app/Shared/Model/CustomerModel/customer-registration-model.model';
import { CustomerService } from 'src/app/Shared/Services/CustomerService/customer.service';

@Component({
  selector: 'app-detail-customer',
  templateUrl: './detail-customer.component.html',
  styleUrls: ['./detail-customer.component.scss'],
  providers: [CustomerService]
})
export class DetailCustomerComponent implements OnInit {
  get routing_Url() { return Routing_Url };

  userId: number = 0;
  model = {} as CustomerRegistrationViewModel;
  constructor(private readonly _customerService: CustomerService, private readonly _activatedRoute: ActivatedRoute) {
  }

  ngOnInit(): void {
    if (this._activatedRoute.snapshot.params.id) {
      this.userId = this._activatedRoute.snapshot.params.id;
      this.getUserDetail();
    }
  }
  getUserDetail() {
    let serve = this._customerService.GetCustomerProfile(this.userId).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.model = res.Data as CustomerRegistrationViewModel;
      }
      else {

      }
    });

  }
}
