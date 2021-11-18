import { BalanceTransferGoldLoanLeadsService } from './../../../Shared/Services/Leads/balance-transfer-gold-loan-leads.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { ToastrService } from 'ngx-toastr';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Message, Routing_Url } from 'src/app/Shared/Helper/constants';
import { BTGoldLoanLeadListModel } from 'src/app/Shared/Model/Leads/btgold-loan-lead-post-model.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { UserSettingService } from 'src/app/Shared/Services/user-setting-services/user-setting.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-balance-transfer-gold-loan-leads',
  templateUrl: './balance-transfer-gold-loan-leads.component.html',
  styleUrls: ['./balance-transfer-gold-loan-leads.component.scss'],
  providers: [UserSettingService , BalanceTransferGoldLoanLeadsService]
})
export class BalanceTransferGoldLoanLeadsComponent implements OnInit {


  model!: BTGoldLoanLeadListModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  displayedColumns: string[] = ['index', 'FullName','FatherName','PrimaryMobileNumber','LoanAmountRequired', 'LeadSourceByUserName','ProductName','Pincode', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'FullName', Text: 'Full Name' },
  { Value: 'PrimaryMobileNumber', Text: 'Mobile Number' },
  { Value: 'LeadSourceByUserName', Text: 'Lead Source By' },
  { Value: 'LoanAmountRequired', Text: 'Loan Amount' },
  { Value: 'FatherName', Text: 'Father Name' },
  { Value: 'ProductName', Text: 'Product' },
  { Value:'Pincode', Text: 'Pincode'}];
   indexModel = new IndexModel();
  totalRecords: number = 0;
  get routing_Url() { return Routing_Url };


  //#endregion

  constructor(private readonly _freshLeadService: BalanceTransferGoldLoanLeadsService,
    private readonly _commonService: CommonService,
    private readonly toast: ToastrService,
    private readonly _userSettingService: UserSettingService) { }
  ngOnInit(): void {
    this.getList();

  }

  getList(): void {
    let serve = this._freshLeadService.GetList(this.indexModel).subscribe(response => {
      serve.unsubscribe();
      if (response.IsSuccess) {
        this.model = response.Data as BTGoldLoanLeadListModel[];
        console.log(this.model);
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