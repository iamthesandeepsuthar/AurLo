CREATE TABLE [dbo].[BTGoldLoanLeadExistingLoanDetail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
    [LeadId] BIGINT NOT NULL references BTGoldLoanLead(Id), 
    [BankName] NVARCHAR(1000) NULL, 
    [Amount] DECIMAL NULL, 
    [Date] DATETIME NULL, 
    [JewelleryValuation] DECIMAL NULL, 
    [OutstandingAmount] DECIMAL NULL, 
    [BalanceTransferAmount] DECIMAL NULL, 
    [RequiredAmount] DECIMAL NULL, 
    [Tenure] INT NULL,
)
