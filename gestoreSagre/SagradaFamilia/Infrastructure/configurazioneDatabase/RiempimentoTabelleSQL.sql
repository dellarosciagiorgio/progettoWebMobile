use GestoreSagre

TRUNCATE TABLE Acquirenti;
INSERT INTO Acquirenti(NomeAcquirente, CognomeAcquirente, MailAcquirente)  
VALUES 
('Marco', 'Neri', 'marco.neri@example.com'),
('Mattia', 'Di Spigno', 'mattia.dispigno@example.com'),
('Giorgio', 'Della Roscia', 'giorgio.dellaroscia@example.com'),
('Daniela', 'Di Lucchio', 'daniela.dilucchio@example.com'),
('Mario', 'Rossi', 'marione94@example.com'),
('Franco', 'Gialli', 'sagraiolo.franchino65@example.com');


TRUNCATE TABLE Organizzatori;
INSERT INTO Organizzatori(NomeOrganizzatore, MailOrganizzatore)  
VALUES 
('Ascoli Eventi SRL', 'sagre@ascolieventi.it'),
('SagraMaster', 'sagra@master.it'),
('Live Tour', 'live.tour@example.com');


TRUNCATE TABLE Admins;
INSERT INTO Admins(NomeAdmin, CognomeAdmin, MailAdmin)  
VALUES 
('Loredana', 'Bruni', 'loredana.bruni@sagradafamilia.com'),
('Gennaro', 'Sagra', 'gennaro.sagra@sagradafamilia.com'),
('Maria', 'Leggiadra', 'maria.leggiadra@sagradafamilia.com');


TRUNCATE TABLE Sagre;
INSERT INTO [dbo].[Sagre]
           ([NomeSagra]
           ,[DescrizioneSagra]
           ,[IdOrganizzatore])
     VALUES
	 ('Sagra della Coratella', 'Nun poi manca, alla sagra della coratella de scapezzà', 2),
	 ('Sagra del Pesce', 'Sagra del pesce fresco di Numana', 1),
	 ('Sagra del Carciofo', 'Carciofi fritti, lessi, gratinati. Insomma, di tutti i tipi', 3);


TRUNCATE TABLE Eventi;
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


TRUNCATE TABLE TipiBiglietto;
INSERT INTO TipiBiglietto(DescrizioneTipoBiglietto, IdEvento)  
VALUES 
('Solo Entrata', 1),
('Mangia a volontà', 1),
('Mangia e bevi a volontà, posto VIP', 1),
('Solo Entrata',2),
('Tavolo davanti al palco', 2),
('Solo Entrata', 3),
('Entrata e Tre birre',3),
('All you can beer, cheers', 3),
('Solo Entrata', 4),
('Mangia a volontà', 4),
('Mangia e bevi a volontà, posto VIP', 4),
('Solo Entrata',5),
('Tavolo davanti al palco', 5),
('Solo Entrata', 6),
('Entrata e Tre birre',6),
('All you can beer, cheers', 6),
('Solo Entrata', 7),
('Mangia a volontà', 7),
('Mangia e bevi a volontà, posto VIP', 7),
('Solo Entrata',8);


TRUNCATE TABLE Stocks;
INSERT INTO Stocks(Quantita, IdTipoBiglietto)  
VALUES
(39, 1),
(34, 2),
(30, 3),
(49, 4),
(47, 5),
(42, 6),
(35, 7),
(47, 8),
(50, 9),
(45, 10),
(33, 11),
(42, 12),
(42, 13),
(37, 14),
(32, 15),
(36, 16),
(46, 17),
(43, 18),
(30, 19),
(42, 20);

TRUNCATE TABLE Biglietti;
INSERT INTO Biglietti(Nominativo, IdAcquirente, IdTipoBiglietto)  
VALUES
('Loredana Bruni', 1, 3),
('Marco Neri', 1, 3),
('Gianmarco Neri', 1, 3),
('Mattia Di Spigno ',2, 5),
('Franco Gialli', 6, 7);