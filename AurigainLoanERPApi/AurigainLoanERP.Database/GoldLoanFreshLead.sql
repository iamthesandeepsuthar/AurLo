﻿CREATE TABLE [dbo].[GoldLoanFreshLead]
(
	[Id] BIGINT NOT NULL PRIMARY KEY Identity, 
    [FullName] NVARCHAR(2000) NOT NULL, 
    [FatherName] NVARCHAR(2000) NULL, 
    [DateOfBirth] DATETIME NOT NULL, 
    [Gender] NVARCHAR(50) NOT NULL, 
    [PrimaryMobileNumber] NVARCHAR(20) NOT NULL, 
    [SecondaryMobileNumber] NVARCHAR(20) NULL, 
    [LoanAmountRequired] FLOAT NOT NULL, 
    [Purpose] NVARCHAR(1000) NULL, 
    [LeadSourceByUserId] BIGINT NOT NULL references UserMaster(Id), 
    [CreatedDate] DATETIME NOT NULL DEFAULT getDate(), 
    [ModifedDate] DATETIME NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0
)