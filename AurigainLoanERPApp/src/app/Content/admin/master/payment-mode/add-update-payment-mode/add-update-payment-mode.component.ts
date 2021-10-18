
import { Component, OnInit } from '@angular/core';
import { PaymentModeModel } from 'src/app/Shared/Model/master-model/payment-mode.model';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PaymentModeService } from 'src/app/Shared/Services/master-services/payment-mode.service';
@Component({
  selector: 'app-add-update-payment-mode',
  templateUrl: './add-update-payment-mode.component.html',
  styleUrls: ['./add-update-payment-mode.component.scss'],
  providers:[PaymentModeService]
})
export class AddUpdatePaymentModeComponent implements OnInit {
  Id: number = 0;
  model = new PaymentModeModel();
  modeForm!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.modeForm.controls; }
  get Model(): PaymentModeModel{  return this.model; }

  constructor(private readonly fb: FormBuilder,
              private readonly _paymentModeService: PaymentModeService,
              private _activatedRoute: ActivatedRoute,
              private _router: Router,
              private readonly toast: ToastrService) {
      if (this._activatedRoute.snapshot.params.id) {
        this.Id = this._activatedRoute.snapshot.params.id;
      }
     }

    ngOnInit(): void {
      this.formInit();
      if (this.Id > 0) {
        this.onGetDetail();
      }
    }

    formInit() {
      this.modeForm = this.fb.group({
        Name: [undefined, Validators.required],
        IsActive: [true, Validators.required],
      });
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
    onSubmit() {
      this.modeForm.markAllAsTouched();
      if (this.modeForm.valid) {
        let subscription = this._paymentModeService.AddUpdatePaymentMode(this.Model).subscribe(response => {
          subscription.unsubscribe();
          if(response.IsSuccess) {
           this.toast.success( this.Id ==0 ?'Record save successful' : 'Record update successful' , 'Success');
           this._router.navigate([this.routing_Url.AdminModule+'/'+this.routing_Url.MasterModule + this.routing_Url.PaymentMode_List_Url]);
          } else {
            this.toast.error(response.Message?.toString(), 'Error');
          }
        })
      }
    }

}
