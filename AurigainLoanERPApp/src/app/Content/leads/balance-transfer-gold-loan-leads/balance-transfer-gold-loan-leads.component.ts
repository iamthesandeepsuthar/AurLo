import { LeadApprovalStatusEnum, LeadStatusEnum } from './../../../Shared/Enum/fixed-value';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { UserRoleEnum } from 'src/app/Shared/Enum/fixed-value';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Routing_Url, Message } from 'src/app/Shared/Helper/constants';
import { LeadApprovalHistoryPopupComponent } from 'src/app/Shared/Helper/shared/Popup/lead-approval-history-popup/lead-approval-history-popup.component';
import { LeadStatusPopupComponent } from 'src/app/Shared/Helper/shared/Popup/lead-status-popup/lead-status-popup.component';
import { LeadStatuslHistoryPopupComponent } from 'src/app/Shared/Helper/shared/Popup/lead-statusl-history-popup/lead-statusl-history-popup.component';
import { BTGoldLoanLeadListModel } from 'src/app/Shared/Model/Leads/btgold-loan-lead-post-model.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { BalanceTransferGoldLoanLeadsService } from 'src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service';
import { UserSettingService } from 'src/app/Shared/Services/user-setting-services/user-setting.service';
import { AuthService } from '../../../Shared/Helper/auth.service';
import { LeadApprovalPopupComponent } from '../../../Shared/Helper/shared/Popup/lead-approval-popup/lead-approval-popup.component';
import { AppointmentBookingPopupComponent } from 'src/app/Shared/Helper/shared/Popup/appointment-booking-popup/appointment-booking-popup.component';

@Component({
  selector: 'app-balance-transfer-gold-loan-leads',
  templateUrl: './balance-transfer-gold-loan-leads.component.html',
  styleUrls: ['./balance-transfer-gold-loan-leads.component.scss'],
  providers: [UserSettingService, BalanceTransferGoldLoanLeadsService]
})
export class BalanceTransferGoldLoanLeadsComponent implements OnInit {


  model!: BTGoldLoanLeadListModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  get UserDetail() { return this._auth.GetUserDetail() };
  get UserRoleEnum() { return UserRoleEnum };
  get LeadStatusEnum() { return LeadStatusEnum};
  get ApprovalStatusEnum() {return LeadApprovalStatusEnum}
  get routing_Url() { return Routing_Url };

  displayedColumns: string[] = ['index','LoanCaseNumber', 'FullName', 'FatherName','IsInternalLead', 'PrimaryMobileNumber','ProductName', 'Pincode', 'LoanAmountRequired','LeadSourceByUserName', 'LeadStatus','ApprovalStatus', 'Action'];
  ViewdisplayedColumns = [{ Value: 'FullName', Text: 'Full Name' },
  {Value:'LoanCaseNumber',Text:'Loan Case'},
  { Value: 'PrimaryMobileNumber', Text: 'Mobile Number' },
  { Value: 'LeadSourceByUserName', Text: 'Lead Source By' },
  { Value: 'LoanAmountRequired', Text: 'Loan Amount' },
   { Value: 'FatherName', Text: 'Father Name' },
  { Value: 'ProductName', Text: 'Product' },
  { Value: 'Pincode', Text: 'Pincode' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;

  //#endregion

  constructor(private readonly _freshLeadService: BalanceTransferGoldLoanLeadsService,
    private readonly _commonService: CommonService,
    private readonly toast: ToastrService,
    private readonly _userSettingService: UserSettingService,
    private readonly _auth: AuthService,
    public dialog: MatDialog) {
    this.indexModel.UserId = this.UserDetail?.UserId as number

  }

  ngOnInit(): void {
    this.getList();
  }
  getList(): void {
    let serve = this._freshLeadService.GetList(this.indexModel).subscribe(response => {
      serve.unsubscribe();
      if (response.IsSuccess) {
        this.model = response.Data as BTGoldLoanLeadListModel[];
        this.dataSource = new MatTableDataSource<BTGoldLoanLeadListModel>(this.model);
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
  onChangeLeadApproveStage(Id: number) {
    const dialogRef = this.dialog.open(LeadApprovalPopupComponent, {
      data: { Id: Id as number, Type: "BTTRANSFER" as string },
      width: '500px',

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.toast.success(Message.SaveSuccess as string, 'Success');
        this.getList();
      } else {
     // this.toast.error(Message.SaveFail as string, 'Error');

      }
    });
  }
  onChangeLeadStatus(Id: number) {
    const dialogRef = this.dialog.open(LeadStatusPopupComponent, {
      data: { Id: Id as number, Type: "BTLEAD" as string ,Heading:'BT Gold Loan Lead Status Change' },
      width: '500px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // this.toast.success(Message.SaveSuccess as string, 'Success');
        this.getList();
      } else {
      //  this.toast.error(Message.SaveFail as string, 'Error');
      }
    });
  }
  onOpenApproveHistory(Id: number) {
    const dialogRef = this.dialog.open(LeadApprovalHistoryPopupComponent, {
      data: { Id: Id as number, Type: "BTLEAD" as string ,Heading:'BT Gold Loan Approval History'},
      width: '600px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {

      }
    });
  }
  onOpenStatusHistory(Id: number) {
    const dialogRef = this.dialog.open(LeadStatuslHistoryPopupComponent, {
      data: { Id: Id as number, Type: "BTLEAD" as string , Heading:"BT Gold Loan Lead History"},
      width: '600px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {

      }
    });
  }
  onChangeAppointmentBooking(Id: number) {
    const dialogRef = this.dialog.open(AppointmentBookingPopupComponent, {
      data: { Id: Id as number, Type: "BTAppointmentBooking" as string ,Heading:'Appointment Booking' },
      width: '1000px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
      } else {
      }
    });
  }
  // updateDeleteStatus(id: number) {

  //   this._commonService.Question(Message.ConfirmUpdate as string).then(result => {
  //     if (result) {
  //       let serv = this._freshLeadService.Delete(id).subscribe(
  //         data => {
  //           serv.unsubscribe();
  //           if (data.IsSuccess) {
  //             this.toast.success(data.Message as string, 'Removed');
  //             this.getList();
  //           } else {
  //             this.toast.warning(data.Message as string, 'Server Error');
  //           }
  //         },
  //         error => {
  //           this.toast.error(error.message as string, 'Error');
  //         }
  //       );
  //     }
  //   });
  // }
  // OnApproveStatus(Id: number) {
  //   this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {

  //     if (isTrue) {
  //       let serv = this._userSettingService.UpdateApproveStatus(Id).subscribe(
  //         data => {
  //           serv.unsubscribe();
  //           if (data.IsSuccess) {
  //             this.toast.success(data.Message as string, 'Access Permission');
  //             this.getList();
  //           } else {
  //             this.toast.warning(data.Message as string, 'Server Error');
  //           }
  //         },
  //         error => {
  //           this.toast.error(error.Message as string, 'Error');
  //         }
  //       );
  //     }
  //   });

  // }
}
