using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class LeadStatusModel
    {
        public long LeadId { get; set; }
        public long Id { get; set; }
        public int LeadStatus{get;set;}
        public string Remark { get; set; }
        public DateTime ActionDate { get; set;}
        public long ActionTakenByUserId { get; set;}
        public string ActionTakenByUser { get; set;}
    }
}
