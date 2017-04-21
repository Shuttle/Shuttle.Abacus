CREATE TABLE [dbo].[ArgumentRestrictedAnswer](
	[ArgumentId] [uniqueidentifier] NOT NULL,
	[Answer] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_ArgumentRestrictedAnswer_Argument]    Script Date: 01/14/2010 10:59:58 ******/
ALTER TABLE [dbo].[ArgumentRestrictedAnswer]  WITH CHECK ADD  CONSTRAINT [FK_ArgumentRestrictedAnswer_Argument] FOREIGN KEY([ArgumentId])
REFERENCES [dbo].[Argument] ([ArgumentId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ArgumentRestrictedAnswer] CHECK CONSTRAINT [FK_ArgumentRestrictedAnswer_Argument]