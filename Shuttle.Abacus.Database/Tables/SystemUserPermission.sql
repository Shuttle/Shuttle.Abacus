CREATE TABLE [dbo].[SystemUserPermission](
	[SystemUserId] [uniqueidentifier] NOT NULL,
	[Permission] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_SystemUserPermission_SystemUser]    Script Date: 01/14/2010 11:00:12 ******/
ALTER TABLE [dbo].[SystemUserPermission]  ADD  CONSTRAINT [FK_SystemUserPermission_SystemUser] FOREIGN KEY([SystemUserId])
REFERENCES [dbo].[SystemUser] ([SystemUserId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SystemUserPermission] CHECK CONSTRAINT [FK_SystemUserPermission_SystemUser]