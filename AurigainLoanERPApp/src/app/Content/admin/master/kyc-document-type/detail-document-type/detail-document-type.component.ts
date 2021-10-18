import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { DocumentTypeModel } from 'src/app/Shared/Model/master-model/document-type.model';
import { KycDocumentTypeService } from 'src/app/Shared/Services/master-services/kyc-document-type.service';

@Component({
  selector: 'app-detail-document-type',
  templateUrl: './detail-document-type.component.html',
  styleUrls: ['./detail-document-type.component.scss'],
  providers:[KycDocumentTypeService]
})
export class DetailDocumentTypeComponent implements OnInit {

  Id: number = 0;
  model = new DocumentTypeModel();
  get routing_Url() { return Routing_Url }
  get Model(): DocumentTypeModel{  return this.model; }

  constructor(private readonly _documentTypeService: KycDocumentTypeService,
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
    let subscription = this._documentTypeService.GetDocumentType(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as DocumentTypeModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }

}
