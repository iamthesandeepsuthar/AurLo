import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GoldLoanFreshLeadModel } from 'src/app/Shared/Model/Leads/gold-loan-fresh-lead.model';

@Component({
  selector: 'app-add-edit-gold-loan-fresh-lead',
  templateUrl: './add-edit-gold-loan-fresh-lead.component.html',
  styleUrls: ['./add-edit-gold-loan-fresh-lead.component.scss']
})
export class AddEditGoldLoanFreshLeadComponent implements OnInit {
  model = {} as GoldLoanFreshLeadModel;
  registrationFromStepOne!: FormGroup;
  registrationFromStepTwo!: FormGroup;
  registrationFromStepThird!: FormGroup;
  constructor(private readonly fb: FormBuilder) { }

  ngOnInit(): void {
  }

  formInit() {
    this.registrationFromStepOne = this.fb.group({
      fullName: [undefined, Validators.required],
      fatherName: [undefined, Validators.required],
      email: [undefined, Validators.required],
      mobile: [undefined, Validators.required],
      gender: [undefined, undefined],
      dob: [undefined,undefined],

    });
    this.registrationFromStepTwo = this.fb.group({
      documentType: [undefined],
      documentNumber: [undefined],
    });
    this.registrationFromStepThird = this.fb.group({
      pincode: [undefined],
      address: [undefined],
      area: [undefined, Validators.required],
    });



  }

}
