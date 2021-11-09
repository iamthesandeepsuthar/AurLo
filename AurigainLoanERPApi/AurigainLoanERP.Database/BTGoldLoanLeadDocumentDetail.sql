CREATE TABLE [dbo].[BTGoldLoanLeadDocumentDetail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
    [LeadId] BIGINT NOT NULL references BTGoldLoanLead(Id),
    [CustomerPhoto] NVARCHAR(MAX) NULL,
    [KYCDocumentPOI] NVARCHAR(MAX) NULL, 
    [KYCDocumentPOA] NVARCHAR(MAX) NULL, 
    [BlankCheque1] NVARCHAR(MAX) NULL, 
    [BlankCheque2] NVARCHAR(MAX) NULL, 
    [LoanDocument] NVARCHAR(MAX) NULL, 
    [AggrementLastPage] NVARCHAR(MAX) NULL, 
    [PromissoryNote] NVARCHAR(MAX) NULL, 
    [ATMWithdrawalSlip] NVARCHAR(MAX) NULL, 
    [ForeClosureLetter] NVARCHAR(MAX) NULL, 
    

)
