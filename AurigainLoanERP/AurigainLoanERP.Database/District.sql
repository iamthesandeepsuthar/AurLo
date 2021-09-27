CREATE TABLE [dbo].[District]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1), 
    [Name] NVARCHAR(500) NOT NULL, 
    [Pincode] nvarchar(30) NOT NULL,
    [State_Id] INT NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedOn] DATETIME NULL, 
    CONSTRAINT [FK_District_State] FOREIGN KEY (State_Id) REFERENCES State(Id)
)
