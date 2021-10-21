import { ToastrService } from 'ngx-toastr';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Routing_Url, Message } from 'src/app/Shared/Helper/constants';
import { AgentListModel } from 'src/app/Shared/Model/Agent/agent.model';
import { AgentService } from 'src/app/Shared/Services/agent-services/agent.service';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { UserSettingService } from '../../../../Shared/Services/user-setting-services/user-setting.service';

@Component({
  selector: 'app-list-agent',
  templateUrl: './list-agent.component.html',
  styleUrls: ['./list-agent.component.scss'],
  providers: [AgentService, UserSettingService]
})
export class ListAgentComponent implements OnInit {


  //#region <<  Variable  >>
  get routing_Url() { return Routing_Url }

  model!: AgentListModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'FullName', 'Gender','Mpin', 'IsActive', 'IsApproved', 'Action'];
  ViewdisplayedColumns = [{ Value: 'FullName', Text: 'Full Name' },
   { Value: 'Gender', Text: 'Gender' },
   {Value: 'Mpin', Text: 'MPIN'},
   { Value: 'Email', Text: 'Email' },
    { Value: 'Mobile', Text: 'Mobile' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  //#endregion

  constructor(private readonly _service: AgentService,
               private readonly _commonService: CommonService,
               private readonly toast: ToastrService,
               private readonly _userSettingService: UserSettingService) { }
  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    let serve = this._service.GetAgentList(this.indexModel).subscribe(response => {
      serve.unsubscribe();
      if (response.IsSuccess) {
        this.model = response.Data as AgentListModel[];
        this.dataSource = new MatTableDataSource<AgentListModel>(this.model);
        this.totalRecords = response.TotalRecord as number;
        if (!this.indexModel.IsPostBack) {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      } else {
        this.toast.warning(response.Message as string, 'Server Error');

      }
    },
      error => {
        this.toast.error(error.Message as string, 'Error');
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

  OnActiveStatus(Id: number) {

    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {

      if (isTrue) {
        let serv = this._service.ChangeActiveStatus(Id).subscribe(
          data => {
            serv.unsubscribe();
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
        let serv = this._service.DeleteAgent(id).subscribe(
          data => {
            serv.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string, 'Removed');
              this.getList();
            } else {
              this.toast.warning(data.Message as string, 'Server Error');
            }
          },
          error => {
            this.toast.error(error.message as string , 'Error');
          }
        );
      }
    });
  }

  OnApproveStatus(Id: number) {
    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {

      if (isTrue) {
        let serv = this._userSettingService.UpdateApproveStatus(Id).subscribe(
          data => {
            serv.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string , 'Access Permission');
              this.getList();
            } else {
              this.toast.warning(data.Message as string , 'Server Error');
            }
          },
          error => {
            this.toast.error(error.Message as string ,'Error');
          }
        );
      }
    });

  }


  //#endregion

}
