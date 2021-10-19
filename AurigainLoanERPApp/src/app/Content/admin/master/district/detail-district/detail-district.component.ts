import { DistrictModel } from 'src/app/Shared/Model/master-model/district.model';
import { Component, OnInit } from '@angular/core';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-detail-district',
  templateUrl: './detail-district.component.html',
  styleUrls: ['./detail-district.component.scss'],
  providers: [StateDistrictService]
})
export class DetailDistrictComponent implements OnInit {
  Id: number = 0;
  model = new DistrictModel();
  get routing_Url() {
    return Routing_Url;
  }
  get Model(): DistrictModel {
    return this.model;
  }
  constructor(
    private readonly _districtService: StateDistrictService,
    private _activatedRoute: ActivatedRoute,
    private readonly toast: ToastrService
  ) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
    }
  }

  ngOnInit(): void {
    this.onGetDetail();
  }
  onGetDetail() {
    let subscription = this._districtService
      .GetDistrict(this.Id)
      .subscribe((res) => {
        subscription.unsubscribe();
        if (res.IsSuccess) {
          debugger;
          this.model = res.Data as DistrictModel;
        } else {
          this.toast.warning('Record not found', 'No Record');
        }
      });
  }
}
