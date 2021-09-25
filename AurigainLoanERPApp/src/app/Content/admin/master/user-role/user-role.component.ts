import { Constants } from './../../../../Shared/Helper/constants';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { UserRoleModel } from 'src/app/Shared/Model/user-role.model';
import { UserRoleService } from 'src/app/Shared/Services/user-role.service';

@Component({
  selector: 'app-user-role',
  templateUrl: './user-role.component.html',
  styleUrls: ['./user-role.component.scss'],
  providers :[ UserRoleService]
})
export class UserRoleComponent implements OnInit {

  //#region <<  Variable  >>

  model!: UserRoleModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'Name', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' }];
  indexModel = new IndexModel();
  totalRecords?: number = 0;
  //#endregion

  constructor(private readonly _userRole: UserRoleService) { }

  ngOnInit(): void {
    this.getList();
  }

  getList() {
    debugger
    this._userRole.GetRoleList(this.indexModel).subscribe(responce => {
debugger
      if (responce.IsSuccess) {
        this.model = responce.Data as UserRoleModel[];
        this.dataSource = new MatTableDataSource<UserRoleModel>(this.model);
        this.totalRecords = responce.TotalRecord;
        if (!this.indexModel.IsPostBack) {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      }
    },
      error => {
      });
  }

  sortData(event: any) {
    this.indexModel.OrderBy = event.active;
    this.indexModel.OrderByAsc = event.direction == 1 ? 1 : 0;
    this.indexModel.IsPostBack = true;
    this.getList();
  }

  onPaginateChange(event: any) {
    this.indexModel.Page = event.pageIndex + 1;
    this.indexModel.PageSize = event.pageSize;
    this.indexModel.IsPostBack = true;
    this.getList();
  }

  OnActiveStatus(Id: number) {

    // const dialogRef = this._dialog.open(ConfirmationDialogComponent, {
    //   width: '350px',
    //   data: Constants.ConfirmUpdate,
    //   disableClose: true
    // });
    // dialogRef.afterClosed().subscribe(isTrue => {

    //   if (isTrue) {
    this._userRole.ChangeActiveStatus(Id).subscribe(
      data => {

        if (data.IsSuccess) {
          this.getList();
          alert(data.Message);

        }
      },
      error => {
        // this._commonService.ScrollingTop();
        alert(error.message);
      }
    );
    //   }
    // });

  }
  updateDeleteStatus(id: number) {
    // const dialogRef = this._dialog.open(ConfirmationDialogComponent, {
    //   width: "350px",
    //   data: GlobalMessagesModel.ConfirmDelete
    // });
    // dialogRef.afterClosed().subscribe(result => {
    //   if (result) {

    this._userRole.DeleteRole(id).subscribe(
      data => {
        if (data.IsSuccess) {
          alert(data.Message);
          this.getList();
        }
      },
      error => {
        alert(error.message);
      }
    );
    //   }
    // });
  }


  //#endregion

}
