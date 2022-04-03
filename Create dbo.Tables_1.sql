USE [TargetInvestimento]
GO

/****** Object: Table [dbo].[Tables] Script Date: 30/03/2022 23:42:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tables] (
    [Id]   INT      NOT NULL,
    [Data] DATETIME NULL
);


insert into Tablee(Id, Data) values (1, 'select SYSDATETIMEOFFSET()');


