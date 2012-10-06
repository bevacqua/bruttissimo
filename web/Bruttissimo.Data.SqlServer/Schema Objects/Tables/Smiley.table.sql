CREATE TABLE [dbo].[Smiley] (
    [Id]            BIGINT IDENTITY (1, 1) NOT NULL,
    [ResourceKey]   NVARCHAR (50)    NOT NULL,
    [Emoticon]      NVARCHAR (50)    NOT NULL,
    [Aliases]       NVARCHAR (MAX)   NULL,
    [CssClass]      NVARCHAR (MAX)   NOT NULL
);