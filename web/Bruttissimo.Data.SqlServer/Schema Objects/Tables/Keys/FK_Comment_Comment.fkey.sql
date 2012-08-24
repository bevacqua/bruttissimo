ALTER TABLE [dbo].[Comment]
    ADD  CONSTRAINT [FK_Comment_Comment] FOREIGN KEY([ParentId]) REFERENCES [dbo].[Comment] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

