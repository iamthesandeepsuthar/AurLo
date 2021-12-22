CREATE TABLE [dbo].[BTGoldLoanLeadAddressDetail]
(
	[Id] BIGINT NOT NULL PRIMARY KEY identity(1,1), 
    [LeadId] BIGINT NOT NULL references BTGoldLoanLead(ID),
    [Address] NVARCHAR(2000) NULL, 
    [AddressLine2] NVARCHAR(1000) NULL, 
    [AeraPincodeId] BIGINT NULL references PincodeArea(Id),
    [CorrespondAddress] NVARCHAR(2000) NULL, 
    [CorrespondAeraPincodeId] BIGINT NULL references PincodeArea(Id),
    [CorrespondAddressLine2] NVARCHAR(1000) NULL, 
)
