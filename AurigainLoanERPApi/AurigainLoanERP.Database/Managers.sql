CREATE TABLE [dbo].[Managers]
(
	[Id] Bigint NOT NULL PRIMARY KEY Identity(0,1), 
    [FullName] NVARCHAR(1000) NOT NULL, 
    [MobileNUmber] NVARCHAR(20) NOT NULL
)
