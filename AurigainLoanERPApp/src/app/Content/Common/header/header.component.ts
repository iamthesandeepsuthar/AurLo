import { LoginResponseModel } from '../../../Shared/Model/User-setting-model/user-setting.model';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AuthService } from '../../../Shared/Helper/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { ChangePasswordDialogComponent } from '../../../Shared/Helper/shared/DialogComponent/change-password-dialog/change-password-dialog.component';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  userLoginModel = {} as LoginResponseModel;
  @Output() SetTheme = new EventEmitter<string>();
  constructor(readonly _authService: AuthService, public dialog: MatDialog) { }

  ngOnInit(): void {
    setTimeout(() => {
      //   this.changeTheme();
    }, 10);
    this.userLoginModel = this._authService.GetUserDetail() as LoginResponseModel;
  }


  changeTheme(themeName: string = '') {
    this.SetTheme.emit(themeName);
  }

  openChangePasswordDialog(): void {
    const dialogRef = this.dialog.open(ChangePasswordDialogComponent, {
      width: '250px',

    });

    dialogRef.afterClosed().subscribe(result => {


    });
  }
}


