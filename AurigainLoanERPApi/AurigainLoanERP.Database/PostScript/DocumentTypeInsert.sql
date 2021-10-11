IF NOT EXISTS(SELECT Id from dbo.DocumentType WHERE Id =1)
BEGIN      

Insert INTO DocumentType(DocumentName,IsNumeric,DocumentNumberLength) VALUES('Aadhar Card',1,12);
Insert INTO DocumentType(DocumentName,IsNumeric,DocumentNumberLength) VALUES('Pan Card',0,10);
Insert INTO DocumentType(DocumentName,IsNumeric,DocumentNumberLength) VALUES('Cheque',1,6);
END  
