using System;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class PaymentModeModel
    {
        public int Id { get; set; }
        public string Mode { get; set; }
        public long? MinimumValue { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    /// <summary>
    /// Payment Mode Dropdown Model
    /// </summary>
    public class DDLPaymentModeModel
    {
        public int Id { get; set; }
        public string Mode { get; set; }

    }
}
