CREATE TABLE [dbo].[FormulaOperation](
	[FormulaId] [uniqueidentifier] NOT NULL,
	[Operation] [varchar](120) NOT NULL,
	[ValueSource] [varchar](120) NOT NULL,
	[ValueSelection] [varchar](120) NOT NULL,
	[SequenceNumber] [int] NOT NULL,
	[Text] [varchar](120) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_FormulaOperation_Formula]    Script Date: 01/14/2010 11:00:06 ******/
ALTER TABLE [dbo].[FormulaOperation]  WITH CHECK ADD  CONSTRAINT [FK_FormulaOperation_Formula] FOREIGN KEY([FormulaId])
REFERENCES [dbo].[Formula] ([FormulaId])
GO

ALTER TABLE [dbo].[FormulaOperation] CHECK CONSTRAINT [FK_FormulaOperation_Formula]