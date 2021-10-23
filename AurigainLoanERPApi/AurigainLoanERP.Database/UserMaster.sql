﻿CREATE TABLE [dbo].[UserMaster]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [UserRoleId] INT NOT NULL references UserRole(Id),
    [UserName] NVARCHAR(500) NOT NULL, 
    [MPin] NVARCHAR(500) NOT NULL,    
    [Password] NVARCHAR(MAX) NULL, 
    [Email] NVARCHAR(300) NULL, 
    [Mobile] NVARCHAR(20) NULL ,
    [ProfilePath] NVARCHAR(MAX),     
    [DeviceToken] NVARCHAR(MAX) NULL, 
    [Token] NVARCHAR(MAX) NULL,
    [IsApproved] bit NOT NULL, 
    [IsWhatsApp] BIT NOT NULL DEFAULT 0,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedBy] BIGINT NULL, 
    
 

)
