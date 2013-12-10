ALTER TABLE [dbo].[Address]
    ADD CONSTRAINT [FK_Address_Country] FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Country] ([pkID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

