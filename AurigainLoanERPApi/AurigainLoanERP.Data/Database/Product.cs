﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AurigainLoanERP.Data.Database
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public int ProductCategoryId { get; set; }
        public double? MinimumAmount { get; set; }
        public double? MaximumAmount { get; set; }
        public double? InterestRate { get; set; }
        public double? ProcessingFee { get; set; }
        public bool? InterestRateApplied { get; set; }
        public double? MinimumTenure { get; set; }
        public double? MaximumTenure { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}