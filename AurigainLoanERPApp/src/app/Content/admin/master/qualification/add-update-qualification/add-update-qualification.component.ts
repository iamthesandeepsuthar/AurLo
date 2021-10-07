import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Routing_Url } from 'src/app/Shared/Helper/constants';
import { QualificationModel } from 'src/app/Shared/Model/master-model/qualification.model';
import { QualificationService } from 'src/app/Shared/Services/master-services/qualification.service';

@Component({
  selector: 'app-add-update-qualification',
  templateUrl: './add-update-qualification.component.html',
  styleUrls: ['./add-update-qualification.component.scss']
})
export class AddUpdateQualificationComponent implements OnInit {
  Id: number = 0;
  model = new QualificationModel();
  get routing_Url() { return Routing_Url }
  constructor(private readonly fb: FormBuilder, private readonly _qualificationService: QualificationService,
    private _activatedRoute: ActivatedRoute, private _router: Router) {

     }

  ngOnInit(): void {
  }

}
