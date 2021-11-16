import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Shared/Helper/auth.service';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { UserRoleEnum } from '../../../Shared/Enum/fixed-value';
import { LoginResponseModel } from '../../../Shared/Model/User-setting-model/user-setting.model';
import { environment } from '../../../../environments/environment';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  get routing_Url() { return Routing_Url }
  roleEnum = UserRoleEnum;
  userModel = {} as LoginResponseModel;
  IsAdminMenu: boolean = false;
  IsCustomerMenu: boolean = false;
  IsOperatorMenu: boolean = false;
  isProd = environment.production;
  constructor(private readonly _authService: AuthService) {
    this.IsAdminMenu = false;
    this.IsCustomerMenu = false;
    this.IsOperatorMenu = false;
  }

  ngOnInit(): void {
    this._authService.IsAuthenticate();

    this._authService.IsAuthentication.subscribe(x => {

      this.userModel = this._authService.GetUserDetail() as LoginResponseModel;
      if (this.userModel.RoleId == this.roleEnum.Customer) {
        this.IsCustomerMenu = true;
      } else if(this.userModel.RoleId == this.roleEnum.Operator) {
        this.IsOperatorMenu = true;
      } else {
        this.IsAdminMenu = true;
      }
    });
  }
}
