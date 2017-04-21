CREATE TABLE [dbo].[GraphNodeArgument](
	[CalculationId] [uniqueidentifier] NOT NULL,
	[ArgumentId] [uniqueidentifier] NOT NULL,
	[Format] [varchar](250) NOT NULL,
	[SequenceNumber] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_GraphNodeArgument_Argument]    Script Date: 01/14/2010 11:00:07 ******/
ALTER TABLE [dbo].[GraphNodeArgument]  WITH CHECK ADD  CONSTRAINT [FK_GraphNodeArgument_Argument] FOREIGN KEY([ArgumentId])
REFERENCES [dbo].[Argument] ([ArgumentId])
GO

ALTER TABLE [dbo].[GraphNodeArgument] CHECK CONSTRAINT [FK_GraphNodeArgument_Argument]