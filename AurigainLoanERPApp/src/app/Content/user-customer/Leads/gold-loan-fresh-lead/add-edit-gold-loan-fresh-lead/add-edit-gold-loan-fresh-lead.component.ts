import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GoldLoanFreshLeadModel } from 'src/app/Shared/Model/Leads/gold-loan-fresh-lead.model';

@Component({
  selector: 'app-add-edit-gold-loan-fresh-lead',
  templateUrl: './add-edit-gold-loan-fresh-lead.component.html',
  styleUrls: ['./add-edit-gold-loan-fresh-lead.component.scss']
})
export class AddEditGoldLoanFreshLeadComponent implements OnInit {
  model = new GoldLoanFreshLeadModel();
  
  leadFromPersonalDetail!: FormGroup;
  leadFromDocumentDetail!: FormGroup;
  leadFromJewelleryDetail!: FormGroup;
  leadFromAppointmentDetail!: FormGroup;

  constructor(private readonly fb: FormBuilder) {
  }

  ngOnInit(): void {
  }

  formInit() {
    this.leadFromPersonalDetail = this.fb.group({
      Product: [undefined, Validators.required],
      Email: [undefined, Validators.required],
      FullName: [undefined, Validators.required],
      FatherName: [undefined, Validators.required],
      DOB: [undefined, Validators.required],
      Gender: [undefined, undefined],
      Mobile: [undefined, Validators.required],
      AlternativeMobile: [undefined, Validators.required],
      Amount: [undefined, Validators.required],
      Purpose: [undefined, Validators.required]
    });

    this.leadFromDocumentDetail = this.fb.group({
      DocumentType: [undefined],
      DocumentNumber: [undefined],
      Pincode: [undefined],
      Aarea: [undefined, Validators.required],
    });

    this.leadFromJewelleryDetail = this.fb.group({
      JewelleryType: [undefined, Validators.required],
      Quantity: [undefined, Validators.required],
      Weight: [undefined, Validators.required],
      Karats: [undefined, Validators.required],
      Tenure: [undefined, Validators.required]
    });

    this.leadFromAppointmentDetail = this.fb.group({
      Branch: [undefined, Validators.required],
      DateofAppointment: [undefined, Validators.required],
      TimeofAppointment: [undefined, Validators.required],
    });



  }
  onSubmit() {
    this.leadFromPersonalDetail.markAllAsTouched();
    this.leadFromDocumentDetail.markAllAsTouched();
    this.leadFromJewelleryDetail.markAllAsTouched();
    this.leadFromAppointmentDetail.markAllAsTouched();
  }

}
