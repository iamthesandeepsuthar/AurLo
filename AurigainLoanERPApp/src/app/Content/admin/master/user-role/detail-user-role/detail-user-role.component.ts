
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonService } from 'src/app/Shared/Services/common.service';

import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { UserRoleService } from 'src/app/Shared/Services/master-services/user-role.service';
import { UserRoleModel } from 'src/app/Shared/Model/master-model/user-role.model';

@Component({
  selector: 'app-detail-user-role',
  templateUrl: './detail-user-role.component.html',
  styleUrls: ['./detail-user-role.component.scss'],
  providers: [UserRoleService]

})
export class DetailUserRoleComponent implements OnInit {
  Id: number = 0;
  model = new UserRoleModel();
  get routing_Url() { return Routing_Url }

  constructor(private readonly _userRole: UserRoleService,
    private readonly _commonService: CommonService, private _activatedRoute: ActivatedRoute) {

    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
      this.onGetDetail();
    }


  }
  ngOnInit(): void {
  }

  onGetDetail() {
    this._userRole.GetRole(this.Id).subscribe(res => {
      if (res.IsSuccess) {
        this.model = res.Data as UserRoleModel;
      } else {

      }
    });
  }


}
