CREATE TABLE [dbo].[MatrixConstraint] (
    [Id] UNIQUEIDENTIFIER NOT NULL, 
    [MatrixId]      UNIQUEIDENTIFIER NOT NULL,
    [Axis] VARCHAR(10) NOT NULL,
    [Index] INT              NOT NULL,
    [Comparison] VARCHAR (65)     NOT NULL,
    [Value]          VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_MatrixConstraint] PRIMARY KEY CLUSTERED ([MatrixId] ASC, [Axis] ASC, [Index] ASC),
    CONSTRAINT [FK_MatrixConstraint_Matrix] FOREIGN KEY ([MatrixId]) REFERENCES [dbo].[Matrix] ([Id])
);


GO

CREATE UNIQUE INDEX [IX_MatrixConstraint_Id] ON [dbo].[MatrixConstraint] ([Id])
