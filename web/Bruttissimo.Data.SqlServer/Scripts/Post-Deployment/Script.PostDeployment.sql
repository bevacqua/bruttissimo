/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT INTO [UserRole] ([Name])
SELECT 'Regular'
UNION ALL
SELECT 'Admin'

INSERT INTO [UserRight] ([Name])
SELECT 'CanAccessApplicationLogs'

INSERT INTO [UserRoleRight] ([UserRoleId], [UserRightId])
SELECT	[Ro].[Id], [Ri].[Id] FROM [UserRight] Ri, [UserRole] Ro
 WHERE	[Ro].[Name] = 'Admin' AND
		[Ri].[Name] = 'CanAccessApplicationLogs'