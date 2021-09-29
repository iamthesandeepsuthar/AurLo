CREATE TABLE [dbo].[DocumentType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DocumentName] NVARCHAR(MAX) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedOn] DATETIME NULL
)
