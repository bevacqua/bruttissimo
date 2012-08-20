CREATE TABLE [dbo].[FacebookImportLog](
	[Id]                [bigint] IDENTITY(1,1)  NOT NULL,
	[ImportDate]        [datetime2](7)          NOT NULL,
    [FacebookFeedId]    NVARCHAR (MAX)          NULL,
	[PostUpdated]       [datetime2](7)          NOT NULL,
    [QueryCount]        [int]                   NOT NULL,
);