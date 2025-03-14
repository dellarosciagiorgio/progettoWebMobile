IF EXISTS(SELECT 1 FROM sys.objects where name = 'Acquirenti')
begin
	drop table Acquirenti
end
Go
CREATE TABLE Acquirenti(
	IdAcquirente INT identity (1,1),
	NomeAcquirente VARCHAR(100) not null,
	CognomeAcquirente VARCHAR(100) not null,
	MailAcquirente VARCHAR(100) not null,
	CONSTRAINT [PK_Acquirenti] primary key clustered
	(
		IdAcquirente ASC
	)
)
GO
IF EXISTS(SELECT 1 FROM sys.objects where name = 'Admins')
begin
	drop table Admins
end
CREATE TABLE Admins(
    IdAdmin INT identity (1,1),
	NomeAdmin VARCHAR(100) not null,
	CognomeAdmin VARCHAR(100) not null,
	MailAdmin VARCHAR(100) not null,
	COnSTRAINT [PK_Admins] primary key clustered
	(
		IdAdmin ASC
	)
)

-- biglietti
GO
IF EXISTS(SELECT 1 FROM sys.objects where name = 'Biglietti')
begin
	drop table Biglietti
end
CREATE TABLE Biglietti(
    IdBiglietto INT identity (1,1),
	Nominativo VARCHAR(100) not null,
	IdAcquirente INT not null,
	IdTipoBiglietto INT not null,
	CONSTRAINT [PK_Biglietti] primary key clustered
	(
		IdBiglietto ASC
	)
	
)

-- Eventi
IF EXISTS(SELECT 1 FROM sys.objects where name = 'Eventi')
begin
	drop table Eventi
end
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
IF EXISTS(SELECT 1 FROM sys.objects where name = 'Feedbacks')
begin
	drop table Feedbacks
end
CREATE TABLE Feedbacks(
    IdFeedback INT identity (1,1),
	IdAcquirente int not null,
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
IF EXISTS(SELECT 1 FROM sys.objects where name = 'Organizzatori')
begin
	drop table Organizzatori
end
Go
CREATE TABLE Organizzatori(
	IdOrganizzatore INT identity (1,1),
	NomeOrganizzatore VARCHAR(100) not null,
	MailOrganizzatore VARCHAR(100) not null,
	CONSTRAINT [PK_Organizzatori] primary key clustered
	(
		IdOrganizzatore ASC
	)
)

-- Sagra
IF EXISTS(SELECT 1 FROM sys.objects where name = 'Sagre')
begin
	drop table Sagre
end
Go
CREATE TABLE Sagre(
	IdSagra INT identity (1,1),
	NomeSagra VARCHAR(100) not null,
	DescrizioneSagra VARCHAR(1000) not null,
	IdOrganizzatore INT not null,
	CONSTRAINT [PK_Sagre] primary key clustered
	(
		IdSagra ASC
	)
)

-- Stock
IF EXISTS(SELECT 1 FROM sys.objects where name = 'Stocks')
begin
	drop table Stocks
end
Go
CREATE TABLE Stocks(
	IdStock INT identity (1,1),
	IdTipoBiglietto INT not null,
	Quantita INT not null,
	IdEvento INT not null,
	CONSTRAINT [PK_Stock] primary key clustered
	(
		IdStock ASC
	)
)

-- TipoBiglietto
IF EXISTS(SELECT 1 FROM sys.objects where name = 'TipiBiglietto')
begin
	drop table TipiBiglietto
end
Go
CREATE TABLE TipiBiglietto(
	IdTipoBiglietto INT identity (1,1),
	DescrizioneTipoBiglietto VARCHAR(1000) not null,
	IdEvento INT not null,
	CONSTRAINT [PK_TipiBiglietto] primary key clustered
	(
		IdTipoBiglietto ASC
	)
)

GO
Alter table Biglietti
add constraint FK_Biglietti_Acquirenti foreign key (IdAcquirente)
references Acquirenti (IdAcquirente);

GO
Alter table Biglietti
add constraint FK_Biglietti_TipoBiglietto foreign key (IdTipoBiglietto)
references TipiBiglietto (IdTipoBiglietto);

GO
alter table Eventi
add constraint FK_Eventi_Sagre foreign key (IdSagra)
references Sagre (IdSagra);


GO
alter table Feedbacks
add constraint FK_Feedbacks_Acquirenti foreign key (IdAcquirente)
references Acquirenti (IdAcquirente);

GO
alter table Feedbacks
add constraint FK_Feedbacks_Sagre foreign key (IdSagra)
references Sagre (IdSagra);

GO
alter table Sagre
add constraint FK_Sagre_Organizzatori foreign key (IdOrganizzatore)
references Organizzatori (IdOrganizzatore);

GO
alter table TipiBiglietto
add constraint FK_TipoBiglietto_Eventi foreign key (IdEvento)
references Eventi (IdEvento);

GO
alter table Stocks
add constraint FK_Stocks_TipoBiglietto foreign key (IdTipoBiglietto)
references TipiBiglietto (IdTipoBiglietto);
