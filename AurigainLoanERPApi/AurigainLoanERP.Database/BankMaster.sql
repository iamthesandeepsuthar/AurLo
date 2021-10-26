CREATE TABLE [dbo].[BankMaster]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1), 
    [Name] NVARCHAR(1000) NOT NULL, 
    [BankLogoUrl] NVARCHAR(1000) NULL, 
    [ContactNumber] NVARCHAR(20) NULL, 
    [Website_Url] NVARCHAR(200) NULL, 
    [FaxNumber] NVARCHAR(50) NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedDate] DATETIME NULL
)
