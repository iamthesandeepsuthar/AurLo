CREATE TABLE [dbo].[Managers]
(
	[Id] Bigint NOT NULL PRIMARY KEY Identity, 
    [FullName] NVARCHAR(1000) NOT NULL, 
    [DateOfBirth] datetime Null,
    [Gender] NVARCHAR(50) NOT NULL, 
    [FatherName] NVARCHAR(500) NULL, 
    [Address] NVARCHAR(2000) NULL, 
     [AddressLine2] NVARCHAR(1000) NULL, 
    [UserId] BIGINT NOT NULL references UserMaster(Id), 
    [DistrictId] BIGINT NULL  references District(Id), 
    [StateId] int NULL references State(Id),
    [Pincode] nvarchar(200) null,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [ModifiedDate] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [Setting] NVARCHAR(MAX) NULL, 
    [AreaPincodeId] BIGINT NULL References PincodeArea(Id)
)
