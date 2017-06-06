CREATE TABLE [dbo].[Matrix] (
    [MatrixId]           UNIQUEIDENTIFIER NOT NULL,
    [Name]               VARCHAR (160)    NOT NULL,
    [RowArgumentName]    VARCHAR (120)    NOT NULL,
    [ColumnArgumentName] VARCHAR (120)    NULL,
    [ResultType]         VARCHAR (65)     CONSTRAINT [DF_Matrix_ResultType] DEFAULT ('Text') NOT NULL,
    CONSTRAINT [PK_Matrix] PRIMARY KEY NONCLUSTERED ([MatrixId] ASC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Matrix]
    ON [dbo].[Matrix]([Name] ASC);

