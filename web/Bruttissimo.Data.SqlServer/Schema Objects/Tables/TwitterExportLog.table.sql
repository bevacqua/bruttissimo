CREATE TABLE [dbo].[TwitterExportLog](
	[Id]                [bigint] IDENTITY(1,1)  NOT NULL,
	[StartDate]         [datetime2](7)          NOT NULL,
	[Duration]          [time](7)               NOT NULL,
    [PostCount]         [int]                   NOT NULL,
    [ExportCount]       [int]                   NOT NULL
);