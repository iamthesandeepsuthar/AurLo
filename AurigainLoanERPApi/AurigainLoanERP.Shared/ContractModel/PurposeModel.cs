using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class PurposeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive{get;set;}
    }
    public class ddlPurposeModel 
    {
        public int Id { get; set; }
        public string Name { get; set;}
    }
}
