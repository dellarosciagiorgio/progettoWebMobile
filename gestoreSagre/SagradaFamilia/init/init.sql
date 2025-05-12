USE master;

GO
-- Controlla se il database esiste già
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'GestoreSagre')
BEGIN
    PRINT 'Database already exists. Exiting...';
    THROW 51000, 'Database already exists. Initialization aborted.', 1;
END
ELSE
BEGIN
    PRINT 'Creating database...';
    CREATE DATABASE GestoreSagre;
END

GO
USE GestoreSagre;

GO
-- ELIMINAZIONE TABELLE
DECLARE @sql NVARCHAR(MAX) = '';

-- Generare comandi per eliminare tutti i vincoli di foreign key
SELECT @sql += 'ALTER TABLE ' + QUOTENAME(OBJECT_NAME(parent_object_id)) 
               + ' DROP CONSTRAINT ' + QUOTENAME(name) + ';'
FROM sys.foreign_keys;

-- Eliminare i vincoli
EXEC sp_executesql @sql;

-- Generare ed eseguire il comando per eliminare tutte le tabelle
SET @sql = '';

SELECT @sql += 'DROP TABLE IF EXISTS ' + QUOTENAME(name) + ';'
FROM sys.tables;

EXEC sp_executesql @sql;


-- CREAZIONE TABELLE
GO
use GestoreSagre;

GO
CREATE TABLE Users(
	IdUser INT identity (1,1),
	Mail VARCHAR(100) not null,
	Pass VARCHAR(100) not null,
	Ruolo VARCHAR(15) not null,
	CONSTRAINT [PK_Users] primary key clustered
	(
		IdUser ASC
	)
)

-- ACQUIRENTI
Go
CREATE TABLE Acquirenti(
	IdAcquirente INT identity (1,1),
	NomeAcquirente VARCHAR(100) not null,
	CognomeAcquirente VARCHAR(100) not null,
	IdUser int not null,
	CONSTRAINT [PK_Acquirenti] primary key clustered
	(
		IdAcquirente ASC
	)
)
GO

CREATE TABLE Admins(
    IdAdmin INT identity (1,1),
	NomeAdmin VARCHAR(100) not null,
	CognomeAdmin VARCHAR(100) not null,
	IdUser int not null,
	COnSTRAINT [PK_Admins] primary key clustered
	(
		IdAdmin ASC
	)
)

-- Eventi

CREATE TABLE Eventi(
    IdEvento INT identity (1,1),
	InformazioniAggiuntive VARCHAR(100) not null,
	DataEvento DATE not null,
	IdSagra int not null,

	CONSTRAINT [PK_Eventi] primary key clustered
	(
		IdEvento ASC
	)
)

-- Feedbacks

CREATE TABLE Feedbacks(
    IdFeedback INT identity (1,1),
	IdAcquirente int,
	Titolo VARCHAR(100) not null,
	Descrizione VARCHAR(1000) not null,
	Rating int not null,
	IdSagra int not null,
	CONSTRAINT [PK_Feedbacks] primary key clustered
	(
		IdFeedback ASC
	)
)

-- Organizzatori

Go
CREATE TABLE Organizzatori(
	IdOrganizzatore INT identity (1,1),
	NomeOrganizzatore VARCHAR(100) not null,
	IdUser int not null,
	CONSTRAINT [PK_Organizzatori] primary key clustered
	(
		IdOrganizzatore ASC
	)
)

-- Sagra

Go
CREATE TABLE Sagre(
	IdSagra INT identity (1,1),
	NomeSagra VARCHAR(100) not null,
	DescrizioneSagra VARCHAR(1000) not null,
	Luogo VARCHAR(100) not null,
	IdOrganizzatore INT,
	CONSTRAINT [PK_Sagre] primary key clustered
	(
		IdSagra ASC
	)
)

-- Stock

Go
CREATE TABLE Stocks(
	IdStock INT identity (1,1),
	IdTipoBiglietto INT ,
	Quantita INT ,
	IdEvento INT ,
	CONSTRAINT [PK_Stock] primary key clustered
	(
		IdStock ASC
	)
)

-- TipoBiglietto

Go
CREATE TABLE TipiBiglietto(
	IdTipoBiglietto INT identity (1,1),
	DescrizioneTipoBiglietto VARCHAR(1000) not null,
	NomeTipoBiglietto varchar(100) not null,
	Prezzo FLOAT not null,
	CONSTRAINT [PK_TipiBiglietto] primary key clustered
	(
		IdTipoBiglietto ASC
	)
)

-- biglietti
GO

CREATE TABLE Biglietti(
    IdBiglietto INT identity (1,1),
	Nominativo VARCHAR(100) not null,
	IdAcquirente INT not null,
	IdTipoBiglietto INT,
	CONSTRAINT [PK_Biglietti] primary key clustered
	(
		IdBiglietto ASC
	)
)

GO
Alter table Biglietti
add constraint FK_Biglietti_Acquirenti foreign key (IdAcquirente)
references Acquirenti (IdAcquirente)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

GO
Alter table Biglietti
add constraint FK_Biglietti_TipoBiglietto foreign key (IdTipoBiglietto)
references TipiBiglietto (IdTipoBiglietto)
    ON DELETE SET NULL
    ON UPDATE CASCADE;

--GO
--Alter table TipiBiglietto
--add constraint FK_TipiBiglietto_Eventi foreign key (IdEvento)
--references Eventi (IdEvento)
--    ON DELETE CASCADE
--    ON UPDATE CASCADE;

GO
alter table Eventi
add constraint FK_Eventi_Sagre foreign key (IdSagra)
references Sagre (IdSagra)
    ON DELETE CASCADE
    ON UPDATE CASCADE;


GO
alter table Feedbacks
add constraint FK_Feedbacks_Acquirenti foreign key (IdAcquirente)
references Acquirenti (IdAcquirente)
  ON DELETE SET NULL
  ON UPDATE CASCADE;

GO
alter table Feedbacks
add constraint FK_Feedbacks_Sagre foreign key (IdSagra)
references Sagre (IdSagra)
  ON DELETE CASCADE
  ON UPDATE CASCADE;

GO
alter table Sagre
add constraint FK_Sagre_Organizzatori foreign key (IdOrganizzatore)
references Organizzatori (IdOrganizzatore)
  ON DELETE SET NULL
  ON UPDATE CASCADE;


GO
alter table Stocks
add constraint FK_Stocks_TipoBiglietto foreign key (IdTipoBiglietto)
references TipiBiglietto (IdTipoBiglietto)
  ON DELETE CASCADE
  ON UPDATE CASCADE;
 
GO
Alter table Stocks
add constraint FK_Stocks_Eventi foreign key (IdEvento)
references Eventi (IdEvento)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

GO
Alter table Acquirenti
add constraint FK_Acquirenti_Users foreign key (IdUser)
references Users (IdUser)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

GO
Alter table Admins
add constraint FK_Admins_Users foreign key (IdUser)
references Users (IdUser)
    ON DELETE CASCADE
    ON UPDATE CASCADE;

GO
Alter table Organizzatori
add constraint FK_Organizzatori_Users foreign key (IdUser)
references Users (IdUser)
    ON DELETE no action
    ON UPDATE no action;
	
-- INSERIMENTO DATI


use GestoreSagre;
-- TRUNCATE TABLE Acquirenti;

INSERT INTO Users(Ruolo, Pass, Mail)
VALUES
('Acquirente', '$2a$11$2Y72Bw9a4DFHQYQ0WAhC/ehml07O3DgHLscymeuRxFoxOglDg.l0K', 'marco.neri@example.com'),
('Acquirente', '$2a$11$MJoUQt/QSnHUNObuM0CgKePTVqC3gY7eza1EX4SLlaOJTfOTDbeHy', 'mattia.dispigno@example.com'),
('Acquirente', '$2a$11$xIiIcLpDzpartxufJoGnteLatFx/4SXVtGa9qFXtVb9rbQHl9TdRO', 'giorgio.dellaroscia@example.com'),
('Acquirente', '$2a$11$8kmlkQO6hk5GuxrWKAMTc.ncwAomtCiGKO.XVCkfsfbSDdPPqTNJ.', 'daniela.dilucchio@example.com'),
('Acquirente', '$2a$11$Cpx8CEPruYhEQqPfMEhsce0URf5nNu2X7u1/q9cp98yvUhNwSpzSy', 'marione94@example.com'),
('Acquirente', '$2a$11$6.cugJObK/UX4kOaU0Zjc.h.UzGSLn7fWZNrasEfM/w.ESa.iAztG', 'sagraiolo.franchino65@example.com'),
('Organizzatore', '$2a$11$oPoxwW8t1NlsG7uPfJH6p.nk.uwumhjOwaAqL2Kh4P7qlgkb5pvzG',  'sagre@ascolieventi.it'),
('Organizzatore',  '$2a$11$pAhkutuM41ij1aN/.KHRp.fDNfMLUtdUsl.KkW40MwbMY5HeLtJei', 'sagra@master.it'),
('Organizzatore',  '$2a$11$bOicXirRB56EoSYpJtnL0eGjkFms7/RsjOno/3ciZu4CUBYZf/PhS', 'live.tour@example.com'),
('Admin', '$2a$11$brRdqj/wjK86ugArPwmFgeuHQKPoTqBiPo.4RCbp9gzu3GhdUdh6W', 'loredana.bruni@sagradafamilia.com'),
('Admin', '$2a$11$MNsSPcPWKejuRN/WQX2bhOrMKTTQ8bDHVJRczKRRaxU8mqSAGApba', 'gennaro.sagra@sagradafamilia.com'),
('Admin', '$2a$11$RYpAJ1kMyBCxkhKbl/D7rOm4h8xZbackAsVFzCZRETeTHUt0vOJZq', 'maria.leggiadra@sagradafamilia.com');

INSERT INTO Acquirenti(NomeAcquirente, CognomeAcquirente, IdUser)  
VALUES 
('Marco', 'Neri', 1),
('Mattia', 'Di Spigno', 2),
('Giorgio', 'Della Roscia', 3),
('Daniela', 'Di Lucchio', 4),
('Mario', 'Rossi', 5),
('Franco', 'Gialli', 6);


-- TRUNCATE TABLE Organizzatori;
INSERT INTO Organizzatori(NomeOrganizzatore, IdUser)  
VALUES 
('Ascoli Eventi SRL', 7),
('SagraMaster', 8),
('Live Tour', 9);


-- TRUNCATE TABLE Admins;
INSERT INTO Admins(NomeAdmin, CognomeAdmin, IdUser)  
VALUES 
('Loredana', 'Bruni', 10),
('Gennaro', 'Sagra', 11),
('Maria', 'Leggiadra', 12);


-- TRUNCATE TABLE Sagre;
INSERT INTO [dbo].[Sagre]
           ([NomeSagra]
           ,[DescrizioneSagra]
		   ,[Luogo]
           ,[IdOrganizzatore])
     VALUES
	 ('Sagra della Coratella', 'Nun poi manca, alla sagra della coratella de scapezzà', 'Scapezzano', 2),
	 ('Sagra del Pesce', 'Sagra del pesce fresco di Numana', 'Numana', 1),
	 ('Sagra del Carciofo', 'Carciofi fritti, lessi, gratinati. Insomma, di tutti i tipi', 'Borgo Passera', 3);


-- TRUNCATE TABLE Eventi;
INSERT INTO Eventi(DataEvento, InformazioniAggiuntive,IdSagra)  
VALUES 
('2025-03-13', 'Si mangia e si beve, alla festa di paese. Dj set dalle 23 con Pippo DJ', 1),
('2025-03-14', 'Mangia a volontà, la polenta di pesce ti sazierà. Live band country dalle 22, Yeehaw!', 1),
('2025-03-16', 'Festa finale di chiusura, non mancare. Live band dalla cena con Dj dalle 23 fino a tarda notte!', 1),
('2025-04-13', 'Si mangia e si beve, alla festa di paese. Dj set dalle 23 con Pippo DJ', 2),
('2025-04-14', 'Mangia a volontà, la polenta alla coratella ti sazierà. Live band country dalle 22, Yeehaw!', 2),
('2025-04-16', 'Festa finale di chiusura, non mancare. Live band dalla cena con Dj dalle 23 fino a tarda notte!', 2),
('2025-05-23', 'Si mangia e si beve, alla festa di paese. Dj set dalle 23 con Pippo DJ', 3),
('2025-05-24', 'Mangia a volontà, la polenta di carciofi ti sazierà. Live band country dalle 22, Yeehaw!', 3),
('2025-05-26', 'Festa finale di chiusura, non mancare. Live band dalla cena con Dj dalle 23 fino a tarda notte!', 3);


-- TRUNCATE TABLE TipiBiglietto;
INSERT INTO TipiBiglietto(NomeTipoBiglietto, DescrizioneTipoBiglietto, Prezzo)  
VALUES 
('Entrata','Solo Entrata',5),
('Mangia','Mangia a volontà', 20),
('All-inclusive', 'Mangia e bevi a volontà, posto VIP',35),
('Entrata','Solo Entrata',4),
('Tavolo Vip','Tavolo davanti al palco',70),
('Entrata','Solo Entrata',2),
('3Beer','Entrata e Tre birre', 10),
('Bevi', 'All you can beer, cheers', 20),
('Entrata', 'Solo Entrata',12),
('Mangia', 'Mangia a volontà',27),
('All-inclusive','Mangia e bevi a volontà, posto VIP',40),
('Entrata', 'Solo Entrata',20);


-- TRUNCATE TABLE Stocks;
INSERT INTO Stocks(Quantita, IdTipoBiglietto, IdEvento)  
VALUES
(39, 1, 1),
(34, 2,1),
(30, 3,1),
(49, 4,2),
(47, 5,2),
(42, 6,3),
(35, 7, 3),
(47, 8, 3),
(50, 9, 4),
(45, 10, 4),
(33, 11, 4),
(42, 12, 5),
(42, 1, 5),
(37, 2, 6),
(32, 3, 6),
(36, 4, 6),
(46, 5, 7),
(43, 6, 7),
(30, 7, 7),
(42, 8, 8);

-- TRUNCATE TABLE Biglietti;
use GestoreSagre;
INSERT INTO Biglietti(Nominativo, IdAcquirente, IdTipoBiglietto)  
VALUES
('Loredana Bruni', 1, 3),
('Marco Neri', 1, 3),
('Gianmarco Neri', 1, 3),
('Mattia Di Spigno ',2, 5),
('Franco Gialli', 6, 7),
('Giorgio Della Roscia', 3, 8),
('Daniela Di Lucchio', 4, 9),
('Mario Rossi', 5, 10),
('Mauro Bianchi', 6, 11),
('Marco Neri', 1, 12),
('Mattia Di Spigno ',2, 10),
('Pierpaolo Della Roscia', 3, 8),
('Franco Gilli', 4, 6),
('Rossana Rossi', 5, 8);

INSERT INTO Feedbacks(Titolo, Descrizione, Rating, IdAcquirente, IdSagra)
VALUES
('Bellissima sagra', 'La sagra della coratella è stata fantastica! Cibo delizioso e atmosfera coinvolgente.', 10, 1, 1),
('Ottimo cibo', 'Il pesce fresco era eccezionale. Tornerò sicuramente!', 7, 2, 2),
('Da migliorare', 'La sagra del carciofo era buona, ma il servizio era lento.', 7, 3, 3),
('Fantastica esperienza', 'Ho adorato la musica dal vivo e il cibo. Un evento da non perdere!', 9, 4, 3),
('Soddisfatto', 'Tutto perfetto, ci tornerò sicuramente!', 8, 4, 2);