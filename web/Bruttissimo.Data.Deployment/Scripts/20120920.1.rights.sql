IF NOT EXISTS(SELECT NULL FROM [Right] WHERE Name = 'CanResetApplicationPool')
    INSERT INTO [Right] VALUES('CanResetApplicationPool')

IF NOT EXISTS(
    SELECT NULL FROM RoleRight RR
    WHERE RR.RoleId  = (SELECT Ro.Id FROM Role  Ro WHERE Ro.Name = 'Admin')
      AND RR.RightId = (SELECT Ri.Id FROM [Right] Ri WHERE Ri.Name = 'CanResetApplicationPool')
)
BEGIN
    INSERT INTO RoleRight (RoleId, RightId)
    SELECT	Ro.Id, Ri.Id
      FROM  [Right] Ri, Role Ro
     WHERE (Ro.Name = 'Admin' AND
		    Ri.Name = 'CanResetApplicationPool')
END