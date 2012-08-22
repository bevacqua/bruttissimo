CREATE TABLE [dbo].[Comment](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [bigint] NOT NULL,
	[PostId] [bigint] NOT NULL,
	[ParentId] [bigint] NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Created] [datetime2](7) NOT NULL
);