CREATE TABLE [dbo].[UserOTP]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,    
	[Mobile] NVARCHAR(20) NOT NULL , 
	[OTP]  NVARCHAR(10), 
    [UserId] bigint references UserMaster(Id) NULL,
    [IsVerify] BIT NOT NULL DEFAULT 0, 
    [SessionStartOn] DATETIME NOT NULL DEFAULT GETDATE(), 
    [ExpireOn] DATETIME NULL , 
    
)
