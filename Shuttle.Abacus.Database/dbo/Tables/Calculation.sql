CREATE TABLE [dbo].[Calculation](
	[CalculationId] [uniqueidentifier] NOT NULL,
	[MethodId] [uniqueidentifier] NOT NULL,
	[OwnerName] [varchar](100) NOT NULL,
	[OwnerId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Type] [varchar](20) NOT NULL,
	[Required] [tinyint] NOT NULL,
	[SequenceNumber] [int] NOT NULL,
 CONSTRAINT [PK_Calculation] PRIMARY KEY NONCLUSTERED 
(
	[CalculationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Calculation_Method]    Script Date: 01/14/2010 10:59:59 ******/
ALTER TABLE [dbo].[Calculation]  WITH CHECK ADD  CONSTRAINT [FK_Calculation_Method] FOREIGN KEY([MethodId])
REFERENCES [dbo].[Method] ([MethodId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Calculation] CHECK CONSTRAINT [FK_Calculation_Method]