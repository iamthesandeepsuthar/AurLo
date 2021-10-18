import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { StateModel } from 'src/app/Shared/Model/master-model/state.model';
import { Routing_Url } from 'src/app/Shared/Helper/constants';

@Component({
  selector: 'app-detail-state',
  templateUrl: './detail-state.component.html',
  styleUrls: ['./detail-state.component.scss'],
  providers: [StateDistrictService]
})
export class DetailStateComponent implements OnInit {

  Id: number = 0;
  model = new StateModel();
  get routing_Url() { return Routing_Url }
  get Model(): StateModel{  return this.model; }

  constructor(private readonly _stateService: StateDistrictService,
              private _activatedRoute: ActivatedRoute ,
              private readonly toast: ToastrService) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
      this.onGetDetail();
    }
  }
  ngOnInit(): void {
    this.onGetDetail();
  }
  onGetDetail() {
    let subscription = this._stateService.GetState(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as StateModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }

}
