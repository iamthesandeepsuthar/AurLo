CREATE TABLE [dbo].[UserBank]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity, 
    [BankName] NVARCHAR(2000) NOT NULL, 
    [AccountNumber] NVARCHAR(500) NOT NULL,
    [IFSCCode] NVARCHAR(100) NOT NULL,
    [Address] NVARCHAR(Max) NULL,
    [UserId] bigint references UserMaster(Id) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedBy] BIGINT NULL, 
)
