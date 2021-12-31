import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { FreshLeadHLPLCLModel } from 'src/app/Shared/Model/Leads/other-loan-leads.model';
import { PersonalHomeCarLoanService } from 'src/app/Shared/Services/Leads/personal-home-car-loan.service';

@Component({
  selector: 'app-detail-home-loan-lead',
  templateUrl: './detail-home-loan-lead.component.html',
  styleUrls: ['./detail-home-loan-lead.component.scss'],
  providers:[PersonalHomeCarLoanService]
})
export class DetailHomeLoanLeadComponent implements OnInit {
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
