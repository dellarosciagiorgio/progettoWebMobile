USE GestoreSagre
-- TRUNCATE TABLE Acquirenti;
INSERT INTO Acquirenti(NomeAcquirente, CognomeAcquirente, MailAcquirente)  
VALUES 
('Marco', 'Neri', 'marco.neri@example.com'),
('Mattia', 'Di Spigno', 'mattia.dispigno@example.com'),
('Giorgio', 'Della Roscia', 'giorgio.dellaroscia@example.com'),
('Daniela', 'Di Lucchio', 'daniela.dilucchio@example.com'),
('Mario', 'Rossi', 'marione94@example.com'),
('Franco', 'Gialli', 'sagraiolo.franchino65@example.com');


-- TRUNCATE TABLE Organizzatori;
INSERT INTO Organizzatori(NomeOrganizzatore, MailOrganizzatore)  
VALUES 
('Ascoli Eventi SRL', 'sagre@ascolieventi.it'),
('SagraMaster', 'sagra@master.it'),
('Live Tour', 'live.tour@example.com');


-- TRUNCATE TABLE Admins;
INSERT INTO Admins(NomeAdmin, CognomeAdmin, MailAdmin)  
VALUES 
('Loredana', 'Bruni', 'loredana.bruni@sagradafamilia.com'),
('Gennaro', 'Sagra', 'gennaro.sagra@sagradafamilia.com'),
('Maria', 'Leggiadra', 'maria.leggiadra@sagradafamilia.com');


-- TRUNCATE TABLE Sagre;
INSERT INTO [dbo].[Sagre]
           ([NomeSagra]
           ,[DescrizioneSagra]
           ,[IdOrganizzatore])
     VALUES
	 ('Sagra della Coratella', 'Nun poi manca, alla sagra della coratella de scapezzà', 2),
	 ('Sagra del Pesce', 'Sagra del pesce fresco di Numana', 1),
	 ('Sagra del Carciofo', 'Carciofi fritti, lessi, gratinati. Insomma, di tutti i tipi', 3);


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
('Entrata', 'Solo Entrata', 10),
('Mangia','Mangia a volontà', 20),
('All-inclusive','Mangia e bevi a volontà, posto VIP', 30),
('Entrata','Solo Entrata', 35),
('Tavolo','Tavolo davanti al palco', 50),
('Entrata','Solo Entrata', 5),
('3Beer','Entrata e Tre birre', 15),
('Bevii','All you can beer, cheers', 25),
('Entrata','Solo Entrata',5),
('Mangia','Mangia a volontà', 20),
('All-inclusive', 'Mangia e bevi a volontà, posto VIP',35),
('Entrata','Solo Entrata',40),
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
(34, 2, 1),
(30, 3, 1),
(49, 4, 2),
(47, 5, 2),
(42, 6, 3),
(35, 7, 3),
(47, 8, 4),
(50, 9, 4),
(45, 10, 5),
(33, 11, 5),
(42, 12, 6),
(42, 13, 6),
(37, 14, 7),
(32, 15, 7),
(36, 16, 7),
(46, 17, 8),
(43, 18, 8),
(30, 19, 8),
(42, 20, 9);

-- TRUNCATE TABLE Biglietti;
INSERT INTO Biglietti(Nominativo, IdAcquirente, IdTipoBiglietto)  
VALUES
('Loredana Bruni', 1, 3),
('Marco Neri', 1, 3),
('Gianmarco Neri', 1, 3),
('Mattia Di Spigno ',2, 5),
('Franco Gialli', 6, 7);