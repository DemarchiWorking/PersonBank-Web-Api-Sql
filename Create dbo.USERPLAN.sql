USE [TargetInvestimento]
GO

/****** Object: Table [dbo].[USERPLAN] Script Date: 30/03/2022 23:31:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[USERPLAN] (
    [ID_PERSON] INT NOT NULL,
    [ID_PLAN]   INT NULL
);


select * from userplan;

select SYSDATETIMEOFFSET();