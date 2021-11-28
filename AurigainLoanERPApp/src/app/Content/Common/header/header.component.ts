import { LoginResponseModel } from 'src/app/Shared/Model/User-setting-model/user-setting.model';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from '../../../Shared/Helper/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  userLoginModel = {} as LoginResponseModel;
  @Output() SetTheme = new EventEmitter<string>();
  constructor(readonly _authService: AuthService) { }

  ngOnInit(): void {
    setTimeout(() => {
      //   this.changeTheme();
    }, 10);
    this.userLoginModel = this._authService.GetUserDetail() as LoginResponseModel;
  }


  changeTheme(themeName: string = '') {
    this.SetTheme.emit(themeName);
  }
}


