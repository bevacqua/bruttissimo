IF NOT EXISTS(SELECT NULL FROM [Right] WHERE Name = 'CanAccessApplicationVariables')
    INSERT INTO [Right] VALUES('CanAccessApplicationVariables')

IF NOT EXISTS(
    SELECT NULL FROM RoleRight RR
    WHERE RR.RoleId  = (SELECT Ro.Id FROM Role  Ro WHERE Ro.Name = 'Admin')
      AND RR.RightId = (SELECT Ri.Id FROM [Right] Ri WHERE Ri.Name = 'CanAccessApplicationVariables')
)
BEGIN
    INSERT INTO RoleRight (RoleId, RightId)
    SELECT	Ro.Id, Ri.Id
      FROM  [Right] Ri, Role Ro
     WHERE (Ro.Name = 'Admin' AND
		    Ri.Name = 'CanAccessApplicationVariables')
END