CREATE TABLE [dbo].[Test] (
    [Id]             UNIQUEIDENTIFIER NOT NULL,
    [Name]               VARCHAR (250)    NOT NULL,
    [FormulaId]        UNIQUEIDENTIFIER    NOT NULL,
    [ExpectedResult]     VARCHAR (120)    NOT NULL,
    [ExpectedResultDataTypeName] VARCHAR (65)     CONSTRAINT [DF_Test_ExpectedResultType] DEFAULT ('Decimal') NOT NULL,
    [Comparison]         VARCHAR (65)     CONSTRAINT [DF_Test_ComparisonType] DEFAULT ('Equals') NOT NULL,
    CONSTRAINT [PK_MethodTest] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);



