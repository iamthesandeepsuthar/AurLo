using System;
using System.Collections.Generic;
using System.Text;

namespace AurigainLoanERP.Shared.Common.Model
{
    public static class FilePathConstant
    {
        public const string UserProfile = "\\Content\\UserFiles\\UserProfile\\";
        public const string UserDocsFile = "\\Content\\UserFiles\\UserDocs\\";
        public const string BankLogoFile = "\\Content\\BankFiles\\Logo\\";
        public const string BTGoldLeadsDocsFile = "\\Content\\UserFiles\\LeadsDocs\\BTGoldLoan\\";
        public const string FreshGoldLeadsDocsFile = "\\Content\\UserFiles\\LeadsDocs\\FreshGoldLoan\\";

    }

    public static class EmailPathConstant
    {
        public const string RegisterTemplate = "\\StaticFiles\\EmailTemplate\\Registration.html";
        public const string UserApproveTemplate = "\\StaticFiles\\EmailTemplate\\UserApproval.html";
        public const string GoldLoanLeadGenerationTemplate = "\\StaticFiles\\EmailTemplate\\GoldLoanLeadGeneration.html";



    }
}
