CREATE TABLE [dbo].[Constraint](
	[OwnerName] [varchar](100) NOT NULL,
	[OwnerId] [uniqueidentifier] NOT NULL,
	[ArgumentId] [uniqueidentifier] NOT NULL,
	[ArgumentName] [varchar](100) NOT NULL,
	[Name] [varchar](60) NOT NULL,
	[Answer] [varchar](120) NOT NULL,
	[AnswerType] [varchar](100) NOT NULL,
	[Description] [varchar](250) NOT NULL,
	[SequenceNumber] [int] NOT NULL
) ON [PRIMARY]