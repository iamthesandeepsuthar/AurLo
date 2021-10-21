CREATE TABLE [dbo].[JewellaryType]
(
	[Id] INT NOT NULL PRIMARY KEY identity(0,1), 
    [Name] NVARCHAR(500) NOT NULL, 
    [Description] NVARCHAR(1000) NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL
)
