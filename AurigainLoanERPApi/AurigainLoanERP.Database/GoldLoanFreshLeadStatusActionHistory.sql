CREATE TABLE [dbo].[GoldLoanFreshLeadStatusActionHistory]
(

	[Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [LeadId] BIGINT NOT NULL  references GoldLoanFreshLead(Id),
	[LeadStatus] int null,
	[ActionTakenByUserId] bigint references UserMaster(Id),
	[ActionDate] DateTime not null default getdate(),
	[Remarks] NVarchar(max)
)
