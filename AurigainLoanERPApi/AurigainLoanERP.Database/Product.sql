﻿CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(0,1), 
    [Name] NVARCHAR(1000) NOT NULL, 
    [Notes] NVARCHAR(MAX) NULL, 
    [ProductCategoryId] INT NOT NULL references ProductCategory(Id), 
    [MinimumAmount] FLOAT NULL, 
    [MaximumAmount] FLOAT NULL, 
    [InterestRate] FLOAT NULL, 
    [ProcessingFee] FLOAT NULL, 
    [InterestRateApplied] BIT NULL, 
    [MinimumTenure] FLOAT NULL, 
    [MaximumTenure] FLOAT NULL
)