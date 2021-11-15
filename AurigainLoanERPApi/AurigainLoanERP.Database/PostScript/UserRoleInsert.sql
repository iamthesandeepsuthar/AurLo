IF  (SELECT COUNT(*) from UserRole )<1
BEGIN  
 
INSERT [dbo].[UserRole] ( [Name], [ParentId], [IsActive], [IsDelete], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy]) VALUES ( N'Super Admin', NULL, 1, 0, CAST(N'2021-09-24T22:11:06.610' AS DateTime), CAST(N'2021-10-07T02:12:31.483' AS DateTime), 1, NULL)
 
INSERT [dbo].[UserRole] ( [Name], [ParentId], [IsActive], [IsDelete], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy]) VALUES ( N'Admin (Web)', 1, 1, 0, CAST(N'2021-09-25T03:52:47.793' AS DateTime), CAST(N'2021-10-03T18:38:32.670' AS DateTime), 0, NULL)
 
INSERT [dbo].[UserRole] ([Name], [ParentId], [IsActive], [IsDelete], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy]) VALUES ( N'Zonal Manager', 2, 1, 0, CAST(N'2021-09-26T17:33:46.160' AS DateTime), CAST(N'2021-09-26T23:36:43.213' AS DateTime), 0, NULL)
 
INSERT [dbo].[UserRole] ( [Name], [ParentId], [IsActive], [IsDelete], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy]) VALUES ( N'Supervisor', 3, 1, 0, CAST(N'2021-09-26T17:33:46.160' AS DateTime), CAST(N'2021-09-26T23:36:43.213' AS DateTime), 0, NULL)
 
INSERT [dbo].[UserRole] ( [Name], [ParentId], [IsActive], [IsDelete], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy]) VALUES ( N'Agent (Web)', 4, 1, 0, CAST(N'2021-10-13T02:43:14.267' AS DateTime), NULL, 0, NULL)
 
INSERT [dbo].[UserRole] ( [Name], [ParentId], [IsActive], [IsDelete], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy]) VALUES ( N'Door Step Agent', 4, 1, 0, CAST(N'2021-10-13T02:43:28.403' AS DateTime), NULL, 0, NULL)

INSERT [dbo].[UserRole] ( [Name], [ParentId], [IsActive], [IsDelete], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy]) VALUES ( N'Customer', NULL , 1, 0, CAST(N'2021-10-13T02:43:28.403' AS DateTime), NULL, 0, NULL)
INSERT [dbo].[UserRole] ( [Name], [ParentId], [IsActive], [IsDelete], [CreatedOn], [ModifiedOn], [CreatedBy], [ModifiedBy]) VALUES ( N'Web Operator', 2 , 1, 0, CAST(N'2021-10-13T02:43:28.403' AS DateTime), NULL, 0, NULL)
 
END  