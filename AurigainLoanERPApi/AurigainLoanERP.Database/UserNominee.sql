CREATE TABLE [dbo].[UserNominee]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity, 
    [NamineeName] NVARCHAR(2000) NOT NULL, 
    [RelationshipWithNominee] NVARCHAR(100) NOT NULL,
    [UserId] bigint references UserMaster(Id) NOT NULL,
    [IsSelfDeclaration] BIT NOT NULL DEFAULT 0, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedBy] BIGINT NULL, 
)
