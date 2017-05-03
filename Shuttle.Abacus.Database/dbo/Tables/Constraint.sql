CREATE TABLE [dbo].[Constraint] (
    [OwnerName]      VARCHAR (100)    NOT NULL,
    [OwnerId]        UNIQUEIDENTIFIER NOT NULL,
    [ArgumentId]     UNIQUEIDENTIFIER NOT NULL,
    [ArgumentName]   VARCHAR (100)    NOT NULL,
    [Name]           VARCHAR (60)     NOT NULL,
    [Answer]         VARCHAR (120)    NOT NULL,
    [AnswerType]     VARCHAR (100)    NOT NULL,
    [Description]    VARCHAR (250)    NOT NULL,
    [SequenceNumber] INT              NOT NULL,
    CONSTRAINT [FK_Constraint_Argument] FOREIGN KEY ([ArgumentId]) REFERENCES [dbo].[Argument] ([ArgumentId])
);

