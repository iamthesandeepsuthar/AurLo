IF NOT EXISTS(SELECT Id from dbo.DocumentType WHERE Id =1)
BEGIN      

Insert INTO DocumentType(DocumentName) VALUES('Aadhar Card');
Insert INTO DocumentType(DocumentName) VALUES('Pan Card');
Insert INTO DocumentType(DocumentName) VALUES('Cheque');
END  
