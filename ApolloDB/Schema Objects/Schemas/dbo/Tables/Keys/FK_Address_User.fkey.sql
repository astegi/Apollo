﻿ALTER TABLE [dbo].[Address]
    ADD CONSTRAINT [FK_Address_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([pkID]) ON DELETE NO ACTION ON UPDATE NO ACTION;
