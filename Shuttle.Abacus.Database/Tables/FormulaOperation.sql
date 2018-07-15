CREATE TABLE [dbo].[FormulaOperation] (
    [Id] UNIQUEIDENTIFIER NOT NULL, 
    [FormulaId]      UNIQUEIDENTIFIER NOT NULL,
    [SequenceNumber] INT              NOT NULL,
    [Operation]      VARCHAR (120)    NOT NULL,
    [ValueProviderName]    VARCHAR (120)    NOT NULL,
    [InputParameter] VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_FormulaOperation] PRIMARY KEY CLUSTERED ([FormulaId] ASC, [SequenceNumber] ASC),
    CONSTRAINT [FK_FormulaOperation_Formula] FOREIGN KEY ([FormulaId]) REFERENCES [dbo].[Formula] ([Id])
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_FormulaOperation]
    ON [dbo].[FormulaOperation]([Id]);

