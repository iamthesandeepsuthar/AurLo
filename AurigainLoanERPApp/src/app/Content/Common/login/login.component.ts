import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Shared/Helper/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  userId!: string
  Password!: string;
  constructor(private readonly _authService: AuthService,
    private readonly _route: Router,
  ) { }

  ngOnInit(): void {
    this.Login();
  }
  Login() {
    this._authService.Login(this.userId).subscribe((res) => {
      if (res.IsSuccess) {
        this._authService.SaveUserToken(res.Data);
        this._route.navigate(['']);
      }
    });
  }
}
