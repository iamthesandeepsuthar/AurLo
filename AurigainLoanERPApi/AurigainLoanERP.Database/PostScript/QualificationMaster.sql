IF  (SELECT COUNT(*) from QualificationMaster )<1
BEGIN  
 
INSERT INTO [dbo].[QualificationMaster] ([Name] )  VALUES  ('10th')
INSERT INTO [dbo].[QualificationMaster] ([Name] )  VALUES  ('12th')
INSERT INTO [dbo].[QualificationMaster] ([Name] )  VALUES  ('Graduation')
INSERT INTO [dbo].[QualificationMaster] ([Name] )  VALUES  ('Post graduation')
 
END  