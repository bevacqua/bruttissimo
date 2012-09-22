ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [FK_User_UserSettings] FOREIGN KEY ([UserSettingsId]) REFERENCES [dbo].[UserSettings] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

