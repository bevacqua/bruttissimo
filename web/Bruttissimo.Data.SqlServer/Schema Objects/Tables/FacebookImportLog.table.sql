CREATE TABLE [dbo].[FacebookImportLog](
	[Id]                [bigint] IDENTITY(1,1)  NOT NULL,
	[StartDate]         [datetime2](7)          NOT NULL,
	[Duration]          [time](7)               NOT NULL,
    [FacebookFeedId]    NVARCHAR (MAX)          NOT NULL,
	[PostUpdated]       [datetime2](7)          NULL,
    [QueryCount]        [int]                   NOT NULL,
    [PostCount]         [int]                   NOT NULL,
    [InsertCount]       [int]                   NOT NULL
);