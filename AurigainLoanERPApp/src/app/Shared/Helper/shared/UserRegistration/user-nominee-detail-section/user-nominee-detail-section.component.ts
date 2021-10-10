import { UserNomineePostModel } from './../../../../Model/doorstep-agent-model/door-step-agent.model';
import { Component, Input, OnInit, EventEmitter, Output } from '@angular/core';
import { DropDownModol } from '../../../common-model';
import { DropDown_key } from '../../../constants';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CommonService } from 'src/app/Shared/Services/common.service';

@Component({
  selector: 'app-user-nominee-detail-section',
  templateUrl: './user-nominee-detail-section.component.html',
  styleUrls: ['./user-nominee-detail-section.component.scss']
})
export class UserNomineeDetailSectionComponent implements OnInit {
  @Input() nomineeModel= new UserNomineePostModel();
  @Output() onSubmit = new EventEmitter<UserNomineePostModel>();
  dropDown = new DropDownModol();
  get ddlkeys() { return DropDown_key };
  formGroup = {} as FormGroup;

  get f() { return this.formGroup.controls; }
  constructor(private readonly fb: FormBuilder, private readonly _commonService: CommonService) { }

  ngOnInit(): void {
    this.GetDropDown();
  }

  GetDropDown() {

    this._commonService.GetDropDown([DropDown_key.ddlRelationship]).subscribe(res => {
      if (res.IsSuccess) {
        
        let ddls = res.Data as DropDownModol;
        this.dropDown.ddlRelationship = ddls.ddlRelationship;
      }
    });
  }
  onFrmSubmit() {
    this.onSubmit.emit(this.nomineeModel);
  }
}
