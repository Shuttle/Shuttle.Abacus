﻿CREATE TABLE [dbo].[FormulaOperation] (
    [FormulaId]      UNIQUEIDENTIFIER NOT NULL,
    [SequenceNumber] INT              NOT NULL,
    [Operation]      VARCHAR (120)    NOT NULL,
    [ValueSource]    VARCHAR (120)    NOT NULL,
    [ValueSelection] VARCHAR (120)    NOT NULL,
    [Text]           VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_FormulaOperation] PRIMARY KEY CLUSTERED ([FormulaId] ASC, [SequenceNumber] ASC),
    CONSTRAINT [FK_FormulaOperation_Formula] FOREIGN KEY ([FormulaId]) REFERENCES [dbo].[Formula] ([FormulaId])
);

