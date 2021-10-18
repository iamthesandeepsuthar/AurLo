
import { PaymentModeService } from './../../../../Shared/Services/master-services/payment-mode.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Message, Routing_Url } from 'src/app/Shared/Helper/constants';
import { PaymentModeModel } from 'src/app/Shared/Model/master-model/payment-mode.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-payment-mode',
  templateUrl: './payment-mode.component.html',
  styleUrls: ['./payment-mode.component.scss'],
  providers: [PaymentModeService]
})
export class PaymentModeComponent implements OnInit {
  get routing_Url() { return Routing_Url }

  model!: PaymentModeModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'Name', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  constructor(private readonly _paymentModeService: PaymentModeService,
    private readonly _commonService: CommonService,
    private readonly toast: ToastrService) { }
ngOnInit(): void {
this.getList();
}

getList(): void {
let subscription =  this._paymentModeService.GetPaymentModeList(this.indexModel).subscribe(response => {
subscription.unsubscribe();
if (response.IsSuccess) {
this.model = response.Data as PaymentModeModel[];
this.dataSource = new MatTableDataSource<PaymentModeModel>(this.model);
this.totalRecords = response.TotalRecord as number;
if (!this.indexModel.IsPostBack) {
 this.dataSource.paginator = this.paginator;
 this.dataSource.sort = this.sort;
}
} else {
// Toast message if  return false ;
this.toast.error(response.Message?.toString() , 'Error');
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

OnActiveStatus(Id: number) {
this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {
if (isTrue) {
let subscription =  this._paymentModeService.ChangeActiveStatus(Id).subscribe( data => {
   subscription.unsubscribe();
   if (data.IsSuccess) {
     this._commonService.Success(data.Message as string)
     this.getList();
   }
 },
 error => {
   this.toast.error(error.message as string , 'Error');
 }
);
}
});
}

updateDeleteStatus(id: number) {
this._commonService.Question(Message.ConfirmUpdate as string).then(result => {
if (result) {
this._paymentModeService.DeletePaymentMode(id).subscribe(
 data => {
   if (data.IsSuccess) {
     this.getList();
     this.toast.success(data.Message as string , 'Success');
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
