import { ToastrService } from 'ngx-toastr';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { IndexModel } from 'src/app/Shared/Helper/common-model';
import { Message, Routing_Url } from 'src/app/Shared/Helper/constants';
import { DocumentTypeModel } from 'src/app/Shared/Model/master-model/document-type.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { KycDocumentTypeService } from 'src/app/Shared/Services/master-services/kyc-document-type.service';
import { MatTableDataSource } from '@angular/material/table';
@Component({
  selector: 'app-kyc-document-type',
  templateUrl: './kyc-document-type.component.html',
  styleUrls: ['./kyc-document-type.component.scss'],
  providers: [KycDocumentTypeService],
})
export class KycDocumentTypeComponent implements OnInit {
  get routing_Url() {
    return Routing_Url;
  }

  model!: DocumentTypeModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'DocumentName','DocumentNumberLength','IsNumeric' ,'IsActive', 'Action'];
  ViewdisplayedColumns = [
    { Value: 'DocumentName', Text: 'Name' },
    { Value: 'DocumentNumberLength', Text: 'Length' },
  ];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  constructor(
    private readonly _documentTypeService: KycDocumentTypeService,
    private readonly _commonService: CommonService,
    private readonly toast: ToastrService
  ) {}
  ngOnInit(): void {
    this.getList();
  }
  getList(): void {
    let subscription = this._documentTypeService
      .GetDocumentTypeList(this.indexModel)
      .subscribe(
        (response) => {
          subscription.unsubscribe();
          if (response.IsSuccess) {
            this.model = response.Data as DocumentTypeModel[];
            this.dataSource = new MatTableDataSource<DocumentTypeModel>(
              this.model
            );
            this.totalRecords = response.TotalRecord as number;
            if (!this.indexModel.IsPostBack) {
              this.dataSource.paginator = this.paginator;
              this.dataSource.sort = this.sort;
            }
          } else {
            this.toast.error(response.Message?.toString(), 'Server Error');
          }
        },
        (error) => {}
      );
  }
  sortData(event: any): void {
    this.indexModel.OrderBy = event.active;
    this.indexModel.OrderByAsc = event.direction == 'asc' ? true : false;
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
    this._commonService
      .Question(Message.ConfirmUpdate as string)
      .then((isTrue) => {
        if (isTrue) {
          let subscription = this._documentTypeService
            .ChangeActiveStatus(Id)
            .subscribe(
              (data) => {
                subscription.unsubscribe();
                if (data.IsSuccess) {
                  this.toast.success(data.Message as string, 'Status Change');
                  this.getList();
                } else {
                  this.toast.error(data.Message as string, 'Server Error');
                }
              },
              (error) => {
                this._commonService.Error(error.message as string);
              }
            );
        }
      });
  }

  updateDeleteStatus(id: number) {
    this._commonService
      .Question(Message.ConfirmUpdate as string)
      .then((result) => {
        if (result) {
          this._documentTypeService.DeleteDocumentType(id).subscribe(
            (data) => {
              if (data.IsSuccess) {
                this.getList();
                this.toast.success(data.Message as string, 'Success');
              } else {
                this.toast.error(data.Message as string, 'Server Error');
              }
            },
            (error) => {
              this.toast.error(error.message as string, 'Error');
            }
          );
        }
      });
  }
}
