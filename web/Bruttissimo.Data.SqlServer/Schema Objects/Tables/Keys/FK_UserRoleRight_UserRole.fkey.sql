ALTER TABLE [dbo].[UserRoleRight]
    ADD CONSTRAINT [FK_UserRoleRight_UserRole] FOREIGN KEY ([UserRoleId]) REFERENCES [dbo].[UserRole] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

