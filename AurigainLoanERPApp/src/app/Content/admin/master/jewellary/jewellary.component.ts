import { JewellaryTypeModel } from './../../../../Shared/Model/master-model/jewellary-type-model.model';
import { JewelleryTypeService } from './../../../../Shared/Services/master-services/jewellery-type.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Message, Routing_Url } from 'src/app/Shared/Helper/constants';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { ToastrService } from 'ngx-toastr';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { IndexModel } from 'src/app/Shared/Helper/common-model';

@Component({
  selector: 'app-jewellary',
  templateUrl: './jewellary.component.html',
  styleUrls: ['./jewellary.component.scss'],
  providers: [JewelleryTypeService]
})
export class JewellaryComponent implements OnInit {

  get routing_Url() { return Routing_Url }

  model!: JewellaryTypeModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'Name', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  //#endregion

  constructor(private readonly _jewelleryType: JewelleryTypeService,
             private readonly _commonService: CommonService,
             private readonly toast: ToastrService) { }
  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    let subscription = this._jewelleryType.GetJewelleryTypeList(this.indexModel).subscribe(response => {
       subscription.unsubscribe();
      if (response.IsSuccess) {
        this.model = response.Data as JewellaryTypeModel[];
        this.dataSource = new MatTableDataSource<JewellaryTypeModel>(this.model);
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
        this._jewelleryType.ChangeActiveStatus(Id).subscribe(
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
        this._jewelleryType.DeleteJewelleryType(id).subscribe(
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
