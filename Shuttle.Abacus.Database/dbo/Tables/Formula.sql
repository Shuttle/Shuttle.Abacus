CREATE TABLE [dbo].[Formula] (
    [FormulaId]          UNIQUEIDENTIFIER NOT NULL,
    [Name]               VARCHAR (120)    NOT NULL,
    [MaximumFormulaName] VARCHAR (120)    CONSTRAINT [DF_Formula_MaximumFormulaName] DEFAULT ('') NOT NULL,
    [MinimumFormulaName] VARCHAR (120)    CONSTRAINT [DF_Formula_MinimumFormulaName] DEFAULT ('') NOT NULL,
    [ExecutionType]      VARCHAR (25)     CONSTRAINT [DF_Formula_ExecutionType] DEFAULT ('First') NOT NULL,
    CONSTRAINT [PK_Formula] PRIMARY KEY NONCLUSTERED ([FormulaId] ASC)
);



