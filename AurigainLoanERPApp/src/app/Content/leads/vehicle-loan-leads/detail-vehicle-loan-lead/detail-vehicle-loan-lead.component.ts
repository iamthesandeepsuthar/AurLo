import { PersonalHomeCarLoanService } from './../../../../Shared/Services/Leads/personal-home-car-loan.service';
import { Component, OnInit } from '@angular/core';
import { FreshLeadHLPLCLModel } from 'src/app/Shared/Model/Leads/other-loan-leads.model';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detail-vehicle-loan-lead',
  templateUrl: './detail-vehicle-loan-lead.component.html',
  styleUrls: ['./detail-vehicle-loan-lead.component.scss'],
  providers:[PersonalHomeCarLoanService]
})
export class DetailVehicleLoanLeadComponent implements OnInit {
  model!:FreshLeadHLPLCLModel;
  get Model(): FreshLeadHLPLCLModel {
    return this.model;
  }
  Id: number = 0;
  get routing_Url() {
   return Routing_Url;
 }
    constructor(private readonly _otherLeadService: PersonalHomeCarLoanService,
               private readonly _toast: ToastrService,
               private _activatedRoute: ActivatedRoute,) {
                 this.model = new FreshLeadHLPLCLModel();
                 if (this._activatedRoute.snapshot.params.id) {
                   this.Id = this._activatedRoute.snapshot.params.id;
                 }
      }
   ngOnInit(): void {
     this.getDetail();
   }
   getDetail() {
     let subscription = this._otherLeadService.GetById(this.Id).subscribe(response =>{
       subscription.unsubscribe();
       if(response.IsSuccess) {
         this.model = response.Data as FreshLeadHLPLCLModel;
         console.log(this.model);
       } else {
         this._toast.error(response.Message as string , 'No Record Found');
         return;
       }
     });
   }
}
