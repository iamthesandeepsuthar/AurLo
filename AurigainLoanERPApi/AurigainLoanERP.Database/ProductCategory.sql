CREATE TABLE [dbo].[ProductCategory]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(0,1), 
    [Name] NVARCHAR(200) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedDate] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedDate] DATETIME NULL, 
    [Note] NVARCHAR(MAX) NULL
)
