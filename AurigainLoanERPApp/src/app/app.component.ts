import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './Shared/Helper/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'AurigainLoanERPApp';

  isAuth: boolean =  false;
  constructor(public  _authService: AuthService , private readonly _route: Router) {
   this._authService.IsAuthenticate();
  this._authService.IsAuthentication.subscribe(x=>{
      this.isAuth = x;
  });
  }
}
