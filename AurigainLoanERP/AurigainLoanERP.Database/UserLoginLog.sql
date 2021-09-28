CREATE TABLE [dbo].[UserLoginLog]
(
	[Id] BIGINT NOT NULL PRIMARY KEY Identity, 
    [UserId] BIGINT references UserMaster(Id) NOT NULL, 
    [LoggedInTIme] DATETIME NOT NULL, 
    [LoggedOutTime] DATETIME NOT NULL
)
