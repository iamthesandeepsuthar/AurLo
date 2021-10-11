import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { IndexModel } from "src/app/Shared/Helper/common-model";
import { Routing_Url, Message } from "src/app/Shared/Helper/constants";
import { QualificationModel } from "src/app/Shared/Model/master-model/qualification.model";
import { CommonService } from "src/app/Shared/Services/common.service";
import { QualificationService } from "src/app/Shared/Services/master-services/qualification.service";

@Component({
  selector: 'app-qualification',
  templateUrl: './qualification.component.html',
  styleUrls: ['./qualification.component.scss'],
  providers: [QualificationService]
})
export class QualificationComponent implements OnInit {


  //#region <<  Variable  >>
  get routing_Url() { return Routing_Url }

  model!: QualificationModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'Name', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Name' }];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  //#endregion

  constructor(private readonly _serviceQualification: QualificationService, private readonly _commonService: CommonService) { }
  ngOnInit(): void {
    this.getList();
  }

  getList(): void {
    this._serviceQualification.GetQualificationList(this.indexModel).subscribe(response => {

      if (response.IsSuccess) {
        this.model = response.Data as QualificationModel[];
        this.dataSource = new MatTableDataSource<QualificationModel>(this.model);
        this.totalRecords = response.TotalRecord as number;
        if (!this.indexModel.IsPostBack) {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      } else {
        // Toast message if  return false ;
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
        this._serviceQualification.ChangeActiveStatus(Id).subscribe(
          data => {
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
        this._serviceQualification.DeleteQualification(id).subscribe(
          data => {
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
  //#endregion

}
