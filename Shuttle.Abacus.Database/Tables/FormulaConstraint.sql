CREATE TABLE [dbo].[FormulaConstraint] (
    [Id] UNIQUEIDENTIFIER NOT NULL, 
    [FormulaId]      UNIQUEIDENTIFIER NOT NULL,
    [ArgumentId]   UNIQUEIDENTIFIER    NOT NULL,
    [Comparison] VARCHAR (65)     NOT NULL,
    [Value]          VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_FormulaConstraint] PRIMARY KEY CLUSTERED ([Id]),
    CONSTRAINT [FK_FormulaConstraint_Formula] FOREIGN KEY ([FormulaId]) REFERENCES [dbo].[Formula] ([Id]),
	CONSTRAINT [FK_FormulaConstraint_Argument] FOREIGN KEY ([ArgumentId]) REFERENCES [dbo].[Argument] ([Id])
);


GO

CREATE UNIQUE INDEX [IX_FormulaConstraint_Id] ON [dbo].[FormulaConstraint] ([Id])
