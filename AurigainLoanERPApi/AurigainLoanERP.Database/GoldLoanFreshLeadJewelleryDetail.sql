CREATE TABLE [dbo].[GoldLoanFreshLeadJewelleryDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    
    [JewelleryTypeId] INT NOT NULL, 
    [Quantity] FLOAT NOT NULL, 
    [Weight] FLOAT NULL, 
    [Karats] INT NULL, 
    [GLFreshLeadId] BIGINT NOT NULL references GoldLoanFreshLead (Id), 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NULL, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedDate] DATETIME NULL
)
