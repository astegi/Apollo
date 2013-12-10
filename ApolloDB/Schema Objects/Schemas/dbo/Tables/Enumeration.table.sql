CREATE TABLE [dbo].[Enumeration] (
    [pkID]         INT            IDENTITY (1, 1) NOT NULL,
    [enumName]     NVARCHAR (100) NOT NULL,
    [enumCategory] NVARCHAR (100) NOT NULL,
    [enumValue]    INT            NOT NULL,
    [IsActive]     BIT            DEFAULT ((1)) NULL
);

