import { JewellaryTypeModel } from './../../../../../Shared/Model/master-model/jewellary-type-model.model';
import { JewelleryTypeService } from './../../../../../Shared/Services/master-services/jewellery-type.service';
import { Component, OnInit } from '@angular/core';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-detail-jewellary',
  templateUrl: './detail-jewellary.component.html',
  styleUrls: ['./detail-jewellary.component.scss'],
  providers: [JewelleryTypeService]
})
export class DetailJewellaryComponent implements OnInit {
  Id: number = 0;
  model = new JewellaryTypeModel();
  get routing_Url() { return Routing_Url }
  get Model(): JewellaryTypeModel{  return this.model; }

  constructor(private readonly _typeService: JewelleryTypeService,
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
    let subscription = this._typeService.JewelleryTypeById (this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as JewellaryTypeModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }

}
