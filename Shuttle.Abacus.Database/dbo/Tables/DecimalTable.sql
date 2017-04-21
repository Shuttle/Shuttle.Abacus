CREATE TABLE [dbo].[DecimalTable](
	[DecimalTableId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](160) NOT NULL,
	[RowArgumentId] [uniqueidentifier] NOT NULL,
	[ColumnArgumentId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DecimalTable] PRIMARY KEY NONCLUSTERED 
(
	[DecimalTableId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_DecimalTable_Argument_Column]    Script Date: 01/14/2010 11:00:03 ******/
ALTER TABLE [dbo].[DecimalTable]  WITH CHECK ADD  CONSTRAINT [FK_DecimalTable_Argument_Column] FOREIGN KEY([ColumnArgumentId])
REFERENCES [dbo].[Argument] ([ArgumentId])
GO

ALTER TABLE [dbo].[DecimalTable] CHECK CONSTRAINT [FK_DecimalTable_Argument_Column]
GO
/****** Object:  ForeignKey [FK_DecimalTable_Argument_Row]    Script Date: 01/14/2010 11:00:03 ******/
ALTER TABLE [dbo].[DecimalTable]  WITH CHECK ADD  CONSTRAINT [FK_DecimalTable_Argument_Row] FOREIGN KEY([RowArgumentId])
REFERENCES [dbo].[Argument] ([ArgumentId])
GO

ALTER TABLE [dbo].[DecimalTable] CHECK CONSTRAINT [FK_DecimalTable_Argument_Row]