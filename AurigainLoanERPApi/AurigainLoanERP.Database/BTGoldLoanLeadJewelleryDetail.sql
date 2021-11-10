CREATE TABLE [dbo].[BTGoldLoanLeadJewelleryDetail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
    [LeadId] BIGINT NOT NULL references BTGoldLoanLead(Id), 
    [JewelleryTypeId] INT NULL references JewellaryType(Id), 
    [Quantity] INT NULL, 
    [Weight] DECIMAL NULL, 
    [Karats] INT NULL, 

)
