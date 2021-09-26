CREATE TABLE [dbo].[State]
(
	[Id] INT NOT NULL PRIMARY KEY Identity (1,1), 
    [Name] NVARCHAR(300) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedON] DATETIME NULL
)
