CREATE TABLE [dbo].[MethodTestArgumentAnswer](
	[MethodTestId] [uniqueidentifier] NOT NULL,
	[ArgumentId] [uniqueidentifier] NOT NULL,
	[Answer] [varchar](100) NOT NULL,
	[AnswerType] [varchar](100) NOT NULL,
	[ArgumentName] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_MethodTestArgumentAnswer_Argument]    Script Date: 01/14/2010 11:00:10 ******/
ALTER TABLE [dbo].[MethodTestArgumentAnswer]  WITH CHECK ADD  CONSTRAINT [FK_MethodTestArgumentAnswer_Argument] FOREIGN KEY([ArgumentId])
REFERENCES [dbo].[Argument] ([ArgumentId])
GO

ALTER TABLE [dbo].[MethodTestArgumentAnswer] CHECK CONSTRAINT [FK_MethodTestArgumentAnswer_Argument]
GO
/****** Object:  ForeignKey [FK_MethodTestArgumentAnswer_MethodTest]    Script Date: 01/14/2010 11:00:11 ******/
ALTER TABLE [dbo].[MethodTestArgumentAnswer]  WITH CHECK ADD  CONSTRAINT [FK_MethodTestArgumentAnswer_MethodTest] FOREIGN KEY([MethodTestId])
REFERENCES [dbo].[MethodTest] ([MethodTestId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[MethodTestArgumentAnswer] CHECK CONSTRAINT [FK_MethodTestArgumentAnswer_MethodTest]