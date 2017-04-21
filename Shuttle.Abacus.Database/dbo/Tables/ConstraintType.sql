CREATE TABLE [dbo].[ConstraintType](
	[Name] [varchar](100) NOT NULL,
	[Text] [varchar](100) NOT NULL,
	[EnabledForRestrictedAnswers] [tinyint] NOT NULL,
 CONSTRAINT [PK_ConstraintType] PRIMARY KEY CLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]