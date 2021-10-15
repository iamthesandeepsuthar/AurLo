﻿CREATE TABLE [dbo].[UserAvailability]
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] BIGINT NOT NULL, 
    [MondayST] TIME NULL, 
    [MondayET] TIME NULL, 
    [TuesdayST] TIME NULL, 
    [TuesdayET] TIME NULL,     
    [WednesdayST] TIME NULL, 
    [WednesdayET] TIME NULL,  
    [ThursdayST] TIME NULL, 
    [ThursdayET] TIME NULL,  
    [FridayST] TIME NULL, 
    [FridayET] TIME NULL,  
    [SaturdayST] TIME NULL, 
    [SaturdayET] TIME NULL, 
    [SundayST] TIME NULL, 
    [SundayET] TIME NULL, 
    [Capacity ] INT NULL, 
    [PinCode] BIGINT NULL, 
    [DistrictId] INT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [IsDelete] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NOT NULL DEFAULT GetDate(), 
    [ModifiedOn] DATETIME NULL, 
    CONSTRAINT [FK_UserAvailability_ToTable] FOREIGN KEY ([UserId]) REFERENCES [UserMaster](Id),
)
