CREATE TABLE [dbo].[Argument](
	[ArgumentId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[AnswerType] [varchar](100) NOT NULL,
	[IsSystemData] [tinyint] NOT NULL CONSTRAINT [DF_Argument_IsSystemData]  DEFAULT ((0)),
 CONSTRAINT [PK_Argument] PRIMARY KEY CLUSTERED 
(
	[ArgumentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]