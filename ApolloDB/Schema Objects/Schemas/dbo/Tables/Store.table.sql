CREATE TABLE [dbo].[Store] (
    [pkID]       INT            IDENTITY (1, 1) NOT NULL,
    [FileName]   NVARCHAR (255) NOT NULL,
    [FileType]   NVARCHAR (50)  NOT NULL,
    [FileLength] BIGINT         NOT NULL,
    [InsDate]    DATETIME       NOT NULL
);

