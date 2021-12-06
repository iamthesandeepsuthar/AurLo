﻿CREATE TABLE [dbo].[UserRole]
(
	[Id] INT NOT NULL PRIMARY KEY IDENtITY(1,1),
	Name VARCHAR(250),
	ParentId INT,
	IsActive BIT NOT NULL DEFAULT 1,
	IsDelete BIT NOT NULL DEFAULT 0,
	CreatedOn DATETIME NOT NULL DEFAULT GETDATE(),
	ModifiedOn DATETIME,
	CreatedBy BIGINT NOT NULL,
	ModifiedBy BIGINT, 
    [UserRoleLevel] INT NULL
)
