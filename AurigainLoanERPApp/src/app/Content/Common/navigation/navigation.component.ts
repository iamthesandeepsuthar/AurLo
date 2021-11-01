import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Shared/Helper/auth.service';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { UserRoleEnum } from '../../../Shared/Enum/fixed-value';
import { LoginResponseModel } from '../../../Shared/Model/User-setting-model/user-setting.model';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  get routing_Url() { return Routing_Url }
  roleEnum = UserRoleEnum;
  userModel = {} as LoginResponseModel;
  constructor(private readonly _authService: AuthService) { }

  ngOnInit(): void {

    this._authService.IsAuthentication.subscribe(x => {
      this.userModel = this._authService.GetUserDetail() as LoginResponseModel;

    });
  }

}
