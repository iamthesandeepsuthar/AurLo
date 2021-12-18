namespace AurigainLoanERP.Shared.Common.Model
{
    public static class FilePathConstant
    {
        public const string UserProfile = "\\Content\\UserFiles\\UserProfile\\";
        public const string UserDocsFile = "\\Content\\UserFiles\\UserDocs\\";
        public const string BankLogoFile = "\\Content\\BankFiles\\Logo\\";
        public const string BTGoldLeadsDocsFile = "\\Content\\UserFiles\\LeadsDocs\\BTGoldLoan\\";
        public const string BTGoldLeadReturnCheques = "\\Content\\UserFiles\\LeadDocs\\BTGoldReturnDoc\\";
        public const string FreshGoldLeadsDocsFile = "\\Content\\UserFiles\\LeadsDocs\\FreshGoldLoan\\";

    }

    public static class EmailPathConstant
    {
        public const string RegisterTemplate = "\\StaticFiles\\EmailTemplate\\Registration.html";
        public const string UserApproveTemplate = "\\StaticFiles\\EmailTemplate\\UserApproval.html";
        public const string GoldLoanLeadGenerationTemplate = "\\StaticFiles\\EmailTemplate\\GoldLoanLeadGeneration.html";

    }

    public static class TokenClaimsConstant
    {
        public const string GenerateTime = "GenerateTime";
        public const string UserId = "UserId";
        public const string UserName = "UserName";
        public const string RoleName = "RoleName";
        public const string RoleId = "RoleId";
        public const string RoleLevel = "RoleLevel";


    }
}
