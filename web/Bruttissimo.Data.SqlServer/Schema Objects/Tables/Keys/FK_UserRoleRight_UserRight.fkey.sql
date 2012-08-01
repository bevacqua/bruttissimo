ALTER TABLE [dbo].[UserRoleRight]
    ADD CONSTRAINT [FK_UserRoleRight_UserRight] FOREIGN KEY ([UserRightId]) REFERENCES [dbo].[UserRight] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

