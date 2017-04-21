CREATE TABLE [dbo].[Limit](
	[LimitId] [uniqueidentifier] NOT NULL,
	[OwnerName] [varchar](100) NOT NULL,
	[OwnerId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Type] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Limit] PRIMARY KEY NONCLUSTERED 
(
	[LimitId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]