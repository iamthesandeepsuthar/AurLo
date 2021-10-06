import { Message, Routing_Url } from './../../../../Shared/Helper/constants';

import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { IndexModel } from 'src/app/Shared/Helper/common-model';

import { UserRoleService } from 'src/app/Shared/Services/master-services/user-role.service';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { UserRoleModel } from 'src/app/Shared/Model/master-model/user-role.model';

@Component({
  selector: 'app-user-role',
  templateUrl: './user-role.component.html',
  styleUrls: ['./user-role.component.scss'],
  providers: [UserRoleService]
})
export class UserRoleComponent implements OnInit {

  //#region <<  Variable  >>
  get routing_Url() { return Routing_Url }

  model!: UserRoleModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'Name', 'ParentRole', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' }, { Value: 'ParentRole', Text: 'Parent Role' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  //#endregion

  constructor(private readonly _userRole: UserRoleService, private readonly _commonService: CommonService) { }
  ngOnInit(): void {
    this.getList();
  }

  getList(): void {

    this._userRole.GetRoleList(this.indexModel).subscribe(response => {

      if (response.IsSuccess) {
        debugger
        this.model = response.Data as UserRoleModel[];
        this.dataSource = new MatTableDataSource<UserRoleModel>(this.model);
        this.totalRecords = response.TotalRecord as number;
        if (!this.indexModel.IsPostBack) {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      } else {
        // Toast message if  return false ;
      }
    },
      error => {
      });
  }

  sortData(event: any): void {
    debugger
    this.indexModel.OrderBy = event.active;
    this.indexModel.OrderByAsc = event.direction == "asc" ? true : false;
    this.indexModel.IsPostBack = true;
    this.getList();
  }
  onSearch() {
    this.indexModel.Page = 1;
    this.getList();
  }

  onPaginateChange(event: any) {
    this.indexModel.Page = event.pageIndex + 1;
    this.indexModel.PageSize = event.pageSize;
    this.indexModel.IsPostBack = true;
    this.getList();
  }

  OnActiveStatus(Id: number) {

    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {

      if (isTrue) {
        this._userRole.ChangeActiveStatus(Id).subscribe(
          data => {
            if (data.IsSuccess) {
              this._commonService.Success(data.Message as string)
              this.getList();
            }
          },
          error => {
            this._commonService.Error(error.message as string)

          }
        );
      }
    });

  }
  updateDeleteStatus(id: number) {

    this._commonService.Question(Message.ConfirmUpdate as string).then(result => {
      if (result) {
        this._userRole.DeleteRole(id).subscribe(
          data => {
            if (data.IsSuccess) {
              this._commonService.Success(data.Message as string)
              this.getList();
            }
          },
          error => {
            this._commonService.Error(error.message as string)

          }
        );
      }
    });
  }


  //#endregion

}
