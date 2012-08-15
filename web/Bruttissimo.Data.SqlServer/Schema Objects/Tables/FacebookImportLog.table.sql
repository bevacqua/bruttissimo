CREATE TABLE [dbo].[FacebookImportLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
    [FacebookFeedId]  NVARCHAR (MAX) NULL,
	[Date] [datetime2](7) NOT NULL
);