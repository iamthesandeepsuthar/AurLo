import { Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { ToastrService } from "ngx-toastr";
import { IndexModel } from "src/app/Shared/Helper/common-model";
import { Routing_Url, Message } from "src/app/Shared/Helper/constants";
import { ProductModel } from "src/app/Shared/Model/master-model/product-model.model";
import { CommonService } from "src/app/Shared/Services/common.service";
import { ProductService } from "src/app/Shared/Services/master-services/product.service";


@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
  providers: [ProductService]
})
export class ProductsComponent implements OnInit {
  get routing_Url() { return Routing_Url }

  model!: ProductModel[];
  dataSource: any;
  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;
  id!: number;
  displayedColumns: string[] = ['index', 'Name', 'ProductCategoryName', 'BankName', 'IsActive', 'Action'];
  ViewdisplayedColumns = [{ Value: 'Name', Text: 'Product Name' },
                          {Value: 'ProductCategoryName', Text:'Product Category'},
                          {Value:'BankName',Text:'Bank Name'}];
  indexModel = new IndexModel();
  totalRecords: number = 0;
  constructor(private readonly _productService: ProductService,
    private readonly _commonService: CommonService,
    private readonly toast: ToastrService) { }

    ngOnInit(): void {
      this.getList();
    }
  getList(): void {
    let subscription = this._productService.GetProductList(this.indexModel).subscribe(response => {
       subscription.unsubscribe();
      if (response.IsSuccess) {
        this.model = response.Data as ProductModel[];
        this.dataSource = new MatTableDataSource<ProductModel>(this.model);
        this.totalRecords = response.TotalRecord as number;
        if (!this.indexModel.IsPostBack) {
          this.dataSource.paginator = this.paginator;
          this.dataSource.sort = this.sort;
        }
      } else {        // Toast message if  return false ;
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
        this._productService.ChangeActiveStatus(Id).subscribe(
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
        this._productService.DeleteProduct(id).subscribe(
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
