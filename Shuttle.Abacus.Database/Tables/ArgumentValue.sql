CREATE TABLE [dbo].[ArgumentValue] (
    [ArgumentId] UNIQUEIDENTIFIER NOT NULL,
    [Value]      VARCHAR (120)    NOT NULL,
    CONSTRAINT [PK_ArgumentValue] PRIMARY KEY CLUSTERED ([ArgumentId] ASC, [Value] ASC),
    CONSTRAINT [FK_ArgumentRestrictedAnswer_Argument] FOREIGN KEY ([ArgumentId]) REFERENCES [dbo].[Argument] ([Id]) ON DELETE CASCADE
);

