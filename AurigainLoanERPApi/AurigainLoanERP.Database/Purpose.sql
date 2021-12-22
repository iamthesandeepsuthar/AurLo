CREATE TABLE [dbo].[Purpose]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1), 
    [Name] NVARCHAR(1000) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedBy] BIGINT NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedDate] DATETIME NULL
)
