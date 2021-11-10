IF  (SELECT COUNT(*) from ProductCategory )<1
BEGIN  
INSERT INTO [dbo].[ProductCategory] ([Name],[IsActive],[IsDelete],[CreatedDate])  VALUES  ('Gold Loan',1,0,GETDATE())
INSERT INTO [dbo].[ProductCategory] ([Name],[IsActive],[IsDelete] ,[CreatedDate])  VALUES  ('Personal Loan',1,0,GETDATE())
INSERT INTO [dbo].[ProductCategory] ([Name],[IsActive],[IsDelete] ,[CreatedDate])  VALUES  ('Car Loan',1,0,GETDATE())
INSERT INTO [dbo].[ProductCategory] ([Name],[IsActive],[IsDelete],[CreatedDate] )  VALUES  ('Home Loan',1,0,GETDATE())
 END  