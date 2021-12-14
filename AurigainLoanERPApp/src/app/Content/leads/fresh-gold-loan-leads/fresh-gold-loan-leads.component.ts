import { LeadStatuslHistoryPopupComponent } from 'src/app/Shared/Helper/shared/Popup/lead-statusl-history-popup/lead-statusl-history-popup.component';
import { LeadStatusPopupComponent } from './../../../Shared/Helper/shared/Popup/lead-status-popup/lead-status-popup.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Routing_Url, Message } from 'src/app/Shared/Helper/constants';
import { GoldLoanFreshLeadListModel } from 'src/app/Shared/Model/Leads/gold-loan-fresh-lead.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { GoldLoanLeadsService } from 'src/app/Shared/Services/Leads/gold-loan-leads.service';
import { UserSettingService } from 'src/app/Shared/Services/user-setting-services/user-setting.service';
import { UserRoleEnum } from 'src/app/Shared/Enum/fixed-value';
import { AuthService } from 'src/app/Shared/Helper/auth.service';

@Component({
  selector: 'app-fresh-gold-loan-leads',
  templateUrl:'./fresh-gold-loan-leads.component.html',
  styleUrls: ['./fresh-gold-loan-leads.component.scss'],
  providers: [GoldLoanLeadsService, UserSettingService]
})
export class FreshGoldLoanLeadsComponent implements OnInit {

  model!: GoldLoanFreshLeadListModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  displayedColumns: string[] = ['index', 'FullName', 'FatherName', 'ProductName', 'Pincode','PrimaryMobileNumber', 'LoanAmountRequired', 'LeadStatus', 'LeadSourceByUserName', 'Action'];
  ViewdisplayedColumns = [{ Value: 'FullName', Text: 'Full Name' },
  { Value: 'PrimaryMobileNumber', Text: 'Mobile Number' },
  { Value: 'LeadSourceByUserName', Text: 'Lead Source By' },
  { Value: 'LoanAmountRequired', Text: 'Loan Amount' },
  { Value: 'FatherName', Text: 'Father Name' },
  { Value: 'ProductName', Text: 'Product' },
  { Value: 'Pincode', Text: 'Pincode' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  get routing_Url() { return Routing_Url };
  get userDetail() { return this._auth.GetUserDetail() };
  get userRoleEnum() { return UserRoleEnum };


  //#endregion

  constructor(private readonly _freshLeadService: GoldLoanLeadsService,
    private readonly _commonService: CommonService,
    private readonly toast: ToastrService,
    private readonly _auth: AuthService,
    private readonly _userSettingService: UserSettingService,
    public dialog: MatDialog) { }
  ngOnInit(): void {
    this.getList();

  }

  getList(): void {
    let serve = this._freshLeadService.GetList(this.indexModel).subscribe(response => {
      serve.unsubscribe();
      if (response.IsSuccess) {
        this.model = response.Data as GoldLoanFreshLeadListModel[];
        console.log(this.model);
        this.dataSource = new MatTableDataSource<GoldLoanFreshLeadListModel>(this.model);
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
    this.indexModel.IsPostBack = false;

    this.getList();
  }
  onPaginateChange(event: any) {
    this.indexModel.Page = event.pageIndex + 1;
    this.indexModel.PageSize = event.pageSize;
    this.indexModel.IsPostBack = true;
    this.getList();
  }
  onChangeLeadStatus(Id: number) {
    const dialogRef = this.dialog.open(LeadStatusPopupComponent, {
      data: { Id: Id as number, Type: "FreshGold" as string },
      width: '500px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.toast.success(Message.SaveSuccess as string, 'Success');
        this.getList();
      } else {
        this.toast.error(Message.SaveFail as string, 'Error');
      }
    });
  }
  onPageSizeChange() {
    this.indexModel.IsPostBack = true;
    this.indexModel.PageSize = Number(this.indexModel.PageSize);
    this.getList();
  }
  OnActiveStatus(Id: number) {

    this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {

      if (isTrue) {
        let serv = this._freshLeadService.ChangeActiveStatus(Id).subscribe(
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
        let serv = this._freshLeadService.Delete(id).subscribe(
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
            this.toast.error(error.message as string, 'Error');
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
  onChangeLeadStatusHistory(Id: number) {
    const dialogRef = this.dialog.open(LeadStatuslHistoryPopupComponent, {
      data: { Id: Id as number, Type: "GoldLoanLeadHistory" as string ,Heading:'Fresh Gold Loan Lead History' },
      width: '600px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
      } else {
      }
    });
  }
}
