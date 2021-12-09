import { LoginResponseModel } from 'src/app/Shared/Model/User-setting-model/user-setting.model';
import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../Shared/Helper/auth.service';
import { UserRoleEnum } from '../../Shared/Enum/fixed-value';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  get UserDetal() { return this._auth.GetUserDetail() as LoginResponseModel };
  UserRoleEnum = UserRoleEnum;
  constructor(readonly _auth: AuthService) { }

  ngOnInit(): void {
  }

}
