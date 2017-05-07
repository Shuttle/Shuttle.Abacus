CREATE TABLE [dbo].[MatrixElement] (
    [MatrixId]    UNIQUEIDENTIFIER NOT NULL,
    [ColumnIndex] INT              NOT NULL,
    [RowIndex]    INT              NOT NULL,
    [Value]       VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_MatrixElement] PRIMARY KEY CLUSTERED ([MatrixId] ASC, [ColumnIndex] ASC, [RowIndex] ASC),
    CONSTRAINT [FK_DecimalValue_DecimalTable] FOREIGN KEY ([MatrixId]) REFERENCES [dbo].[Matrix] ([MatrixId])
);



