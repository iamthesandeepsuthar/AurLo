
import { QualificationService } from './../../../../../Shared/Services/master-services/qualification.service';
import { Component, OnInit } from '@angular/core';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { QualificationModel } from 'src/app/Shared/Model/master-model/qualification.model';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-detail-qualification',
  templateUrl: './detail-qualification.component.html',
  styleUrls: ['./detail-qualification.component.scss'],
  providers: [QualificationService]
})
export class DetailQualificationComponent implements OnInit {
  Id: number = 0;
  model = new QualificationModel();
  get routing_Url() { return Routing_Url }
  get Model(): QualificationModel{  return this.model; }

  constructor(private readonly _qualificationService: QualificationService,
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
    let subscription = this._qualificationService.GetQualification(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as QualificationModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }
}
