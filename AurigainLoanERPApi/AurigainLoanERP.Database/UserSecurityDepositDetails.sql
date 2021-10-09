CREATE TABLE [dbo].[UserSecurityDepositDetails]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[UserId] bigint references UserMaster(Id) NOT NULL,
	[PaymentModeId] int references PaymentMode(Id) NOT NULL, 
    [TransactionStatus] INT NULL, 
    [Amount] DECIMAL(18, 2) NOT NULL, 
    [CreditDate] DATETIME NOT NULL, 
    [ReferanceNumber] NVARCHAR(30) NULL, 
    [AccountNumber] NVARCHAR(25) NULL, 
    [BankName] VARCHAR(500) NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedDate] DATETIME NULL,
)
