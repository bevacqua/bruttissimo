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

IF NOT EXISTS(SELECT NULL FROM Role WHERE Name = 'Regular')
    INSERT INTO Role VALUES('Regular')

IF NOT EXISTS(SELECT NULL FROM Role WHERE Name = 'Admin')
    INSERT INTO Role VALUES('Admin')

IF NOT EXISTS(SELECT NULL FROM [Right] WHERE Name = 'CanAccessSystemPanel')
    INSERT INTO [Right] VALUES('CanAccessSystemPanel')

IF NOT EXISTS(SELECT NULL FROM [Right] WHERE Name = 'CanAccessApplicationLogs')
    INSERT INTO [Right] VALUES('CanAccessApplicationLogs')

IF NOT EXISTS(SELECT NULL FROM [Right] WHERE Name = 'CanAccessApplicationJobs')
    INSERT INTO [Right] VALUES('CanAccessApplicationJobs')

IF NOT EXISTS(
    SELECT NULL FROM RoleRight RR
    WHERE RR.RoleId  = (SELECT Ro.Id FROM Role  Ro WHERE Ro.Name = 'Admin')
      AND RR.RightId = (SELECT Ri.Id FROM [Right] Ri WHERE Ri.Name = 'CanAccessSystemPanel')
)
BEGIN
    INSERT INTO RoleRight (RoleId, RightId)
    SELECT	Ro.Id, Ri.Id
      FROM  [Right] Ri, Role Ro
     WHERE (Ro.Name = 'Admin' AND
		    Ri.Name = 'CanAccessSystemPanel')
END

IF NOT EXISTS(
    SELECT NULL FROM RoleRight RR
    WHERE RR.RoleId  = (SELECT Ro.Id FROM Role  Ro WHERE Ro.Name = 'Admin')
      AND RR.RightId = (SELECT Ri.Id FROM [Right] Ri WHERE Ri.Name = 'CanAccessApplicationLogs')
)
BEGIN
    INSERT INTO RoleRight (RoleId, RightId)
    SELECT	Ro.Id, Ri.Id
      FROM  [Right] Ri, Role Ro
     WHERE (Ro.Name = 'Admin' AND
		    Ri.Name = 'CanAccessApplicationLogs')
END

IF NOT EXISTS(
    SELECT NULL FROM RoleRight RR
    WHERE RR.RoleId  = (SELECT Ro.Id FROM Role  Ro WHERE Ro.Name = 'Admin')
      AND RR.RightId = (SELECT Ri.Id FROM [Right] Ri WHERE Ri.Name = 'CanAccessApplicationJobs')
)
BEGIN
    INSERT INTO RoleRight (RoleId, RightId)
    SELECT	Ro.Id, Ri.Id
      FROM  [Right] Ri, Role Ro
     WHERE (Ro.Name = 'Admin' AND
		    Ri.Name = 'CanAccessApplicationJobs')
END