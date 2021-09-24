/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
IF (EXISTS(SELECT * FROM [dbo].[UserRole]))  
BEGIN  
    TRUNCATE TABLE [dbo].[MyReferenceTable]  
Insert INTO UserRole (Name,CreatedBy) VALUES('Admin',1)


END  