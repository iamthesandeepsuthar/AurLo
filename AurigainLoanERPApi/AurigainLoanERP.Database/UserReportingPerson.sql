CREATE TABLE [dbo].[UserReportingPerson]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity, 
    [UserId] BIGINT REFERENCES UserMaster(Id) NOT NULL,
    [ReportingUserId] BIGINT REFERENCES UserMaster(Id) NOT NULL,
    [CreatedOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ModifiedOn] DATETIME NULL, 
    [CreatedBy] BIGINT NULL, 
    [ModifiedBy] BIGINT NULL, 
)
