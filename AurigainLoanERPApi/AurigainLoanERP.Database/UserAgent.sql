CREATE TABLE [dbo].[UserAgent]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
    [UserId] bigint references UserMaster(Id) NOT NULL,
	[FullName] Nvarchar(2000) NOT NULL,
	[FatherName] Nvarchar(2000) NULL,
	[UniqueId] Nvarchar(500) NOT NULL, 
    [Gender] NVARCHAR(50) NOT NULL, 
    [QualificationId] INT NOT NULL references QualificationMaster(Id), 
    [Address] NVARCHAR(MAX) NULL, 
    [PinCode] NVARCHAR(50) NULL, 
    [DistrictId] INT NULL references District(Id),
    [DateOfBirth] DateTime,
    [ProfilePictureUrl] NVARCHAR(MAX),
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedBy] BIGINT NULL, 
 
)
