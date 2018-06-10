CREATE TABLE [dbo].[Argument] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name]       VARCHAR (120)    NOT NULL,
    [ValueType] VARCHAR (65)     NOT NULL,
    CONSTRAINT [PK_Argument] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Argument]
    ON [dbo].[Argument]([Name] ASC);

