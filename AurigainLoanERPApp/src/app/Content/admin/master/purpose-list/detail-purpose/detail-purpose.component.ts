import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { PurposeModel } from 'src/app/Shared/Model/master-model/purpose-model.model';
import { PurposeService } from 'src/app/Shared/Services/master-services/purpose.service';

@Component({
  selector: 'app-detail-purpose',
  templateUrl: './detail-purpose.component.html',
  styles: [ ],
  providers:[PurposeService]
})
export class DetailPurposeComponent implements OnInit {
  Id: number = 0;
  model = new PurposeModel();
  purposeForm!: FormGroup;
  get routing_Url() { return Routing_Url }
  get f() { return this.purposeForm.controls; }
  get Model(): PurposeModel{  return this.model; }

  constructor( private readonly _purposeService: PurposeService,
               private _activatedRoute: ActivatedRoute,
               private _router: Router,
               private readonly toast: ToastrService) {
      if (this._activatedRoute.snapshot.params.id) {
        this.Id = this._activatedRoute.snapshot.params.id;
      }
     }

  ngOnInit(): void {
    if (this.Id > 0) {
      this.onGetDetail();
    }
  }
  onGetDetail() {
    let subscription = this._purposeService.GetPurpose(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as PurposeModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }
}
