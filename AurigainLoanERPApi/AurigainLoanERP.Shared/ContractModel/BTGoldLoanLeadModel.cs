using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class BTGoldLoanLeadViewModel : BTGoldLoanLeadPostModel
    {
        public int LeadSourceByuserName { get; set; }
    }
    public class BTGoldLoanLeadPostModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Profession { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public string SecondaryMobile { get; set; }
        public string Purpose { get; set; }
        public long LeadSourceByuserId { get; set; }

        public BtGoldLoanLeadAddressPostModel AddressDetail { get; set; }
        public BtGoldLoanLeadAppointmentPostModel AppointmentDetail { get; set; }
        public BtGoldLoanLeadDocumentPostModel DocumentDetail { get; set; }
        public BtGoldLoanLeadExistingLoanPostModel ExistingLoanDetail { get; set; }
        public BtGoldLoanLeadJewelleryDetailPostModel JewelleryDetail { get; set; }
        public BtGoldLoanLeadKYCDetailPostModel KYCDetail { get; set; }

    }

    public class BtGoldLoanLeadAddressPostModel
    {
        public long Id { get; set; }
        public string Address { get; set; }
        public long? AeraPincodeId { get; set; }
        public string CorrespondAddress { get; set; }
        public long? CorrespondAeraPincodeId { get; set; }
    }
    public class BtGoldLoanLeadAppointmentPostModel
    {
        public long Id { get; set; }
        public int? BranchId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
    }
    public class BtGoldLoanLeadDocumentPostModel
    {
        public long Id { get; set; }
        public string CustomerPhoto { get; set; }
        public string KycDocumentPoi { get; set; }
        public string KycDocumentPoa { get; set; }
        public string BlankCheque1 { get; set; }
        public string BlankCheque2 { get; set; }
        public string LoanDocument { get; set; }
        public string AggrementLastPage { get; set; }
        public string PromissoryNote { get; set; }
        public string AtmwithdrawalSlip { get; set; }
        public string ForeClosureLetter { get; set; }
    }

    public class BtGoldLoanLeadExistingLoanPostModel
    {
        public long Id { get; set; }
        public string BankName { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
        public decimal? JewelleryValuation { get; set; }
        public decimal? OutstandingAmount { get; set; }
        public decimal? BalanceTransferAmount { get; set; }
        public decimal? RequiredAmount { get; set; }
        public int? Tenure { get; set; }
    }
    public class BtGoldLoanLeadJewelleryDetailPostModel
    {
        public long Id { get; set; }
       
        public int? JewelleryTypeId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Weight { get; set; }
        public int? Karats { get; set; }
    }
    public class BtGoldLoanLeadKYCDetailPostModel
    {
        public long Id { get; set; }
       
        public int PoidocumentTypeId { get; set; }
        public string PoidocumentNumber { get; set; }
        public int PoadocumentTypeId { get; set; }
        public string PoadocumentNumber { get; set; }
    }
}
