CREATE TABLE [dbo].[Note] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    [CreateDate]  DATETIME2 (7)  NOT NULL,
    [Status]      NVARCHAR (MAX) NOT NULL,
    [History]     NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED ([Id] ASC)
);

