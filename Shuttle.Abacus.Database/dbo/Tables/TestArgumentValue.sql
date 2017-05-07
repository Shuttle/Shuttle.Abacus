CREATE TABLE [dbo].[TestArgumentValue] (
    [MethodTestId] UNIQUEIDENTIFIER NOT NULL,
    [ArgumentName] VARCHAR (120)    NOT NULL,
    [Value]        VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_TestArgumentValue] PRIMARY KEY CLUSTERED ([MethodTestId] ASC, [ArgumentName] ASC),
    CONSTRAINT [FK_TestArgumentValue_Test] FOREIGN KEY ([MethodTestId]) REFERENCES [dbo].[Test] ([TestId]) ON DELETE CASCADE
);

