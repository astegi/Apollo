CREATE TABLE [dbo].[User] (
    [pkID]         INT            IDENTITY (1, 1) NOT NULL,
    [UserName]     NVARCHAR (50)  NOT NULL,
    [Password]     NVARCHAR (512) NOT NULL,
    [CreationTime] DATETIME       NOT NULL,
    [IsActive]     BIT            NOT NULL
);

