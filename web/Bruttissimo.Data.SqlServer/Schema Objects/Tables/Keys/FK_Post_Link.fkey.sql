﻿ALTER TABLE [dbo].[Post]
    ADD CONSTRAINT [FK_Post_Link] FOREIGN KEY ([LinkId]) REFERENCES [dbo].[Link] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

