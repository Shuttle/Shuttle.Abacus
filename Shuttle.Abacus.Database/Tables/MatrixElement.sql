CREATE TABLE [dbo].[MatrixElement] (
    [MatrixId]             UNIQUEIDENTIFIER NOT NULL,
    [Column]          INT              NOT NULL,
    [Row]             INT              NOT NULL,
    [Id] UNIQUEIDENTIFIER NOT NULL, 
    [Value]                VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_MatrixElement] PRIMARY KEY CLUSTERED ([MatrixId] ASC, [Column] ASC, [Row] ASC),
    CONSTRAINT [FK_MatrixElement_Matrix] FOREIGN KEY ([MatrixId]) REFERENCES [dbo].[Matrix] ([Id])
);








GO

CREATE UNIQUE INDEX [IX_MatrixElement_Id] ON [dbo].[MatrixElement] ([Id])
