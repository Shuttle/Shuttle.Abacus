CREATE TABLE [dbo].[Formula](
	[FormulaId] [uniqueidentifier] NOT NULL,
	[OwnerName] [varchar](100) NOT NULL,
	[OwnerId] [uniqueidentifier] NOT NULL,
	[SequenceNumber] [int] NOT NULL,
	[Description] [varchar](2000) NOT NULL,
 CONSTRAINT [PK_Formula] PRIMARY KEY NONCLUSTERED 
(
	[FormulaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]