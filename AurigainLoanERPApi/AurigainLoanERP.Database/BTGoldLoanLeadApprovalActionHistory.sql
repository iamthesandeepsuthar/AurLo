CREATE TABLE [dbo].[BTGoldLoanLeadApprovalActionHistory]
(
	[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[LeadId] BIGINT NOT NULL  references BTGoldLoanLead(Id),
	[ApprovalStatus] INT NULL,
	[ActionTakenByUserId] BIGINT REFERENCES UserMaster(Id),
	[ActionDate] DATETIME NOT NULL DEFAULT GETDATE(),
	[Remarks] NVARCHAR(MAX)
)
