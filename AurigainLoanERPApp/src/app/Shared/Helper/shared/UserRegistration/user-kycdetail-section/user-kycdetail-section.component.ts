import { AlertService } from './../../../../Services/alert.service';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { UserKYCPostModel } from '../../../../Model/doorstep-agent-model/door-step-agent.model';
import { DropDownModol } from '../../../common-model';
import { DropDown_key } from '../../../constants';

@Component({
  selector: 'app-user-kycdetail-section',
  templateUrl: './user-kycdetail-section.component.html',
  styleUrls: ['./user-kycdetail-section.component.scss']
})
export class UserKYCDetailSectionComponent implements OnInit {
  @Input() kycModel: UserKYCPostModel[] = [] as UserKYCPostModel[];

  model: UserKYCPostModel = {} as UserKYCPostModel;
  dropDown = new DropDownModol();
  get ddlkeys() { return DropDown_key };

  constructor(private readonly fb: FormBuilder, private readonly _commonService: CommonService, private readonly _alertService: AlertService) { }

  ngOnInit(): void {
    this.GetDropDown();
    this.model.Kycnumber
  }

  GetDropDown() {

    this._commonService.GetDropDown([DropDown_key.ddlDocumentType]).subscribe(res => {
      if (res.IsSuccess) {
        debugger
        let ddls = res.Data as DropDownModol;
        this.dropDown.ddlDocumentType = ddls.ddlDocumentType;
      }
    });
  }

  getDocumentType(value: string | number) {
    return this.dropDown.ddlDocumentType.find(x => x.Value == value)?.Text;
  }
  AddEditKycItem() {

    if (this.model.Kycnumber.length > 0 && this.model.KycdocumentTypeId > 0) {
      let itm = this.kycModel.findIndex(x => x.KycdocumentTypeId == this.model.KycdocumentTypeId);
      if (itm < 0) {
        this.kycModel.push(this.model);
      } else {
        this._alertService.Warning("Kyc Document Detail Already exist!");
      }
      this.model = {} as UserKYCPostModel;
    }
  }

  fillKycItem(index: number) {
    if (index >= 0) {
      let item = this.kycModel.find((x, i) => i == index) as UserKYCPostModel;
      this.model = {} as UserKYCPostModel;
      this.model.Id = item!.Id;
      this.model.KycdocumentTypeId = item!.KycdocumentTypeId;
      this.model.Kycnumber = item!.Kycnumber;
      this.kycModel.splice(index, 1);
    }
  }
  removeKycItem(index: number) {
    if (index >= 0) {

      this.kycModel.splice(index, 1);
    }
  }

}
