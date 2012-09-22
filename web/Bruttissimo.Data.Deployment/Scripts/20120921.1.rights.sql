IF NOT EXISTS(SELECT NULL FROM [Right] WHERE Name = 'CanAccessDebuggingDetails')
    INSERT INTO [Right] VALUES('CanAccessDebuggingDetails')

IF NOT EXISTS(
    SELECT NULL FROM RoleRight RR
    WHERE RR.RoleId  = (SELECT Ro.Id FROM Role  Ro WHERE Ro.Name = 'Admin')
      AND RR.RightId = (SELECT Ri.Id FROM [Right] Ri WHERE Ri.Name = 'CanAccessDebuggingDetails')
)
BEGIN
    INSERT INTO RoleRight (RoleId, RightId)
    SELECT	Ro.Id, Ri.Id
      FROM  [Right] Ri, Role Ro
     WHERE (Ro.Name = 'Admin' AND
		    Ri.Name = 'CanAccessDebuggingDetails')
END