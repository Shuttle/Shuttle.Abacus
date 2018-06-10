CREATE TABLE [dbo].[Formula] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]               VARCHAR (120)    NOT NULL,
    [MaximumFormulaName] VARCHAR (120)    CONSTRAINT [DF_Formula_MaximumFormulaName] DEFAULT ('') NOT NULL,
    [MinimumFormulaName] VARCHAR (120)    CONSTRAINT [DF_Formula_MinimumFormulaName] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Formula] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);



