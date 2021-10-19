import { ToastrService } from 'ngx-toastr';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Message, Routing_Url } from 'src/app/Shared/Helper/constants';
import { StateModel } from 'src/app/Shared/Model/master-model/state.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-state',
  templateUrl: './state.component.html',
  styleUrls: ['./state.component.scss'],
  providers: [StateDistrictService]
})
export class StateComponent implements OnInit {
  get routing_Url() { return Routing_Url }

  model!: StateModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'Name', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  constructor(private readonly _stateService: StateDistrictService,
              private readonly _commonService: CommonService,
              private readonly toast: ToastrService) { }
  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
   let subscription =  this._stateService.GetStateList(this.indexModel).subscribe(response => {
      subscription.unsubscribe();
      if (response.IsSuccess) {
        this.model = response.Data as StateModel[];
        this.dataSource = new MatTableDataSource<StateModel>(this.model);
        this.totalRecords = response.TotalRecord as number;
        if (!this.indexModel.IsPostBack) {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      } else {
        this.toast.warning(response.Message?.toString(), 'Server Error');
      }
    },
      error => {
        this.toast.error(error.Message , 'Error');
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
       let subscription  =  this._stateService.ChangeActiveStatus(Id).subscribe(
          data => {
            subscription.unsubscribe();
            if (data.IsSuccess) {
              this.toast.success(data.Message as string,'Status Change');
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
       let subscription =  this._stateService.DeleteState(id).subscribe(
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
            this._commonService.Error(error.message as string)

          }
        );
      }
    });
  }
}
