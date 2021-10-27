CREATE TABLE [dbo].[BankBranchMaster]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1), 
    [BranchName] NCHAR(10) NOT NULL, 
    [BranchCode] NVARCHAR(100) NOT NULL, 
    [IFSC] nvarchar(50)NOT NULL,
    [Pincode] NVARCHAR(20) NOT NULL,
    [Address] NVARCHAR(2000) NULL, 
    [ContactNumber] nvarchar(50),
    [BranchEmailId] nvarchar(200),
    [BankId] INT  references BankMaster(Id) NOT NULL, 
    [ConfigurationSettingJSON] NVARCHAR(MAX) NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedDate] DATETIME NULL
)
