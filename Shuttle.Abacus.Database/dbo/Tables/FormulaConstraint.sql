﻿CREATE TABLE [dbo].[FormulaConstraint] (
    [FormulaId]      UNIQUEIDENTIFIER NOT NULL,
    [SequenceNumber] INT              NOT NULL,
    [ArgumentName]   VARCHAR (120)    NOT NULL,
    [ComparisonType] VARCHAR (65)     NOT NULL,
    [Value]          VARCHAR (120)    NOT NULL,
    [Description]    VARCHAR (250)    NOT NULL,
    CONSTRAINT [PK_Constraint] PRIMARY KEY CLUSTERED ([FormulaId] ASC, [SequenceNumber] ASC),
    CONSTRAINT [FK_FormulaConstraint_Formula] FOREIGN KEY ([FormulaId]) REFERENCES [dbo].[Formula] ([FormulaId])
);

