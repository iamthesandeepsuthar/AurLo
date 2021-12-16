CREATE TABLE [dbo].[BalanceTransferReturnBankChequeDetail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
	BTReturnId	BIGINT NOT NULL REFERENCES BalanceTransferLoanReturn(Id),
	ChequeNumber nvarchar(10) NOT NULL,
	ChequeImage nvarchar(max)
)
