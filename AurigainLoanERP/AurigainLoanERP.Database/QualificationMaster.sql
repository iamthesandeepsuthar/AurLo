CREATE TABLE [dbo].[QualificationMaster]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(500) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedOn] DATETIME NULL
)
