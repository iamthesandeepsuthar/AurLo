CREATE TABLE [dbo].[UserDocumentFiles]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    DocumentId BIGINT NOT NULL REFERENCES UserDocument(Id),
    [FileName] NVARCHAR(MAX) NOT NULL,
    [FileType] NVARCHAR(500) NOT NULL,
    [Path] NVARCHAR(MAX) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedBy] BIGINT NULL, 
    

 
)
