CREATE TABLE [dbo].[TestArgument] (
    [TestId]       UNIQUEIDENTIFIER NOT NULL,
    [ArgumentId] UNIQUEIDENTIFIER    NOT NULL,
    [Value]        VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_TestArgument] PRIMARY KEY CLUSTERED ([TestId] ASC, [ArgumentId] ASC),
    CONSTRAINT [FK_TestArgument_Test] FOREIGN KEY ([TestId]) REFERENCES [dbo].[Test] ([Id]) ON DELETE CASCADE
);







