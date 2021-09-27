CREATE TABLE [dbo].[PaymentMode]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Mode] NVARCHAR(200) NOT NULL, 
    [MinimumValue] BIGINT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedOn] DATETIME NULL
)
