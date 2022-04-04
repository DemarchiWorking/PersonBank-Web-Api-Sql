CREATE TABLE [dbo].[PHYSICAL_PERSON] (
    [ID_PERSON]              INT           IDENTITY (1, 1) NOT NULL,
    [FULL_NAME]              NVARCHAR (90) NOT NULL,
    [BIRTH_DATE]             DATETIME      NOT NULL,
    [CPF]                    NVARCHAR (11) NOT NULL UNIQUE,
    [STREET_ADDRESS]         NVARCHAR (90) NULL,
    [SUBURB]                 NVARCHAR (90) NULL,
    [ZIP_CODE]               NVARCHAR (90) NOT NULL,
    [CITY]                   NVARCHAR (90) NOT NULL,
    [STATE]                  NVARCHAR (90) NOT NULL,
    [ADDITIONAL_INFORMATION] NVARCHAR (90) NULL,
    [MONTHLY_INCOME]         NVARCHAR (90) NULL,
    [CREATED_AT] DATETIME);

    ALTER TABLE PHYSICAL_PERSON
ADD CONSTRAINT PHYSICAL_PERSON_PK PRIMARY KEY (ID_PERSON)


INSERT INTO PHYSICAL_PERSON(
                       FULL_NAME
                       , BIRTH_DATE
                       , CPF
                       , STREET_ADDRESS
                       , SUBURB
                       , ZIP_CODE
                       , CITY
                       , STATE
                       , ADDITIONAL_INFORMATION
                       , MONTHLY_INCOME
                       , CREATED_AT)
                     VALUES
                     (
                        'Andre Aquino'
                        , '2022-01-01T19:38:18.680Z'
                        , '12849989754'
                        , 'RUA Lp'
                        , 'SUBURB '
                        , '26700000'
                        , 'Angra dos Reis'
                        , 'tt'
                        , 'tt'
                        , 8900
                        , (SELECT CURRENT_TIMESTAMP)  
);

SELECT * FROM PHYSICAL_PERSON;

CREATE TABLE [dbo].[PLANS] (
    [ID_PLAN]     INT             IDENTITY (1, 1) NOT NULL,	
    [NAME] NVARCHAR (90)   NULL,
    [DESCRIPTION] NVARCHAR (190)   NULL,
    [PRECO]       FLOAT NULL
);

    ALTER TABLE PLANS
ADD CONSTRAINT PLANS_PK PRIMARY KEY (ID_PLAN)

INSERT INTO PLANS (NAME ,DESCRIPTION, PRECO) VALUES ('VipPlan','O plano vip vai possibilitar o cliente a ter um robô que avisará sobre tendências de investimentos, e sugerir a compra de ações de uma determinada empresa.','50.00');

SELECT * FROM PLANS;

CREATE TABLE [dbo].[USERPLAN]
(
	[ID_USERPLAN]   INT  IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[ID_PERSON] INT NULL, 
    	[ID_PLAN] INT NULL,
	[UPDATED_AT] DATETIME
);

ALTER TABLE USERPLAN
ADD CONSTRAINT FK_PLAN FOREIGN KEY (ID_PLAN) REFERENCES PLANS (ID_PLAN);


ALTER TABLE USERPLAN
ADD CONSTRAINT USERPLAN_FK FOREIGN KEY (ID_PERSON) REFERENCES PHYSICAL_PERSON (ID_PERSON);


