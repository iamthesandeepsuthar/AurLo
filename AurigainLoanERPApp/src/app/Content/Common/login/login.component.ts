import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/Shared/Helper/auth.service";
import { environment } from "src/environments/environment";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  userId!: string
  Password!: string;
  constructor(private readonly _authService: AuthService,
    private readonly _route: Router,private readonly toast: ToastrService
  ) { }

  ngOnInit(): void {

  }
  onSubmit() {
    if(this.userId ==  undefined || this.Password == undefined) {
  this.toast.warning('Please enter username and password', 'Required');
  return ;
    }
    if(this.userId == this.Password) {
      if (!environment.IsAutoLogin) {
        this._authService.Login(this.userId).subscribe((res) => {
          if (res.IsSuccess) {
            this._authService.SaveUserToken(res.Data);
            this._route.navigate(['']);
          }
        });
      } else {
        this._authService.SaveUserToken("testtoken");
        this._route.navigate(['']);
      }
    } else {
      this.toast.warning('Please enter valid credential','Not Valid');
    }






  }
}
