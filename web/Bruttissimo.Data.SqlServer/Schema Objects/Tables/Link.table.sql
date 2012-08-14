CREATE TABLE [dbo].[Link] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Type]         TINYINT        NOT NULL,
    [ReferenceUri] NVARCHAR (MAX) NOT NULL,
    [Title]        NVARCHAR (MAX) NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [Picture]      NVARCHAR (MAX) NULL,
    [Created]      DATETIME2 (7)  NOT NULL
);