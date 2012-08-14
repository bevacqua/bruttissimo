CREATE TABLE [dbo].[Import](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Source] [tinyint] NOT NULL
);