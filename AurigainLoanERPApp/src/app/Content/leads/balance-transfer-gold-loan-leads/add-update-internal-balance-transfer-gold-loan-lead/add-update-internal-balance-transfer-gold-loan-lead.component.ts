import { PurposeService } from 'src/app/Shared/Services/master-services/purpose.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserRoleEnum, ProductCategoryEnum } from 'src/app/Shared/Enum/fixed-value';
import { AuthService } from 'src/app/Shared/Helper/auth.service';
import { DropDownModel } from 'src/app/Shared/Helper/common-model';
import { DropDown_key, Message, Routing_Url } from 'src/app/Shared/Helper/constants';
import { BTGoldLoanDeletePostModel, BtGoldLoanLeadJewelleryDetailPostModel, BTGoldLoanLeadPostModel, BTGoldLoanLeadViewModel } from 'src/app/Shared/Model/Leads/btgold-loan-lead-post-model.model';
import { DDLBranchModel } from 'src/app/Shared/Model/master-model/bank-model.model';
import { DDLDocumentTypeModel } from 'src/app/Shared/Model/master-model/document-type.model';
import { DDLJewellaryType } from 'src/app/Shared/Model/master-model/jewellary-type-model.model';
import { DDLProductModel } from 'src/app/Shared/Model/master-model/product-model.model';
import { AvailableAreaModel } from 'src/app/Shared/Model/User-setting-model/user-availibility.model';
import { CommonService } from 'src/app/Shared/Services/common.service';
import { BalanceTransferGoldLoanLeadsService } from 'src/app/Shared/Services/Leads/balance-transfer-gold-loan-leads.service';
import { BankBranchService } from 'src/app/Shared/Services/master-services/bank-branch.service';
import { JewelleryTypeService } from 'src/app/Shared/Services/master-services/jewellery-type.service';
import { KycDocumentTypeService } from 'src/app/Shared/Services/master-services/kyc-document-type.service';
import { ProductService } from 'src/app/Shared/Services/master-services/product.service';
import { StateDistrictService } from 'src/app/Shared/Services/master-services/state-district.service';
import { ddlPurposeModel } from 'src/app/Shared/Model/master-model/purpose-model.model';
import { GoldLoanFreshLeadJewelleryDetailModel } from 'src/app/Shared/Model/Leads/gold-loan-fresh-lead.model';
import { FileInfo } from 'src/app/Content/Common/file-selector/file-selector.component';
import { DocumentTypeEnum } from '../../../../Shared/Enum/fixed-value';
import { FilePostModel } from '../../../../Shared/Model/doorstep-agent-model/door-step-agent.model';
import { FileModel } from '../../../../Shared/Model/Leads/btgold-loan-lead-post-model.model';

@Component({
  selector: 'app-add-update-internal-balance-transfer-gold-loan-lead',
  templateUrl: './add-update-internal-balance-transfer-gold-loan-lead.component.html',
  styleUrls: ['./add-update-internal-balance-transfer-gold-loan-lead.component.scss'],
  providers: [ProductService, KycDocumentTypeService,
    StateDistrictService, BankBranchService,
    JewelleryTypeService,
    BalanceTransferGoldLoanLeadsService, PurposeService]

})
export class AddUpdateInternalBalanceTransferGoldLoanLeadComponent implements OnInit {


  leadId: number = 0;
  model = new BTGoldLoanLeadPostModel();
  JewelleryModel = new BtGoldLoanLeadJewelleryDetailPostModel();

  AeraPincode!: string | any;
  CorrespondAeraPincode!: string | any;
  BankId!: number;
  leadFormPersonalDetail!: FormGroup;
  leadFormAddressDetail!: FormGroup;
  leadFormAppointmentDetail!: FormGroup;
  leadFormJewelleryDetail!: FormGroup;
  leadFormDocumentDetail!: FormGroup;
  leadFormExistingLoanDetail!: FormGroup;
  leadFormKYCDetail!: FormGroup;

  dropDown = new DropDownModel();
  get ddlkeys() { return DropDown_key };
  ddlProductModel: DDLProductModel[] = [];
  ddlBranchModel: DDLBranchModel[] = [];
  ddlAreaModel: AvailableAreaModel[] = [];
  ddlCorespondAreaModel: AvailableAreaModel[] = [];
  ddlDocumentType: DDLDocumentTypeModel[] = [];
  ddlDocumentTypePOI: DDLDocumentTypeModel[] = [];
  ddlDocumentTypePOA: DDLDocumentTypeModel[] = [];
  ddlPurpose: ddlPurposeModel[] = [];
  isSameAddress = false;
  ddlJewellaryType: DDLJewellaryType[] = [];
  ddlKarats = [{ Name: "18 Karats", Id: 18 }, { Name: "20  Karats", Id: 20 }, { Name: "22  Karats", Id: 22 }, { Name: "24  Karats", Id: 24 }];
  docPOIMaxChar: number = 0;
  docPOAMaxChar: number = 0;

  get f1() { return this.leadFormPersonalDetail.controls; }
  get f2() { return this.leadFormAddressDetail.controls; }
  get f3() { return this.leadFormAppointmentDetail.controls; }
  get f4() { return this.leadFormJewelleryDetail.controls; }
  get f5() { return this.leadFormDocumentDetail.controls; }
  get f6() { return this.leadFormExistingLoanDetail.controls; }
  get f7() { return this.leadFormKYCDetail.controls; }
  get userDetail() { return this._auth.GetUserDetail() };
  get DobMaxDate() {
    var date = new Date();
    date.setFullYear(date.getFullYear() - 18);
    return date
  };
  get POAMaxFile() {
    return this.model?.KYCDetail?.PoadocumentTypeId == DocumentTypeEnum.AadhaarCard ? 2 : 1;

  }
  get POIMaxFile() {
    return this.model?.KYCDetail?.PoidocumentTypeId == DocumentTypeEnum.AadhaarCard ? 2 : 1;

  }
  constructor(private readonly fb: FormBuilder, readonly _commonService: CommonService,
    private readonly _productService: ProductService,
    private readonly _stateDistrictService: StateDistrictService,
    readonly _router: Router, private readonly _bankBranchService: BankBranchService,
    private readonly _activatedRoute: ActivatedRoute, private readonly _auth: AuthService,
    private readonly _balanceTransferService: BalanceTransferGoldLoanLeadsService,
    private readonly _jewelleryTypeService: JewelleryTypeService,
    private readonly _kycDocumentTypeService: KycDocumentTypeService,
    private readonly toast: ToastrService,
    private readonly _purposeService: PurposeService) {
  }

  ngOnInit(): void {
    this.formInit();
    this.GetDropDowns();

    if (this._activatedRoute.snapshot?.params?.id) {
      this.leadId = this._activatedRoute.snapshot.params.id;
      this.onGetDetail();
    }
  }

  formInit() {
    this.leadFormPersonalDetail = this.fb.group({
      Product: [undefined, Validators.required],
      Email: [undefined, Validators.compose([Validators.required, Validators.email])],
      FullName: [undefined, Validators.required],
      FatherName: [undefined, Validators.required],
      DOB: [undefined, Validators.required],
      Gender: [undefined, Validators.required],
      Mobile: [undefined, Validators.required],
      AlternativeMobile: [undefined, Validators.required],
      Amount: [undefined, Validators.required],
      AccountNumber: [undefined, Validators.required],
      Purpose: [undefined, Validators.required]
    });

    this.leadFormAddressDetail = this.fb.group({

      Pincode: [undefined, Validators.compose([Validators.minLength(6), Validators.maxLength(6)])],
      Area: [undefined],
      Address: [undefined],
      AddressLine2: [undefined],
      CorrespondPincode: [undefined, Validators.compose([Validators.minLength(6), Validators.maxLength(6)])],
      CorrespondArea: [undefined],
      CorrespondAddress: [undefined],
      CorrespondAddressLine2: [undefined],

    });

    this.leadFormJewelleryDetail = this.fb.group({
      JewelleryType: [undefined, undefined],
      Quantity: [undefined, undefined],
      Weight: [undefined, undefined],
      Karats: [undefined, undefined],

    });


    this.leadFormAppointmentDetail = this.fb.group({
      Bank: [undefined, undefined],
      Branch: [undefined, undefined],
      DateofAppointment: [undefined, undefined],
      TimeofAppointment: [undefined, undefined],
    });


    this.leadFormKYCDetail = this.fb.group({
      PoidocumentType: [undefined],
      PoidocumentNumber: [undefined],
      PoadocumentType: [undefined],
      PoadocumentNumber: [undefined],
      PanNumber: [undefined, Validators.compose([Validators.minLength(10), Validators.maxLength(10)])],

    });


    this.leadFormDocumentDetail = this.fb.group({
      POIdocument: [undefined],
      POAdocument: [undefined],
      Cheque1: [undefined],
      Cheque2: [undefined],
      PromisaryNote: [undefined],
      LoanDocument: [undefined],
      ForeClouserLetter: [undefined],
      ATMWithdrawalSlip: [undefined],
      LastPageOfAgreement: [undefined],
      CustomerPhoto: [undefined]
    });


    this.leadFormExistingLoanDetail = this.fb.group({
      BankName: [undefined],
      Amount: [undefined],
      Date: [undefined],
      JewelleryValuation: [undefined],
      OutstandingAmount: [undefined],
      BalanceTransferAmount: [undefined],
      RequiredAmount: [undefined],
      Tenure: [undefined],
    });


    // document upload


  }
  onSubmit() {
    this.leadFormPersonalDetail.markAllAsTouched();
    this.leadFormAddressDetail.markAllAsTouched();
    this.leadFormAppointmentDetail.markAllAsTouched();
    this.leadFormJewelleryDetail.markAllAsTouched()
    this.leadFormKYCDetail.markAllAsTouched()
    this.leadFormDocumentDetail.markAllAsTouched();
    this.leadFormExistingLoanDetail.markAllAsTouched();

    this.model.LeadSourceByuserId = this._auth.GetUserDetail()?.UserId as number;

    if (this.leadFormPersonalDetail.valid && this.leadFormAddressDetail.valid && this.leadFormAppointmentDetail.valid
      && this.leadFormJewelleryDetail.valid && this.leadFormKYCDetail.valid && this.leadFormDocumentDetail.valid
      && this.leadFormExistingLoanDetail.valid) {

      if (this.userDetail?.RoleId == UserRoleEnum.Operator || this.userDetail?.RoleId == UserRoleEnum.Agent || this.userDetail?.RoleId == UserRoleEnum.DoorStepAgent) {
        this.model.LeadSourceByuserId = this.userDetail.UserId as number;

      }
      else if (this.userDetail?.RoleId == UserRoleEnum.Customer) {
        this.model.CustomerUserId = this.userDetail.UserId as number;

      }

      if (this.model.LoanAmount) {
        this.model.LoanAmount = Number(this.model.LoanAmount);

      } else {
        this.model.LoanAmount = 0;
      }

      if (this.model.ExistingLoanDetail.Amount) {
        this.model.ExistingLoanDetail.Amount = Number(this.model.ExistingLoanDetail.Amount);

      } else {
        this.model.ExistingLoanDetail.Amount = 0;
      }

      if (this.model.ExistingLoanDetail.JewelleryValuation) {
        this.model.ExistingLoanDetail.JewelleryValuation = Number(this.model.ExistingLoanDetail.JewelleryValuation);
      }
      if (this.model.ExistingLoanDetail.OutstandingAmount) {
        this.model.ExistingLoanDetail.OutstandingAmount = Number(this.model.ExistingLoanDetail.OutstandingAmount);
      }
      if (this.model.ExistingLoanDetail.BalanceTransferAmount) {
        this.model.ExistingLoanDetail.BalanceTransferAmount = Number(this.model.ExistingLoanDetail.BalanceTransferAmount);
      }
      if (this.model.ExistingLoanDetail.RequiredAmount) {
        this.model.ExistingLoanDetail.RequiredAmount = Number(this.model.ExistingLoanDetail.RequiredAmount);
      }
      if (this.model.ExistingLoanDetail.Tenure) {
        this.model.ExistingLoanDetail.Tenure = Number(this.model.ExistingLoanDetail.Tenure);
      }

      this.model.JewelleryDetail.forEach(element => {
        if (element.Quantity) {
          element.Quantity = Number(element.Quantity);
        }

        if (element.Weight) {
          element.Weight = Number(element.Weight);
        }
      });


      this._balanceTransferService.AddUpdateInternalLead(this.model).subscribe(res => {
        if (res.IsSuccess) {
          this.toast.success(Message.SaveSuccess);

          this._router.navigate([`${Routing_Url.Lead_Module}/${Routing_Url.BT_GoldLoan_List_Url}`]);

        } else {
          this.toast.success(Message.SaveFail);

        }
      });
    }

  }
  onGetDetail() {
    if (this.leadId > 0) {
      let serve = this._balanceTransferService.GetById(this.leadId).subscribe(res => {
        serve.unsubscribe();
        if (res.IsSuccess) {
          let viewData = res.Data as BTGoldLoanLeadViewModel;
          this.model.Id = viewData?.Id;
          this.model.ProductId = viewData?.ProductId ?? undefined;
          this.model.FullName = viewData?.FullName ?? undefined;
          this.model.FatherName = viewData?.FatherName ?? undefined;
          this.model.Gender = viewData?.Gender ?? undefined;
          this.model.DateOfBirth = viewData?.DateOfBirth ?? undefined;
          this.model.Profession = viewData?.Profession ?? undefined;
          this.model.Mobile = viewData?.Mobile ?? undefined;
          this.model.EmailId = viewData?.EmailId ?? undefined;
          this.model.CustomerUserId = viewData?.CustomerUserId ?? undefined;
          this.model.SecondaryMobile = viewData?.SecondaryMobile ?? undefined;
          this.model.Purpose = viewData?.Purpose ?? undefined;
          this.model.LoanAmount = viewData?.LoanAmount ?? undefined;
          this.model.LoanAccountNumber = viewData?.LoanAccountNumber ?? undefined;
          this.model.LeadSourceByuserId = viewData?.LeadSourceByuserId ?? undefined;
          this.model.PurposeId = viewData?.PurposeId ?? undefined;

          if (viewData?.DetailAddress) {
            this.AeraPincode = viewData?.DetailAddress?.PinCode ?? undefined;
            this.getDropDownPinCodeArea(false, true);
            this.model.AddressDetail.Id = viewData?.DetailAddress?.Id ?? undefined;
            this.model.AddressDetail.Address = viewData?.DetailAddress?.Address ?? undefined;
            this.model.AddressDetail.AeraPincodeId = viewData?.DetailAddress?.AeraPincodeId ?? undefined;

            this.CorrespondAeraPincode = viewData?.DetailAddress?.CorrespondPinCode ?? undefined;
            this.getDropDownPinCodeArea(true, true);
            this.model.AddressDetail.CorrespondAddress = viewData?.DetailAddress?.CorrespondAddress ?? undefined;
            this.model.AddressDetail.CorrespondAeraPincodeId = viewData?.DetailAddress?.CorrespondAeraPincodeId ?? undefined;
          }
          if (viewData?.AppointmentDetail) {
            this.BankId = viewData?.AppointmentDetail?.BankId ?? undefined;
            this.getDropDownBranch();

            this.model.AppointmentDetail.Id = viewData?.AppointmentDetail?.Id ?? undefined;
            this.model.AppointmentDetail.BranchId = viewData?.AppointmentDetail?.BranchId ?? undefined;
            this.model.AppointmentDetail.AppointmentDate = viewData?.AppointmentDetail?.AppointmentDate ?? undefined;
            this.model.AppointmentDetail.AppointmentTime = viewData?.AppointmentDetail?.AppointmentTime ?? undefined;
          }

          if (viewData?.ExistingLoanDetail) {
            this.model.ExistingLoanDetail.Id = viewData?.ExistingLoanDetail?.Id ?? undefined;
            this.model.ExistingLoanDetail.BankName = viewData?.ExistingLoanDetail?.BankName ?? undefined;
            this.model.ExistingLoanDetail.Amount = viewData?.ExistingLoanDetail?.Amount ?? undefined;
            this.model.ExistingLoanDetail.Date = viewData?.ExistingLoanDetail?.Date ?? undefined;
            this.model.ExistingLoanDetail.JewelleryValuation = viewData?.ExistingLoanDetail?.JewelleryValuation ?? undefined;
            this.model.ExistingLoanDetail.OutstandingAmount = viewData?.ExistingLoanDetail?.OutstandingAmount ?? undefined;
            this.model.ExistingLoanDetail.BalanceTransferAmount = viewData?.ExistingLoanDetail?.BalanceTransferAmount ?? undefined;
            this.model.ExistingLoanDetail.RequiredAmount = viewData?.ExistingLoanDetail?.RequiredAmount ?? undefined;
            this.model.ExistingLoanDetail.Tenure = viewData?.ExistingLoanDetail?.Tenure ?? undefined;
          }
          if (viewData?.JewelleryDetail) {

            viewData?.JewelleryDetail.forEach(element => {
              let itm = new BtGoldLoanLeadJewelleryDetailPostModel();

              itm.Id = element?.Id ?? undefined;
              itm.JewelleryTypeId = element?.JewelleryTypeId ?? undefined;
              itm.Quantity = element?.Quantity ?? undefined;
              itm.Weight = element?.Weight ?? undefined;
              itm.Karats = element?.Karats ?? undefined;
              this.model.JewelleryDetail.push(itm)
            });

          }

          if (viewData?.KYCDetail) {
            this.model.KYCDetail.Id = viewData?.KYCDetail?.Id ?? undefined;
            this.model.KYCDetail.PoidocumentTypeId = viewData?.KYCDetail?.PoidocumentTypeId ?? undefined;
            this.model.KYCDetail.PoidocumentNumber = viewData?.KYCDetail?.PoidocumentNumber ?? undefined;
            this.model.KYCDetail.PoadocumentTypeId = viewData?.KYCDetail?.PoadocumentTypeId ?? undefined;
            this.model.KYCDetail.PoadocumentNumber = viewData?.KYCDetail?.PoadocumentNumber ?? undefined;
            this.model.KYCDetail.PANNumber = viewData?.KYCDetail?.PANNumber ?? undefined;
            this.onChangePOADocument();
            this.onChangePOIDocument();

          }
          if (viewData?.DocumentDetail) {


            this.model.DocumentDetail.Id = viewData?.Id;
            this.model.DocumentDetail.CustomerPhoto = {

              FileName: viewData?.DocumentDetail?.CustomerPhoto?.split('/')[viewData?.DocumentDetail?.CustomerPhoto?.split('/')?.length - 1] ?? undefined,
              IsEditMode: false,
              File: viewData?.DocumentDetail?.CustomerPhoto ?? undefined,
              FileType: viewData?.DocumentDetail?.CustomerPhoto.split('.')[1] ?? undefined
            } ?? undefined;

            viewData?.DocumentDetail.KycDocumentPoi.forEach(element => {
              this.model.DocumentDetail.KycDocumentPoi?.push({

                FileName: element.split('/')[element.split('/').length - 1],
                IsEditMode: false,
                File: element,
                FileType: element.split('.')[1]
              })
            });


            viewData?.DocumentDetail.KycDocumentPoa.forEach(element => {
              this.model.DocumentDetail.KycDocumentPoa?.push({

                FileName: element.split('/')[element.split('/').length - 1],
                IsEditMode: false,
                File: element,
                FileType: element.split('.')[1]
              })
            });

            this.model.DocumentDetail.BlankCheque1 = {

              FileName: viewData?.DocumentDetail?.BlankCheque1?.split('/')[viewData?.DocumentDetail?.BlankCheque1?.split('/')?.length - 1],
              IsEditMode: false,
              File: viewData?.DocumentDetail?.BlankCheque1 ?? undefined,
              FileType: viewData?.DocumentDetail?.BlankCheque1.split('.')[1] ?? undefined
            } ?? undefined;


            this.model.DocumentDetail.BlankCheque2 = {

              FileName: viewData?.DocumentDetail?.BlankCheque2?.split('/')[viewData?.DocumentDetail?.BlankCheque2?.split('/')?.length - 1] ?? undefined,
              IsEditMode: false,
              File: viewData?.DocumentDetail?.BlankCheque2 ?? undefined,
              FileType: viewData?.DocumentDetail?.BlankCheque2?.split('.')[1] ?? undefined
            } ?? undefined;

            this.model.DocumentDetail.LoanDocument = {

              FileName: viewData?.DocumentDetail?.LoanDocument?.split('/')[viewData?.DocumentDetail?.LoanDocument?.split('/')?.length - 1] ?? undefined,
              IsEditMode: false,
              File: viewData?.DocumentDetail?.LoanDocument ?? undefined,
              FileType: viewData?.DocumentDetail?.LoanDocument.split('.')[1] ?? undefined
            } ?? undefined;

            this.model.DocumentDetail.AggrementLastPage = {

              FileName: viewData?.DocumentDetail?.AggrementLastPage?.split('/')[viewData?.DocumentDetail?.AggrementLastPage?.split('/')?.length - 1] ?? undefined,
              IsEditMode: false,
              File: viewData?.DocumentDetail?.AggrementLastPage ?? undefined,
              FileType: viewData?.DocumentDetail?.AggrementLastPage.split('.')[1] ?? undefined
            } ?? undefined;


            this.model.DocumentDetail.PromissoryNote = {

              FileName: viewData?.DocumentDetail?.PromissoryNote?.split('/')[viewData?.DocumentDetail?.PromissoryNote?.split('/')?.length - 1],
              IsEditMode: false,
              File: viewData?.DocumentDetail?.PromissoryNote ?? undefined,
              FileType: viewData?.DocumentDetail?.PromissoryNote.split('.')[1] ?? undefined
            } ?? undefined;
            this.model.DocumentDetail.AtmwithdrawalSlip = {

              FileName: viewData?.DocumentDetail?.AtmwithdrawalSlip?.split('/')[viewData?.DocumentDetail?.AtmwithdrawalSlip?.split('/')?.length - 1] ?? undefined,
              IsEditMode: false,
              File: viewData?.DocumentDetail?.AtmwithdrawalSlip ?? undefined,
              FileType: viewData?.DocumentDetail?.AtmwithdrawalSlip.split('.')[1] ?? undefined
            } ?? undefined;

            this.model.DocumentDetail.ForeClosureLetter = {

              FileName: viewData?.DocumentDetail?.ForeClosureLetter?.split('/')[viewData?.DocumentDetail?.ForeClosureLetter?.split('/')?.length - 1],
              IsEditMode: false,
              File: viewData?.DocumentDetail?.ForeClosureLetter ?? undefined,
              FileType: viewData?.DocumentDetail?.ForeClosureLetter.split('.')[1] ?? undefined
            } ?? undefined;

          }
        }
      })
    }
  }

  onDocumentAttach(type: number, file: FileInfo[]) {
    switch (type) {
      case 1:
        if (file.length > 0) {

          this.model.DocumentDetail.CustomerPhoto = {
            File: file[0].FileBase64,
            FileName: file[0].Name,
            FileType: file[0].Name.split('.')[1],
            IsEditMode: this.model.DocumentDetail.Id > 0 ? true : false
          }
        }
        break;

      case 2:
        if (file.length > 0) {

          this.model.DocumentDetail.BlankCheque1 = {
            File: file[0].FileBase64,
            FileName: file[0].Name,
            FileType: file[0].Name.split('.')[1],
            IsEditMode: this.model.DocumentDetail.Id > 0 ? true : false
          }
        }
        break;

      case 3:
        if (file.length > 0) {

          this.model.DocumentDetail.BlankCheque2 = {
            File: file[0].FileBase64,
            FileName: file[0].Name,
            FileType: file[0].Name.split('.')[1],
            IsEditMode: this.model.DocumentDetail.Id > 0 ? true : false
          }
        }
        break;

      case 4:
        if (file.length > 0) {
          this.model.DocumentDetail.KycDocumentPoi = [];
          file.forEach(element => {
            this.model.DocumentDetail.KycDocumentPoi?.push({
              File: element.FileBase64,
              FileName: element.Name,
              FileType: element.Name.split('.')[1],
              IsEditMode: this.model.DocumentDetail.Id > 0 ? true : false
            })
          });


        }
        break;

      case 5:
        if (file.length > 0) {
          this.model.DocumentDetail.KycDocumentPoa = [];
          file.forEach(element => {
            this.model.DocumentDetail.KycDocumentPoa?.push({
              File: element.FileBase64,
              FileName: element.Name,
              FileType: element.Name.split('.')[1],
              IsEditMode: this.model.DocumentDetail.Id > 0 ? true : false
            })
          });


        }
        break;

      case 6:
        if (file.length > 0) {

          this.model.DocumentDetail.LoanDocument = {
            File: file[0].FileBase64,
            FileName: file[0].Name,
            FileType: file[0].Name.split('.')[1],
            IsEditMode: this.model.DocumentDetail.Id > 0 ? true : false
          }
        }
        break;

      case 7:
        if (file.length > 0) {

          this.model.DocumentDetail.ForeClosureLetter = {
            File: file[0].FileBase64,
            FileName: file[0].Name,
            FileType: file[0].Name.split('.')[1],
            IsEditMode: this.model.DocumentDetail.Id > 0 ? true : false
          }
        }
        break;

      case 8:
        if (file.length > 0) {

          this.model.DocumentDetail.AtmwithdrawalSlip = {
            File: file[0].FileBase64,
            FileName: file[0].Name,
            FileType: file[0].Name.split('.')[1],
            IsEditMode: this.model.DocumentDetail.Id > 0 ? true : false
          }
        }
        break;

      case 9:
        if (file.length > 0) {

          this.model.DocumentDetail.PromissoryNote = {
            File: file[0].FileBase64,
            FileName: file[0].Name,
            FileType: file[0].Name.split('.')[1],
            IsEditMode: this.model.DocumentDetail.Id > 0 ? true : false
          }
        }
        break;

      case 10:
        if (file.length > 0) {

          this.model.DocumentDetail.AggrementLastPage = {
            File: file[0].FileBase64,
            FileName: file[0].Name,
            FileType: file[0].Name.split('.')[1],
            IsEditMode: this.model.DocumentDetail.Id > 0 ? true : false
          }
        }
        break;



      default:
        break;
    }
  }

  RemoveDocument(type: number, file: FileModel, isPOAPOIDoc: boolean) {
    let deleteModel = {} as BTGoldLoanDeletePostModel;
    deleteModel.LeadId = this.leadId;
    deleteModel.documentType = type;
    deleteModel.IsPOIPOADOC = isPOAPOIDoc;
    deleteModel.FileName = file.File;

    let serve = this._balanceTransferService.DeleteBTGoldLoanLeadDocumentFile(deleteModel).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.toast.success(Message.FileDeleted);
        switch (type) {
          case 1:

            this.model.DocumentDetail.CustomerPhoto = null;
            break;

          case 2:


            this.model.DocumentDetail.BlankCheque1 = null;

            break;

          case 3:


            this.model.DocumentDetail.BlankCheque2 = null;

            break;

          case 4:

            let fidx = this.model.DocumentDetail.KycDocumentPoi?.findIndex(x => x.FileName == file.FileName) ?? -1;

            if (fidx >= 0) {
              this.model.DocumentDetail.KycDocumentPoi?.slice(fidx, 1);
            }
            break;

          case 5:

            let fPOAidx = this.model.DocumentDetail.KycDocumentPoa?.findIndex(x => x.FileName == file.FileName) ?? -1;

            if (fPOAidx >= 0) {
              this.model.DocumentDetail.KycDocumentPoa?.slice(fPOAidx, 1);
            }



            break;

          case 6:
            this.model.DocumentDetail.LoanDocument = null;
            break;

          case 7:
            this.model.DocumentDetail.ForeClosureLetter = null;
            break;

          case 8:
            this.model.DocumentDetail.AtmwithdrawalSlip = null;
            break;

          case 9:
            this.model.DocumentDetail.PromissoryNote = null;

            break;

          case 10:
            this.model.DocumentDetail.AggrementLastPage = null;

            break;



          default:
            break;
        }
      }
    })
  }

  //#region  <<DropDown>>
  GetDropDowns() {
    this.GetDropDown();
    this.getDDLProducts();
    this.GetDDLJewelleryType();
    this.getDDLDocumentType();
    this.getPurpose();
  }
  getDDLProducts() {
    let serve = this._productService.GetProductbyCategory(ProductCategoryEnum.GoldLoan).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlProductModel = res?.Data as DDLProductModel[];
      }
    });
  }
  GetDropDown() {
    let serve = this._commonService.GetDropDown([DropDown_key.ddlGender, DropDown_key.ddlBank]).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        let ddls = res?.Data as DropDownModel;
        this.dropDown.ddlBank = ddls?.ddlBank ?? [];
        this.dropDown.ddlGender = ddls?.ddlGender ?? [];
      }
    });
  }
  getDropDownBranch() {
    this.ddlBranchModel = [];
    let serve = this._bankBranchService.GetBranchesbyBankId(this.BankId.toString()).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlBranchModel = res?.Data as DDLBranchModel[];
      }
    });
  }
  GetDDLJewelleryType() {
    let serve = this._jewelleryTypeService.GetDDLJewelleryType().subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlJewellaryType = res.Data as DDLJewellaryType[];
      }
    });

  }
  getPurpose() {
    let subscription = this._purposeService.GetDDLPurpose().subscribe(res => {
      subscription.unsubscribe();
      if (res.IsSuccess) {
        this.ddlPurpose = res.Data as ddlPurposeModel[];
      } else {
        this.toast.warning('Purpose list not available', 'No record found');
        this.ddlPurpose = [];
        return;
      }
    });
  }
  getAddressLine2(area?: any) {
    this.model.AddressDetail.AddressLine2 = area?.AddressLine2;
  }
  getCorrespondingAddressLine2(area?: any) {
    this.model.AddressDetail.CorrespondAddressLine2 = area?.AddressLine2;
  }
  getDropDownPinCodeArea(isCorrespond: boolean = false, isRemoveValue = false) {
    let pinCode: string = isCorrespond ? this.CorrespondAeraPincode : this.AeraPincode;

    if (isCorrespond) {
      this.ddlCorespondAreaModel = [];
      this.model.AddressDetail.CorrespondAeraPincodeId = isRemoveValue ? null : this.model.AddressDetail.CorrespondAeraPincodeId;
    } else {
      this.ddlAreaModel = [];
      this.model.AddressDetail.AeraPincodeId = isRemoveValue ? null : this.model.AddressDetail.AeraPincodeId;

    }

    let serve = this._stateDistrictService.GetAreaByPincode(pinCode).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {

        if (isCorrespond) {
          this.ddlCorespondAreaModel = res?.Data as AvailableAreaModel[];
        } else {
          this.ddlAreaModel = res?.Data as AvailableAreaModel[];
        }



      }
    })

  }
  onChangePinCode(PinCode: any) {
    if (this.model.AddressDetail) {
      this.getDropDownPinCodeArea();
      this.getDropDownBranch();
    }
  }
  onSameAddressAssign(eve: any) {
    if (eve.target.checked) {
      this.CorrespondAeraPincode = this.AeraPincode;
      this.model.AddressDetail.CorrespondAddress = this.model.AddressDetail.Address;
      this.model.AddressDetail.CorrespondAddressLine2 = this.model.AddressDetail.AddressLine2;
      this.getDropDownPinCodeArea(true);
      this.model.AddressDetail.CorrespondAeraPincodeId = this.model.AddressDetail.AeraPincodeId;
    }
  }

  getDDLDocumentType() {
    let serve = this._kycDocumentTypeService.GetDDLDocumentTypeForBTLeadKYC(true).subscribe(res => {
      serve.unsubscribe();
      if (res.IsSuccess) {
        this.ddlDocumentType = res?.Data as DDLDocumentTypeModel[];
        this.ddlDocumentTypePOI = res?.Data as DDLDocumentTypeModel[];
        this.ddlDocumentTypePOA = res?.Data as DDLDocumentTypeModel[];


        this.ddlDocumentTypePOI = this.ddlDocumentTypePOI?.filter(x => this.model?.KYCDetail?.PoadocumentTypeId ? x.Id != this.model?.KYCDetail?.PoadocumentTypeId : true);
        this.ddlDocumentTypePOA = this.ddlDocumentTypePOA?.filter(x => this.model?.KYCDetail?.PoidocumentTypeId ? x.Id != this.model?.KYCDetail?.PoidocumentTypeId : true);


      }
    });
  }

  onCheckDocumentNumberPOI(eve: any, typeId: number) {

    let dataItem = this.ddlDocumentTypePOI?.find(x => x.Id == typeId) as DDLDocumentTypeModel;

    if (dataItem?.IsNumeric) {
      return this._commonService.NumberOnly(eve);

    } else {
      return this._commonService.AlphaNumericOnly(eve);

    }
  }

  onCheckDocumentNumberPOA(eve: any, typeId: number) {

    let dataItem = this.ddlDocumentTypePOA?.find(x => x.Id == typeId) as DDLDocumentTypeModel;

    if (dataItem?.IsNumeric) {
      return this._commonService.NumberOnly(eve);

    } else {
      return this._commonService.AlphaNumericOnly(eve);

    }
  }

  onChangePOIDocument(value: any = this.model?.KYCDetail?.PoidocumentTypeId) {

    if (value) {


      let doc = this.ddlDocumentTypePOI?.find(x => x?.Id == value);
      this.f7.PoidocumentNumber.setValidators(Validators.compose([Validators.minLength(doc?.DocumentNumberLength as number), Validators.maxLength(doc?.DocumentNumberLength as number)]));
      this.f7.PoidocumentNumber.updateValueAndValidity();

      this.docPOIMaxChar = doc?.DocumentNumberLength ?? this.docPOIMaxChar;
    }
    this.ddlDocumentTypePOA = this.ddlDocumentType?.filter(x => this.model?.KYCDetail?.PoidocumentTypeId ? x.Id != this.model?.KYCDetail?.PoidocumentTypeId : true);

  }

  onChangePOADocument(value: any = this.model?.KYCDetail?.PoadocumentTypeId) {
    debugger
    if (value) {
      let doc = this.ddlDocumentTypePOA?.find(x => x.Id == value);
      this.f7.PoadocumentNumber.setValidators(Validators.compose([Validators.minLength(doc?.DocumentNumberLength as number), Validators.maxLength(doc?.DocumentNumberLength as number)]));
      this.f7.PoadocumentNumber.updateValueAndValidity();
      this.docPOAMaxChar = doc?.DocumentNumberLength ?? this.docPOAMaxChar;
    }

    this.ddlDocumentTypePOI = this.ddlDocumentType?.filter(x => this.model?.KYCDetail?.PoadocumentTypeId ? x.Id != this.model?.KYCDetail?.PoadocumentTypeId : true);

  }


  onGetJewellaryTypeName(value: any) {
    return this.ddlJewellaryType.find(x => x.Id == value)?.Name;

  }
  onJewellaryAdd() {
    this.UpdateJewellaryValidation(true);
    this.leadFormJewelleryDetail.markAllAsTouched();
    if (this.leadFormJewelleryDetail.valid) {
      this.model.JewelleryDetail.push(this.JewelleryModel);
      this.UpdateJewellaryValidation(false);

    }
  }
  onJewellaryEdit(idx: number) {
    this.UpdateJewellaryValidation(true);
    this.JewelleryModel = this.model.JewelleryDetail[idx];
    this.model.JewelleryDetail.splice(idx, 1);


  }
  onJewellaryDelete(idx: number) {
    this.model.JewelleryDetail.splice(idx, 1);
  }
  onJewellaryAddCancel() {
    if (this.JewelleryModel.JewelleryTypeId && this.JewelleryModel.Karats && this.JewelleryModel.Quantity && this.JewelleryModel.Weight) {
      this.model.JewelleryDetail.push(this.JewelleryModel);
      this.JewelleryModel = new BtGoldLoanLeadJewelleryDetailPostModel();
    }
    this.UpdateJewellaryValidation(false);

  }
  UpdateJewellaryValidation(enable = false) {
    if (enable) {
      this.f4.JewelleryType.setValidators([Validators.required]);
      this.f4.Quantity.setValidators([Validators.required]);
      this.f4.Weight.setValidators([Validators.required]);
      this.f4.Karats.setValidators([Validators.required]);
    } else {
      this.f4.JewelleryType.removeValidators([Validators.required]);
      this.f4.Quantity.removeValidators([Validators.required]);
      this.f4.Weight.removeValidators([Validators.required]);
      this.f4.Karats.removeValidators([Validators.required]);
    }
    this.f4.JewelleryType.updateValueAndValidity();
    this.f4.Quantity.updateValueAndValidity();
    this.f4.Weight.updateValueAndValidity();
    this.f4.Karats.updateValueAndValidity();
  }



  //#endregion

}
