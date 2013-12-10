CREATE TABLE [dbo].[Account] (
    [pkID]          INT            IDENTITY (1, 1) NOT NULL,
    [UserID]        INT            NOT NULL,
    [AccountTypeID] INT            NOT NULL,
    [AccountValue]  NVARCHAR (100) NOT NULL
);

