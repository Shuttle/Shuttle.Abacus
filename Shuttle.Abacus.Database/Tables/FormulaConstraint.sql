CREATE TABLE [dbo].[FormulaConstraint] (
    [FormulaId]      UNIQUEIDENTIFIER NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL, 
    [ArgumentId]   UNIQUEIDENTIFIER    NOT NULL,
    [Comparison] VARCHAR (65)     NOT NULL,
    [Value]          VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_FormulaConstraint] PRIMARY KEY CLUSTERED ([FormulaId] ASC),
    CONSTRAINT [FK_FormulaConstraint_Formula] FOREIGN KEY ([FormulaId]) REFERENCES [dbo].[Formula] ([Id]),
	CONSTRAINT [FK_FormulaConstraint_Argument] FOREIGN KEY ([ArgumentId]) REFERENCES [dbo].[Argument] ([Id])
);


GO

CREATE UNIQUE INDEX [IX_FormulaConstraint_Id] ON [dbo].[FormulaConstraint] ([Id])
