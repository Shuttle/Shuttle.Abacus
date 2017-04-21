CREATE TABLE [dbo].[DecimalValue](
	[DecimalValueId] [uniqueidentifier] NOT NULL,
	[DecimalTableId] [uniqueidentifier] NOT NULL,
	[ColumnIndex] [int] NOT NULL,
	[RowIndex] [int] NOT NULL,
	[DecimalValue] [float] NOT NULL,
 CONSTRAINT [PK_DecimalValue] PRIMARY KEY NONCLUSTERED 
(
	[DecimalValueId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_DecimalValue_DecimalTable]    Script Date: 01/14/2010 11:00:04 ******/
ALTER TABLE [dbo].[DecimalValue]  WITH CHECK ADD  CONSTRAINT [FK_DecimalValue_DecimalTable] FOREIGN KEY([DecimalTableId])
REFERENCES [dbo].[DecimalTable] ([DecimalTableId])
GO

ALTER TABLE [dbo].[DecimalValue] CHECK CONSTRAINT [FK_DecimalValue_DecimalTable]