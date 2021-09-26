CREATE TABLE [dbo].[UserDocument]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,    
	[DocumentTypeId] INT NOT NULL references DocumentType(Id), 
	[DocumentUrl]  NVARCHAR(MAX), 
    [UserId] bigint references UserMaster(Id) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedBy] BIGINT NULL, 

)
