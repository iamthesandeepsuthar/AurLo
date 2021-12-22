import { MatSort } from '@angular/material/sort';
import { PurposeModel } from 'src/app/Shared/Model/master-model/purpose-model.model';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Message, Routing_Url } from 'src/app/Shared/Helper/constants';
import { PurposeService } from 'src/app/Shared/Services/master-services/purpose.service';
import { MatPaginator } from '@angular/material/paginator';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-purpose-list',
  templateUrl: './purpose-list.component.html',
  styles: [],
  providers:[PurposeService]
})
export class PurposeListComponent implements OnInit {

   //#region <<  Variable  >>
   get routing_Url() { return Routing_Url }

   model!: PurposeModel[];
   dataSource: any;
   @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
   @ViewChild(MatSort, { static: true }) sort!: MatSort;
   id!: number;
   displayedColumns: string[] = ['index', 'Name', 'IsActive', 'Action'];
   ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' }];
   indexModel = new IndexModel();
   totalRecords: number = 0;
   //#endregion

   constructor(private readonly _purposeService: PurposeService,
              private readonly _commonService: CommonService,
              private readonly toast: ToastrService) { }
   ngOnInit(): void {
     this.getList();
   }

   getList(): void {
     let subscription = this._purposeService.GetPurposeList(this.indexModel).subscribe(response => {
        subscription.unsubscribe();
       if (response.IsSuccess) {
         this.model = response.Data as PurposeModel[];
         this.dataSource = new MatTableDataSource<PurposeModel>(this.model);
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

   OnActiveStatus(Id: number) {

     this._commonService.Question(Message.ConfirmUpdate as string).then(isTrue => {

       if (isTrue) {
         this._purposeService.ChangeActiveStatus(Id).subscribe(
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
         this._purposeService.DeletePurpose(id).subscribe(
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
   //#endregion

}
