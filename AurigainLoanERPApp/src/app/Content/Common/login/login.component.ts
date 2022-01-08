import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/Shared/Helper/auth.service";
import { environment } from "src/environments/environment";
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { LoginPostModel, LoginResponseModel } from 'src/app/Shared/Model/User-setting-model/user-setting.model';
import { UserRoleEnum } from '../../../Shared/Enum/fixed-value';
import { DashboardModule } from '../../dashboard/dashboard.module';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  isShowPass = false;
  model = {} as LoginPostModel;
  get routing_Url() { return Routing_Url };

  constructor(private readonly _authService: AuthService,
    private readonly _route: Router, private readonly toast: ToastrService
  ) { }

  ngOnInit(): void {
  }

  onSubmit() {
    this.model.Plateform = "Web";
    if (this.model.MobileNumber == undefined || this.model.Password == undefined) {
      this.toast.warning('Please enter username and password', 'Required');
      return;
    }
    if (!environment.IsAutoLogin) {
      this._authService.Login(this.model).subscribe((res) => {
        if (res.IsSuccess) {
          let data = res.Data as LoginResponseModel;
          this._authService.SaveUserToken(data.Token);
          this._authService.SaveUserDetail(data);
          // switch (data.RoleId) {
          //   case UserRoleEnum.Customer:
          //     this._route.navigate([Routing_Url.UserCustomerModule]);

          //     break;
          //   case UserRoleEnum.Operator:
          //     this._route.navigate([Routing_Url.UserBackendOperatorModule]);

          //     break;

          //   default:
          setTimeout(() => {
            location.reload();
            this.toast.success(res.Message?.toString(), 'Login Response');
          }, 0);
          this._route.navigate(['']);

          //     break;
          // }


        } else {
          this.toast.info(res.Message?.toString(), 'Login Response');
        }
      });
    } else {
      this._authService.SaveUserToken("testtoken");
      this._route.navigate(['']);
    }
  }

}
