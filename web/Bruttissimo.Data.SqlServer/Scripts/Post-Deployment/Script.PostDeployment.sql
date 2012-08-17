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

IF NOT EXISTS(SELECT NULL FROM UserRole WHERE Name = 'Regular')
    INSERT INTO UserRole VALUES('Regular')

IF NOT EXISTS(SELECT NULL FROM UserRole WHERE Name = 'Admin')
    INSERT INTO UserRole VALUES('Admin')

IF NOT EXISTS(SELECT NULL FROM UserRight WHERE Name = 'CanAccessSystemPanel')
    INSERT INTO UserRight VALUES('CanAccessSystemPanel')

IF NOT EXISTS(SELECT NULL FROM UserRight WHERE Name = 'CanAccessApplicationLogs')
    INSERT INTO UserRight VALUES('CanAccessApplicationLogs')

IF NOT EXISTS(SELECT NULL FROM UserRight WHERE Name = 'CanAccessApplicationJobs')
    INSERT INTO UserRight VALUES('CanAccessApplicationJobs')

IF NOT EXISTS(
    SELECT NULL FROM UserRoleRight RR
    WHERE RR.UserRoleId  = (SELECT Ro.Id FROM UserRole  Ro WHERE Ro.Name = 'Admin')
      AND RR.UserRightId = (SELECT Ri.Id FROM UserRight Ri WHERE Ri.Name = 'CanAccessSystemPanel')
)
BEGIN
    INSERT INTO UserRoleRight (UserRoleId, UserRightId)
    SELECT	Ro.Id, Ri.Id
      FROM  UserRight Ri, UserRole Ro
     WHERE (Ro.Name = 'Admin' AND
		    Ri.Name = 'CanAccessSystemPanel')
END

IF NOT EXISTS(
    SELECT NULL FROM UserRoleRight RR
    WHERE RR.UserRoleId  = (SELECT Ro.Id FROM UserRole  Ro WHERE Ro.Name = 'Admin')
      AND RR.UserRightId = (SELECT Ri.Id FROM UserRight Ri WHERE Ri.Name = 'CanAccessApplicationLogs')
)
BEGIN
    INSERT INTO UserRoleRight (UserRoleId, UserRightId)
    SELECT	Ro.Id, Ri.Id
      FROM  UserRight Ri, UserRole Ro
     WHERE (Ro.Name = 'Admin' AND
		    Ri.Name = 'CanAccessApplicationLogs')
END

IF NOT EXISTS(
    SELECT NULL FROM UserRoleRight RR
    WHERE RR.UserRoleId  = (SELECT Ro.Id FROM UserRole  Ro WHERE Ro.Name = 'Admin')
      AND RR.UserRightId = (SELECT Ri.Id FROM UserRight Ri WHERE Ri.Name = 'CanAccessApplicationJobs')
)
BEGIN
    INSERT INTO UserRoleRight (UserRoleId, UserRightId)
    SELECT	Ro.Id, Ri.Id
      FROM  UserRight Ri, UserRole Ro
     WHERE (Ro.Name = 'Admin' AND
		    Ri.Name = 'CanAccessApplicationJobs')
END