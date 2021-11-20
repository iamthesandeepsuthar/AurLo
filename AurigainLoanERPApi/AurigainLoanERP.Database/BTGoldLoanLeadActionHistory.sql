CREATE TABLE [dbo].[BTGoldLoanLeadActionHistory]
(
	[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [LeadId] BIGINT NOT NULL  references BTGoldLoanLead(Id),
	[LeadStatus] int null,
	[AssignFromUserId] bigint references UserMaster(Id),
	[AssignToUserId] bigint references UserMaster(Id),
	[ActionDate] DateTime not null default getdate()
)
