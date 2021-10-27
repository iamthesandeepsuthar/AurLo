CREATE TABLE [dbo].[GoldLoanFreshLeadKycDocument]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [KycDocumentTypeId] INT NOT NULL references DocumentType(Id), 
    [DocumentNumber] NVARCHAR(50) NOT NULL, 
    [PanNumber] NVARCHAR(20) NULL, 
    [PincodeAreaId] BIGINT NOT NULL References PincodeArea(Id), 
    [AddressLine1] NVARCHAR(2500) NULL, 
    [AddressLine2] NVARCHAR(2000) NULL, 
    [IsDelete] BIT NULL DEFAULT 0, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreatedDate] DATETIME NOT NULL DEFAULT getDate(), 
    [GLFreshLeadId] BIGINT NOT NULL references GoldLoanFreshLead (Id)
)
