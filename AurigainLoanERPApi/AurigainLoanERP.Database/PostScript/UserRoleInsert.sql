IF NOT EXISTS(SELECT Id from UserRole WHERE Id = 1)
BEGIN  
Insert INTO UserRole ( Name, ParentId, IsActive, IsDelete,CreatedOn, CreatedBy, ModifiedBy) VALUES ( 'Super Admin', null, 1, 0, CAST(N'2021-09-26T17:33:46.160' AS DateTime),0,null);
INSERT into UserRole ( Name, ParentId, IsActive, IsDelete,CreatedOn, CreatedBy, ModifiedBy) VALUES ( 'Admin', 1, 1, 0, CAST(N'2021-09-26T17:33:46.160' AS DateTime),0,null);
INSERT into UserRole ( Name, ParentId, IsActive, IsDelete,CreatedOn, CreatedBy, ModifiedBy) VALUES ( 'Zonal Manager', 2, 1, 0, CAST(N'2021-09-26T17:33:46.160' AS DateTime), 0, NULL);
INSERT into UserRole ( Name, ParentId, IsActive, IsDelete,CreatedOn, CreatedBy, ModifiedBy) VALUES ( 'Supervisor', 3, 1, 0, CAST(N'2021-09-26T17:33:46.160' AS DateTime), 0, NULL);
INSERT into UserRole ( Name, ParentId, IsActive, IsDelete,CreatedOn, CreatedBy, ModifiedBy) VALUES ( 'Agent', 4, 1, 0, CAST(N'2021-09-26T17:33:46.160' AS DateTime), 0, NULL);
INSERT into UserRole ( Name, ParentId, IsActive, IsDelete,CreatedOn, CreatedBy, ModifiedBy) VALUES ( 'Doorstep Agent', 4, 1, 0, CAST(N'2021-09-26T17:33:46.160' AS DateTime), 0, NULL);
END  