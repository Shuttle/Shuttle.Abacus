CREATE TABLE [dbo].[MatrixElement] (
    [Id] UNIQUEIDENTIFIER NOT NULL, 
    [MatrixId]             UNIQUEIDENTIFIER NOT NULL,
    [Row]             INT              NOT NULL,
    [Column]          INT              NOT NULL,
    [Value]                VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_MatrixElement] PRIMARY KEY CLUSTERED ([MatrixId] ASC, [Row] ASC, [Column] ASC),
    CONSTRAINT [FK_MatrixElement_Matrix] FOREIGN KEY ([MatrixId]) REFERENCES [dbo].[Matrix] ([Id])
);








GO

CREATE UNIQUE INDEX [IX_MatrixElement_Id] ON [dbo].[MatrixElement] ([Id])
