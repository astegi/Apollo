CREATE TABLE [dbo].[MessageType] (
    [pkID]         INT            IDENTITY (1, 1) NOT NULL,
    [MessageCode]  INT            NOT NULL,
    [Severity]     NVARCHAR (50)  NOT NULL,
    [Facility]     NVARCHAR (50)  NOT NULL,
    [Description]  TEXT           NOT NULL,
    [Language]     NVARCHAR (50)  NOT NULL,
    [SymbolicName] NVARCHAR (100) NOT NULL
);

