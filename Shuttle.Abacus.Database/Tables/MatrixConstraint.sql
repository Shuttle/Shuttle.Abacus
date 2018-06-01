CREATE TABLE [dbo].[MatrixConstraint] (
    [MatrixId]      UNIQUEIDENTIFIER NOT NULL,
    [Axis]   VARCHAR (120)    NOT NULL,
    [SequenceNumber] INT              NOT NULL,
    [Comparison] VARCHAR (65)     NOT NULL,
    [Value]          VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_MatrixConstraint] PRIMARY KEY CLUSTERED ([MatrixId] ASC, [Axis] ASC, [SequenceNumber] ASC),
    CONSTRAINT [FK_MatrixConstraint_Matrix] FOREIGN KEY ([MatrixId]) REFERENCES [dbo].[Matrix] ([MatrixId])
);

