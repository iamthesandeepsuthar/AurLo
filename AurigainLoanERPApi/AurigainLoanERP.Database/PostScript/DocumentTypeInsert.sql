IF NOT EXISTS(SELECT Id from dbo.DocumentType WHERE Id =1)
BEGIN      

Insert INTO DocumentType(DocumentName,IsNumeric,DocumentNumberLength,IsKYC,RequiredFileCount) VALUES('Aadhar Card',1,12,1,2);
Insert INTO DocumentType(DocumentName,IsNumeric,DocumentNumberLength,IsKYC,RequiredFileCount) VALUES('Pan Card',0,10,1,1);
Insert INTO DocumentType(DocumentName,IsNumeric,DocumentNumberLength,IsKYC,RequiredFileCount) VALUES('Cheque',1,6,1,1);
END  
