using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class PincodeAreaModel
    {
        public long Id { get; set; }
        public string Pincode { get; set; }
        public string AreaName { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public long DistrictId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? Modifieddate { get; set; }
        public DistrictModel District { get; set; }
    }
    public class AvailableAreaModel
    {
        public long Id { get;set;}
        public string AreaName { get;set;}
        public string PinCode { get;set;}
        public long DistrictId { get;set;}
        public long StateId { get;set;}
    }
    public class AddressDetailModel
    {
        public string AreaName { get; set;}
        public string StateName { get; set;}
        public string DistrictName { get; set;}
        public long DistrictId { get; set;}
        public long StateId { get; set;}
        public long AreaPincodeId { get; set;}
    }
}
