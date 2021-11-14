﻿CREATE TABLE [dbo].[BTGoldLoanLead]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
    [FullName] NVARCHAR(3000) NOT NULL, 
    [FatherName] NVARCHAR(3000) NOT NULL, 
    [Gender] VARCHAR(50) NOT NULL, 
    [DateOfBirth] DATETIME NOT NULL, 
    [Profession] VARCHAR(350) NULL,   
    [Mobile] VARCHAR(20) NOT NULL,
    [LoanAmount] DECIMAL NOT NULL, 
    [ProductId] INT Not NULL References Product(Id),
    [EmailId] NVARCHAR(500) NULL, 
    [SecondaryMobile] VARCHAR(20) NULL, 
    [Purpose] NVARCHAR(3000) NULL, 
    [LeadSourceBYUserId] BIGINT NOT NULL REFERENCES USERMASTER(ID),
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedBy] BIGINT NULL, 
    
)
