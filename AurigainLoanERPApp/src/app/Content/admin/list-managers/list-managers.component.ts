import { UserSettingService } from 'src/app/Shared/Services/user-setting-services/user-setting.service';
import { UserManagerModel } from './../../../Shared/Model/master-model/user-manager-model.model';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Message, Routing_Url } from 'src/app/Shared/Helper/constants';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { ManagerService } from 'src/app/Shared/Services/master-services/manager.service';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-list-managers',
  templateUrl: './list-managers.component.html',
  styleUrls: ['./list-managers.component.scss'],
  providers: [ManagerService,UserSettingService]
})
export class ListManagersComponent implements OnInit {

  get routing_Url() { return Routing_Url }
  model!: UserManagerModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'FullName','Mobile','Mpin','RoleName','IsApproved', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'FullName', Text: 'Name' },
                          { Value:'RoleName', Text:'Role'},
                          {Value: 'Mobile', Text: 'Mobile Number'},
                          { Value: 'Mpin' , Text: 'MPin'}];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  constructor(private readonly _managerService: ManagerService,
              private readonly _commonService: CommonService,
              private readonly toast: ToastrService,
              private readonly _userSettingService:UserSettingService) { }

  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    let subscription = this._managerService.GetUserManagerList(this.indexModel).subscribe(response => {
       subscription.unsubscribe();
      if (response.IsSuccess) {
        this.model = response.Data as UserManagerModel[];
        this.dataSource = new MatTableDataSource<UserManagerModel>(this.model);
        this.totalRecords = response.TotalRecord as number;
        if (!this.indexModel.IsPostBack) {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      } else {
        // Toast message if  return false ;
        this.toast.error(response.Message?.toString(), 'Record Not Found');
      }
    },
      error => {
      });
  }

  sortData(event: any): void {
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
  OnApproveStatus(Id: number) {
    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {
      if (isTrue) {
        let subscription = this._userSettingService.UpdateApproveStatus(Id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string, 'Access Permission');
              this.getList();
            } else {
              this.toast.warning(data.Message as string, 'Server Error');
            }
          },
          error => {
            this.toast.error(error.Message as string, 'Error');
          }
        );
      }
    });

  }

  OnActiveStatus(Id: number) {

    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {

      if (isTrue) {
        this._managerService.ChangeActiveStatus(Id).subscribe(
          data => {
            if (data.IsSuccess) {
              this.toast.success(data.Message as string, 'Status Change');
              this.getList();
            } else {
              this.toast.error(data. Message as string, 'Server Error');
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
        this._managerService.DeleteUserManager(id).subscribe(
          data => {
            if (data.IsSuccess) {
             this.toast.success(data.Message?.toString(), 'Remove');
              this.getList();
            } else {
              this.toast.error(data.Message as string, 'Server Error');
            }
          },
          error => {
            this.toast.error(error.message as string, 'Error');

          }
        );
      }
    });
  }
}
