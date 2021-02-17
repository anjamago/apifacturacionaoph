/**
	AUTOR: Andy Martinez
	DATE: 15/02/2021
**/

CREATE DATABASE OPHELIATEST;
USE OPHELIATEST;

CREATE TABLE CATEGORIES(
	ID INT IDENTITY(1,1) NOT NULL,
	CATEGORY VARCHAR(150) NOT NULL,
	CREATE_AT DATETIME DEFAULT GETDATE(),
	UPDATE_AT DATETIME DEFAULT GETDATE(),

	CONSTRAINT PK_CATEGORIES PRIMARY KEY (
		ID ASC
	),
	CONSTRAINT UQ_CATEGORIES UNIQUE(
		CATEGORY
	)
);

CREATE TABLE PRODUCTS(
	ID INT IDENTITY(1,1) NOT NULL,
	PRODUCT VARCHAR(250) NOT NULL,
	CUANTITY INT NOT NULL,
	PRINCE FLOAT NOT NULL,
	ID_CATEGORY INT NOT NULL,
	CREATE_AT DATETIME DEFAULT GETDATE(),
	UPDATE_AT DATETIME DEFAULT GETDATE(),

	CONSTRAINT PK_PRODUCT PRIMARY KEY (
		ID ASC
	),
	CONSTRAINT CK_NEGATIVE_PRINCE CHECK(
		PRINCE >= 0
	),
	CONSTRAINT CK_CUANTITY CHECK(
		CUANTITY > 0
	),
	CONSTRAINT FK_PRODUCT_CATEGORIE FOREIGN KEY (ID_CATEGORY)
		REFERENCES CATEGORIES(ID)
);

CREATE TABLE SELLERS (
	ID INT IDENTITY(1,1) NOT NULL,
	SELLER_CODE VARCHAR(60) NOT NULL,
	SELLER_NAME VARCHAR(250) NOT NULL,
	CREATE_AT DATETIME DEFAULT GETDATE(),
	UPDATE_AT DATETIME DEFAULT GETDATE(),

	CONSTRAINT PK_SELLER PRIMARY KEY (
		ID ASC
	),
	CONSTRAINT UQ_SELLER_CODE UNIQUE(
		SELLER_CODE
	)
);

CREATE TABLE CLIENTS (
	ID INT IDENTITY(1,1) NOT NULL,
	NAME VARCHAR(150) NOT NULL,
	LAST_NAME VARCHAR(150) NOT NULL,
	ADDRESS VARCHAR(250) NOT NULL,
	EMAIL VARCHAR(150),
	BIRTHDAY DATETIME NOT NULL,
	IDENTIFICATION_NUMBER BIGINT NOT NULL,
	CREATE_AT DATETIME DEFAULT GETDATE(),
	UPDATE_AT DATETIME DEFAULT GETDATE(),

	CONSTRAINT PK_CLIENT PRIMARY KEY(
		ID ASC
	),
	CONSTRAINT UQ_CLIENT_UNIQUE UNIQUE(
		EMAIL,IDENTIFICATION_NUMBER
	),

);

CREATE TABLE INVOICES (
	ID UNIQUEIDENTIFIER NOT NULL CONSTRAINT DK_INVOICE_ID DEFAULT NEWID(),
	INVOICE_DATE DATETIME NOT NULL,
	SELLER_ID INT NOT NULL,
	CLIENT_ID INT NOT NULL,
	CREATE_AT DATETIME DEFAULT GETDATE(),
	UPDATE_AT DATETIME DEFAULT GETDATE(),

	CONSTRAINT PK_INVOICES PRIMARY KEY(
		ID ASC
	),

	CONSTRAINT FK_SELLER_INVOICE FOREIGN KEY (SELLER_ID)
		REFERENCES SELLERS(ID),

	CONSTRAINT FK_CLIENT_INVOICE FOREIGN KEY (CLIENT_ID)
		REFERENCES CLIENTS(ID),
);

CREATE TABLE INVOICE_DETAIL (
	ID INT IDENTITY(1,1) NOT NULL,
	ID_INVOICE UNIQUEIDENTIFIER NOT NULL,
	ID_PRODUCT INT NOT NULL,
	CUANTITY INT NOT NULL,
	PRINCE_PRODUCT FLOAT NOT NULL,
	PRINCE_INVOICE FLOAT NOT NULL,
	CREATE_AT DATETIME DEFAULT GETDATE(),
	UPDATE_AT DATETIME DEFAULT GETDATE(),

	CONSTRAINT PK_INVOICE_DETAIL PRIMARY KEY(
		ID ASC
	),
	CONSTRAINT CK_NEGATIVE_PRINCE_INVOICE_DETAILD CHECK(
		PRINCE_PRODUCT >= 0 AND PRINCE_INVOICE >=0
	),

	CONSTRAINT FK_INVOICE_INVOICEDETAIL FOREIGN KEY (ID_INVOICE)
		REFERENCES INVOICES(ID),

	CONSTRAINT FK_PRODUCT_INVOICEDETAIL FOREIGN KEY (ID_PRODUCT)
		REFERENCES PRODUCTS(ID),

);


--------------insert-----------------------------

INSERT INTO [OPHELIATEST].[dbo].[CLIENTS] ([NAME],[LAST_NAME],[ADDRESS],[EMAIL],[IDENTIFICATION_NUMBER],[BIRTHDAY]) VALUES
('Leanne','Graham','Gwenborough','Sincere@april.biz',92998,'1987-12-07'),
('Ervin ','Howell ','Wisokyburgh','Shanna@melissa.tv',9299348,'1997-02-07'),
('Clementine ','Bauch','McKenziehaven','Nathan@yesenia.net',9278998,'1993-05-30'),
('Clementine ','Bauch','McKenziehaven','Nathan1@yesenia.net',927892398,'1880-05-30');


INSERT INTO [OPHELIATEST].[dbo].[SELLERS] ([SELLER_CODE],[SELLER_NAME]) VALUES
('Mrs. Dennis Schulist','935-8478'),
('Nicholas Runolfsdottir','493.6943'),
('Glenna Reichert','6794x41206');


INSERT INTO [OPHELIATEST].[dbo].[CATEGORIES] ([CATEGORY]) VALUES
('Vehiculos'),
('Tecnologia'),
('Servicios'),
('Electrodomesticos');



INSERT INTO [OPHELIATEST].[dbo].[PRODUCTS] ([PRODUCT],[CUANTITY],[PRINCE],[ID_CATEGORY]) VALUES
('MASDA',30,48.000,1),
('BMW',10,98.000,1),
('MERCEDES-BENZ',10,78.000,1),

('Asus',100,3.000,2),
('Iphon',5,5.000,2),
('Hp',5,1.000,2),

('Nevera',12,2.500,4),
('TV',4,2.500,4);



select * from [OPHELIATEST].[dbo].[INVOICES] 
where INVOICE_DATE='2000-05-12' and CLIENT_ID=1 ;

insert into [OPHELIATEST].[dbo].[INVOICES] (INVOICE_DATE,SELLER_ID,CLIENT_ID)
VALUES
('2000-02-12',1,1),
('2000-03-22',1,1),
('2000-04-05',1,1),
('2000-04-07',1,1),
('2000-05-12',1,1),

('2000-02-12',2,2),
('2000-02-12',1,3),
('2000-03-12',3,2),
('2000-06-12',1,3),
('2000-06-12',1,2),
('2000-06-12',1,3);

INSERT INTO [OPHELIATEST].[dbo].[INVOICE_DETAIL]
 ([ID_INVOICE],[ID_PRODUCT],[CUANTITY],[PRINCE_PRODUCT],[PRINCE_INVOICE])
VALUES('74774796-2f53-4f57-867a-b4d8a5f6513f',3,2,200.000,120.000);


Select DISTINCT c.*
from INVOICES i
INNER JOIN CLIENTS c on i.CLIENT_ID = c.ID
where DATEDIFF(YEAR,c.BIRTHDAY,GETDATE()) < 35

SELECT id.ID_PRODUCT, SUM(id.PRINCE_INVOICE) as totalInvoice from 
INVOICES as inv
INNER JOIN INVOICE_DETAIL id on inv.ID = id.ID_INVOICE
where YEAR(inv.INVOICE_DATE) = 2000
GROUP BY id.ID_PRODUCT;
