CREATE TABLE [dbo].[BTGoldLoanLeadAppointmentDetail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
    [LeadId] BIGINT NOT NULL references BTGoldLoanLead(Id), 
    [BranchId] INT NULL references BankBranchMaster(Id), 
    [AppointmentDate] DATETIME NULL, 
    [AppointmentTime] TIME NULL, 

)
