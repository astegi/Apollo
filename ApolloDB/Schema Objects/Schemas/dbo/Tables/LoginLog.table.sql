CREATE TABLE [dbo].[LoginLog] (
    [pkID]      INT      IDENTITY (10000, 1) NOT NULL,
    [UserID]    INT      NOT NULL,
    [LoginDate] DATETIME NOT NULL
);

