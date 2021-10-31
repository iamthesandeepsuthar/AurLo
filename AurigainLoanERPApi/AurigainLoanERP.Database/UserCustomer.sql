CREATE TABLE [dbo].[UserCustomer]
(
	[Id] BIGINT NOT NULL PRIMARY KEY Identity, 
    [FullName] NVARCHAR(2000) NULL, 
    [FatherName] NVARCHAR(2000) NULL, 
    [Gender] NVARCHAR(30) NULL,  
    [UserId] bigint references UserMaster(Id) not null,
    [PincodeAreaId] BIGINT references PincodeArea(Id),
    [Address] NVARCHAR(MAX) NULL,        
    [DateOfBirth] DateTime,       
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedBy] BIGINT NULL, 
)
