CREATE TABLE [dbo].[UserLoginLog]
(
	[Id] BIGINT NOT NULL PRIMARY KEY Identity,     
    [LoggedInTIme] DATETIME NOT NULL, 
    [LoggedOutTime] DATETIME NOT NULL, 
    [Mobile] NVARCHAR(20) NULL, 
    [UserId] BIGINT NOT NULL references UserMaster(Id)
)
