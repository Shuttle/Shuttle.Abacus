CREATE TABLE [dbo].[MethodTest](
	[MethodTestId] [uniqueidentifier] NOT NULL,
	[MethodId] [uniqueidentifier] NOT NULL,
	[ExpectedResult] DECIMAL(18, 6) NOT NULL,
	[Description] [varchar](250) NOT NULL,
 CONSTRAINT [PK_MethodTest] PRIMARY KEY NONCLUSTERED 
(
	[MethodTestId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_MethodTest_Method]    Script Date: 01/14/2010 11:00:09 ******/
ALTER TABLE [dbo].[MethodTest]  WITH CHECK ADD  CONSTRAINT [FK_MethodTest_Method] FOREIGN KEY([MethodId])
REFERENCES [dbo].[Method] ([MethodId])
GO

ALTER TABLE [dbo].[MethodTest] CHECK CONSTRAINT [FK_MethodTest_Method]