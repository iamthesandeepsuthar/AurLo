CREATE TABLE [dbo].[BalanceTransferLoanReturn]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
	LeadId BIGINT NOT NULL REFERENCES BTGoldLoanLead(Id),
	AmountPaidToExistingBank bit DEFAULT 0 ,
	GoldReceived bit DEFAULT 0,
	GoldSubmittedToBank bit DEFAULT 0 ,
	LoanDisbursement bit DEFAULT 0,
	LoanAccountNumber nvarchar(50),
	BankName nvarchar(500),
	CustomerName nvarchar(500),
	PaymentMethod int ,
	UtrNumber nvarchar(50),
	Remarks nvarchar(1000),
	AmountReturn decimal, 
    [FinalPaymentDate] DATETIME NULL,
	
	)
