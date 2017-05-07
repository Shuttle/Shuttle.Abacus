CREATE TABLE [dbo].[Test] (
    [TestId]             UNIQUEIDENTIFIER NOT NULL,
    [FormulaId]          UNIQUEIDENTIFIER NOT NULL,
    [ExpectedResult]     VARCHAR (120)    NOT NULL,
    [ExpectedResultType] VARCHAR (65)     CONSTRAINT [DF_Test_ExpectedResultType] DEFAULT ('Text') NOT NULL,
    [ComparisonType]     VARCHAR (65)     CONSTRAINT [DF_Test_ComparisonType] DEFAULT ('Equals') NOT NULL,
    [Description]        VARCHAR (250)    NOT NULL,
    CONSTRAINT [PK_MethodTest] PRIMARY KEY NONCLUSTERED ([TestId] ASC)
);

