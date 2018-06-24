CREATE TABLE [dbo].[FormulaOperation] (
    [FormulaId]      UNIQUEIDENTIFIER NOT NULL,
    [SequenceNumber] INT              NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL, 
    [Operation]      VARCHAR (120)    NOT NULL,
    [ValueProviderName]    VARCHAR (120)    NOT NULL,
    [InputParameter] VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_FormulaOperation] PRIMARY KEY CLUSTERED ([FormulaId] ASC, [SequenceNumber] ASC),
    CONSTRAINT [FK_FormulaOperation_Formula] FOREIGN KEY ([FormulaId]) REFERENCES [dbo].[Formula] ([Id])
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_FormulaOperation]
    ON [dbo].[FormulaOperation]([Id]);

