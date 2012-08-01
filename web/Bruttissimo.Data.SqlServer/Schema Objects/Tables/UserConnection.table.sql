CREATE TABLE [dbo].[UserConnection] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserId]              BIGINT         NOT NULL,
    [TwitterId]           NVARCHAR (MAX) NULL,
    [OpenId]              NVARCHAR (MAX) NULL,
    [FacebookId]          NVARCHAR (MAX) NULL,
    [FacebookAccessToken] NVARCHAR (MAX) NULL
);

