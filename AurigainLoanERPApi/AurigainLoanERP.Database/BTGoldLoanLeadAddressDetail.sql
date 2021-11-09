CREATE TABLE [dbo].[BTGoldLoanLeadAddressDetail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
    [LeadId] BIGINT NOT NULL references BTGoldLoanLead(ID),
    [Address] NVARCHAR(3500) NULL, 
    [AeraPincodeId] BIGINT NULL references PincodeArea(Id),
    [CorrespondAddress] NVARCHAR(3500) NULL, 
    [CorrespondAeraPincodeId] BIGINT NULL references PincodeArea(Id),
  
)
