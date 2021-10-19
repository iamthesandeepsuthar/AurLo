CREATE TABLE [dbo].[PincodeArea]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(0,1), 
    [Pincode] NVARCHAR(20) NOT NULL, 
    [AreaName] NVARCHAR(500) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [DistrictId] BIGINT NOT NULL  references District(Id), 
    [CreatedDate] DATETIME NOT NULL DEFAULT getdate(), 
    [Modifieddate] DATETIME NULL
)
