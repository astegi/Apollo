﻿ALTER TABLE [dbo].[UserDetails]
    ADD CONSTRAINT [FK_UserDetails_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([pkID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

