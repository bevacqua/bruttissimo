CREATE TABLE [dbo].[Post] (
    [Id]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]          BIGINT         NULL,
    [LinkId]          BIGINT         NULL,
    [FacebookFeedId]  NVARCHAR (MAX) NULL,
    [FacebookPostId]  NVARCHAR (MAX) NULL,
    [FacebookUserId]  NVARCHAR (MAX) NULL,
    [UserMessage]     NVARCHAR (MAX) NULL,
    [Created]         DATETIME2 (7)  NOT NULL,
    [Updated]         DATETIME2 (7)  NULL
);