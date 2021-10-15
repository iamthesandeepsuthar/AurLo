CREATE TABLE [dbo].[UserDoorStepAgent]
(
	[Id] INT NOT NULL PRIMARY KEY Identity	,
	[UserId] bigint references UserMaster(Id) NOT NULL,
	[FullName] Nvarchar(2000) NOT NULL, 
    [FatherName] NVARCHAR(2000) NOT NULL, 
    [UniqueId] NVARCHAR(500) NOT NULL,
    [SelfFunded] bit NOT NULL,
	[Gender] NVARCHAR(50) NOT NULL, 
    [QualificationId] INT NOT NULL references QualificationMaster(Id), 
    [Address] NVARCHAR(MAX) NULL, 
    [PinCode] NVARCHAR(50) NULL, 
    [DistrictId] INT NULL references District(Id),
    [DateOfBirth] DateTime,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedBy] BIGINT NULL, 
    [SecurityDepositId] INT references UserSecurityDepositDetails(Id) NULL, 

)
