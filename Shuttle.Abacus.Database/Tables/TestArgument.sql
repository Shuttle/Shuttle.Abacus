CREATE TABLE [dbo].[TestArgument] (
    [TestId]       UNIQUEIDENTIFIER NOT NULL,
    [ArgumentName] VARCHAR (120)    NOT NULL,
    [Value]        VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_TestArgument] PRIMARY KEY CLUSTERED ([TestId] ASC, [ArgumentName] ASC),
    CONSTRAINT [FK_TestArgument_Test] FOREIGN KEY ([TestId]) REFERENCES [dbo].[Test] ([TestId]) ON DELETE CASCADE
);







