import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { IndexModel } from "src/app/Shared/Helper/common-model";
import { Routing_Url, Message } from "src/app/Shared/Helper/constants";
import { DoorStepAgentListModel, DoorStepAgentViewModel } from "src/app/Shared/Model/doorstep-agent-model/door-step-agent.model";
import { CommonService } from "src/app/Shared/Services/common.service";
import { DoorStepAgentService } from "src/app/Shared/Services/door-step-agent-services/door-step-agent.service";
import { UserSettingService } from 'src/app/Shared/Services/user-setting-services/user-setting.service';
import { ReportingPersonPopupComponent } from 'src/app/Shared/Helper/shared/Popup/reporting-person-popup/reporting-person-popup.component';

@Component({
  selector: 'app-list-door-step-agent',
  templateUrl: './list-door-step-agent.component.html',
  styleUrls: ['./list-door-step-agent.component.scss'],
  providers: [DoorStepAgentService, UserSettingService]
})
export class ListDoorStepAgentComponent implements OnInit {


  //#region <<  Variable  >>
  get routing_Url() { return Routing_Url };

  model!: DoorStepAgentListModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'FullName', 'Gender', 'IsApproved', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'FullName', Text: 'Full Name' },
  { Value: 'Gender', Text: 'Gender' },
  { Value: 'Email', Text: 'Email' },
  { Value: 'Mobile', Text: 'Mobile' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  isTableView: boolean = true;
  //#endregion

  constructor(private readonly _service: DoorStepAgentService,
    private readonly _commonService: CommonService,
    private readonly toast: ToastrService,
    private readonly _userSettingService: UserSettingService,
    public dialog: MatDialog) { }
  ngOnInit(): void {
    this.getList();
  }
  getList(): void {

    let serve = this._service.GetDoorStepAgentList(this.indexModel).subscribe(response => {
      serve.unsubscribe();
      if (response.IsSuccess) {
        this.model = response.Data as DoorStepAgentListModel[];
        this.dataSource = new MatTableDataSource<DoorStepAgentListModel>(this.model);
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
    this.indexModel.IsPostBack = true;
    this.getList();
  }
  onPageSizeChange() {

    this.indexModel.IsPostBack = true;
    this.indexModel.PageSize = Number(this.indexModel.PageSize);
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
        let subscription = this._service.ChangeActiveStatus(Id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string, 'Status Change');
              this.getList();
            } else {
              this.toast.warning(data.Message as string, 'Server Error');
            }
          },
          error => {
            this.toast.error(error.message as string, 'Error');
          }
        );
      }
    });

  }
  updateDeleteStatus(id: number) {

    this._commonService.Question(Message.ConfirmUpdate as string).then(result => {
      if (result) {
        let subscription = this._service.DeleteDoorStepAgent(id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string, 'Remove');
              this.getList();
            } else {
              this.toast.warning(data.Message as string, 'Server Error');
            }
          },
          error => {
            this.toast.error(error.message as string, 'Error');

          }
        );
      }
    });
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
  onChangeAddReportingPerson(Id: number){
    const dialogRef = this.dialog.open(ReportingPersonPopupComponent, {
      data: { Id: Id as number, Type: "DoorStepAgent" as string ,Heading:'Reporting Person' },
      width: '400px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
      } else {
      }
    });
  }
  //#endregion

}
