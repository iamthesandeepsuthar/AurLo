﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.ContractModel
{
    public class BankModel
    {
        public BankModel()
        {
            Branches = new List<BranchModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string BankLogoUrl { get; set; }
        public string ContactNumber { get; set; }
        public string WebsiteUrl { get; set; }
        public string FaxNumber { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<BranchModel> Branches { get; set; }
    }
    public class BranchModel
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string Ifsc { get; set; }
        public string Address { get; set; }
        public string ContactNumber { get; set; }
        public string BranchEmailId { get; set; }
        public int BankId { get; set; }
        public string ConfigurationSettingJson { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class DDLBankModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class DDLBrnachModel
    {
        public int Id { get; set; }
        public string BrnachName { get; set; }
        public string Ifsc { get; set; }

    }
}