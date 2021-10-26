CREATE TABLE [dbo].[GoldLoanFreshLeadAppointmentDetail]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
    [BankId] INT NOT NULL references BankMaster(Id), 
    [BranchId] int not null references BankBranchMaster (Id), 
    [AppointmentDate] DATETIME NULL, 
    [AppointmentTime] TIMESTAMP NULL, 
    [GLFreshLeadId] bigint not null references GoldLoanFreshLead(Id),
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDelete] BIT NULL DEFAULT 0, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GetDate()
)
