CREATE TABLE [dbo].[DocumentType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DocumentName] NVARCHAR(MAX) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsNumeric] BIT NOT NULL DEFAULT 0 ,
    [IsKYC] BIT NOT NULL DEFAULT 0 ,
    [RequiredFileCount] INT NOT NULL DEFAULT 0,
    [DocumentNumberLength] INT NULL,
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedOn] DATETIME NULL
)
