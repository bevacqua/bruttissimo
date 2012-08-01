ALTER TABLE [dbo].[UserConnection]
    ADD CONSTRAINT [FK_UserConnection_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

