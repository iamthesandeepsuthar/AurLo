CREATE TABLE [dbo].[BTGoldLoanLeadKYCDetail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
    [LeadId] BIGINT NOT NULL references BTGoldLoanLead(Id),
	[POIDocumentTypeId] INT NOT NULL references DocumentType(Id), 
    [POIDocumentNumber] VARCHAR(50) NOT NULL,
	[POADocumentTypeId] INT NOT NULL references DocumentType(Id), 
    [POADocumentNumber] VARCHAR(50) NOT NULL,

)
