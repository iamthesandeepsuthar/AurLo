IF NOT EXISTS(SELECT Id from UserRole WHERE Id = 1)
BEGIN  
Insert INTO UserRole (Name,CreatedBy) VALUES('Admin',1);
END  