import { UserManagerModel } from 'src/app/Shared/Model/master-model/user-manager-model.model';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserManagerService } from 'src/app/Shared/Services/user-manager.service';
import { Routing_Url } from 'src/app/Shared/Helper/constants';

@Component({
  selector: 'app-detail-managers',
  templateUrl: './detail-managers.component.html',
  styleUrls: ['./detail-managers.component.scss'],
  providers:[UserManagerService]
})
export class DetailManagersComponent implements OnInit {
  Id: number = 0;
  model = new UserManagerModel();
  get routing_Url() { return Routing_Url }
  get Model(): UserManagerModel{  return this.model; }

  constructor(private readonly _managerService: UserManagerService,
              private _activatedRoute: ActivatedRoute ,
              private readonly toast: ToastrService) {
    if (this._activatedRoute.snapshot.params.id) {
      this.Id = this._activatedRoute.snapshot.params.id;
      this.onGetDetail();
    }
  }
  ngOnInit(): void {
    this.onGetDetail();
  }
  onGetDetail() {
    let subscription = this._managerService.GetManagerById(this.Id).subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
      this.model = res.Data as UserManagerModel;
      } else {
    this.toast.warning('Record not found', 'No Record');
      }
    });
  }

}
