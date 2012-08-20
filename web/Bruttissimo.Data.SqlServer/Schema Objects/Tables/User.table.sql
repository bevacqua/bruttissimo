CREATE TABLE [dbo].[User] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [RoleId]      BIGINT         NOT NULL,
    [Email]       NVARCHAR (320) NULL,
    [DisplayName] NVARCHAR (60)  NULL,
    [Password]    NVARCHAR (60)  NULL,
    [Created]     DATETIME2 (7)  NOT NULL,
    [TimeZone]    FLOAT          NOT NULL
);