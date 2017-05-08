CREATE TABLE [dbo].[TestArgumentValue] (
    [TestId]       UNIQUEIDENTIFIER NOT NULL,
    [ArgumentName] VARCHAR (120)    NOT NULL,
    [Value]        VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_TestArgumentValue] PRIMARY KEY CLUSTERED ([TestId] ASC, [ArgumentName] ASC),
    CONSTRAINT [FK_TestArgumentValue_Test] FOREIGN KEY ([TestId]) REFERENCES [dbo].[Test] ([TestId]) ON DELETE CASCADE
);



