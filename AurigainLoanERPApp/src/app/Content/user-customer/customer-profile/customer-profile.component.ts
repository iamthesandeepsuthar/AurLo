import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../../Shared/Services/CustomerService/customer.service';
import { ActivatedRoute } from '@angular/router';
import { CustomerRegistrationViewModel } from 'src/app/Shared/Model/CustomerModel/customer-registration-model.model';

@Component({
  selector: 'app-customer-profile',
  templateUrl: './customer-profile.component.html',
  styleUrls: ['./customer-profile.component.scss'],
  providers: [CustomerService]
})
export class CustomerProfileComponent implements OnInit {
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
