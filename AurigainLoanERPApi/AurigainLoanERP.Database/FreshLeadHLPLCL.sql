CREATE TABLE [dbo].[FreshLeadHLPLCL]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
    [FullName] NVARCHAR(1000) NOT NULL, 
    [FatherName] NVARCHAR(1000) NOT NULL, 
    [LeadType] bit not null,
    [EmailId] NVARCHAR(1000) NOT NULL, 
    [MobileNumber] NVARCHAR(20) NOT NULL, 
    [LoanAmount] FLOAT NOT NULL, 
    [AnnualIncome] FLOAT NULL, 
    [LeadSourceByUserId] BIGINT NOT NULL references UserMaster(Id), 
    [ProductId] INT NOT NULL references Product(Id), 
    [EmployeeType] NVARCHAR(200) NULL, 
    [NoOfITR] INT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedDate] DATETIME NOT NULL DEFAULT getDate(), 
    [ModifiedDate] DATETIME NULL,    
)
