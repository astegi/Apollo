CREATE TABLE [dbo].[Address] (
    [pkID]          INT            IDENTITY (1, 1) NOT NULL,
    [UserID]        INT            NOT NULL,
    [CountryID]     INT            NULL,
    [City]          NVARCHAR (50)  NULL,
    [Address]       NVARCHAR (150) NULL,
    [ZipCode]       NVARCHAR (10)  NULL,
    [AddressTypeID] INT            NULL
);

