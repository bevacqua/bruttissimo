﻿ALTER TABLE [dbo].[RoleRight]
    ADD CONSTRAINT [FK_RoleRight_Right] FOREIGN KEY ([RightId]) REFERENCES [dbo].[Right] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

