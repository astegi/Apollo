﻿ALTER TABLE [dbo].[Store]
    ADD CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED ([pkID] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);

