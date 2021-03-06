﻿CREATE TABLE [dbo].[Matrix] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Name]               VARCHAR (160)    NOT NULL,
    [RowArgumentId]    UNIQUEIDENTIFIER    NOT NULL,
    [ColumnArgumentId] UNIQUEIDENTIFIER    NULL,
    [DataTypeName]         VARCHAR (65)     CONSTRAINT [DF_Matrix_ResultType] DEFAULT ('Text') NOT NULL,
    CONSTRAINT [PK_Matrix] PRIMARY KEY NONCLUSTERED ([Id] ASC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Matrix]
    ON [dbo].[Matrix]([Name] ASC);

