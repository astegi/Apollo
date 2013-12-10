CREATE TABLE [dbo].[Country] (
    [pkID]         INT            IDENTITY (1, 1) NOT NULL,
    [CountryCode]  NVARCHAR (3)   NULL,
    [CurrencyCode] NVARCHAR (3)   NULL,
    [CountryName]  NVARCHAR (150) NOT NULL
);

