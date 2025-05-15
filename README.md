# Progetto Web e Mobile

A.A. 2024/25

Giorgio Della Roscia, Daniela Di Lucchio e Mattia Di Spigno

## Contesto

<div align='center' width='100%'>
    <img width='30%' src='gestoreSagre/static/logo.png' alt="Logo">
</div>

Il sito web realizzato è una piattaforma multiutente per la gestione di sagre paesane.

Ogni sagra è considerata come entità astratta, in quanto non trova una connotazione spazio-temporale se non con gli eventi: le relative istanze.

### Attori

Ci sono tre possibili soggetti utilizzatori: gli acquirenti, gli organizzatori e gli admin. Oltre a questi abbiamo anche predisposto la navigabilità ristretta ad utenti non autenticati.

Andiamo ora a vederli rispetto alle possibili interazioni col database.

> [!NOTE]
> Tutti gli use cases menzionati hanno relative API, ma non sempre una corrispettiva implementazione frontend. Ciò per due primarie motivazioni:
> + non è richiesto dai requisiti imposti presentare un prodotto completo;
> + vincoli temporali.

#### Utente non autenticato

Sono disponibili le `GET` per la visualizzazione di sagre ed eventi, questi ultimi sia con data passata che futura rispetto quella attuale.

Possibile utilizzo delle schermate di log in e sign up per autenticarsi al portale.

#### Acquirente

Si ha la possibilità di prenotare il biglietto per un evento, con relativa pagina dedicata alla visualizzazione di tutti i biglietti disponibili.

È inoltre capace di rilasciare un feedback rispetto la sagra, non l'evento.

#### Organizzatore

Può caricare sagre ed eventi. Così come gestirne modifiche e rimozioni.

#### Admin

L'utente che intende registrarsi come organizzatore deve essere approvato dall'admin.

Si ha inoltre la capacità di rimuovere un organizzatore o acquirente esistente.

## Avvio

Clonare innanzitutto la repository

```bash
git clone https://github.com/dellarosciagiorgio/progettoWebMobile
cd progettoWebMobile/
```

### Backend

Creare un file `.env` nella stessa cartella del `compose.yml` file, ovvero in `gestoreSagre/SagradaFamilia/`. Inserire in questo file le seguenti variabili:

```
DB_CONN_STRING="Data Source=sqlserver,1433;Initial Catalog=GestoreSagre;User ID=sa;Password=sagre_admin!1234;Trust Server Certificate=True;"
SA_PASSWORD=sagre_admin!1234
SQL_CONT_NAME=sqlserver
MY_SECRET=laTuaChiaveSegretaMoltoLunga123456789
MY_PLATFORM=linux/amd64
CERT_PASSWORD=laTuaChiaveSegretaMoltoLunga123456789
```

Per avviare il server eseguire i seguenti comandi:

```bash
cd gestoreSagre/SagradaFamilia/
docker compose up --build -d
```

Al termine, per chiudere il servizio occorre spegnere il container docker:

```bash
docker compose down
```

### Frontend

L'applicazione va lanciata eseguendo i seguenti comandi:

```bash
cd gestoreSagre/
npm run dev -- --open
```

In questo modo verrà automaticamente lanciata una scheda del browser all'indirizzo `http://localhost:5173/`

## Tecnologie

Avendo costruito pressoché un gestionale, necessitiamo di un backend che fornisca delle chiamate HTTP ad un database ed un frontend che permetta solamente agli utenti col giusto ruolo di effettuarle.

### Backend

Il servizio di backend è stato sviluppato usando .NET seguendo quella che viene chiamata CLEAN architecture.
Il codice si divide in 4 macro aree:
+ **Application**, parte dedicata alle logiche di business;
+ **Model**, entità e modelli;
+ **Infrastructure**, parte di collegamento al database e mapping delle entità;
+ **Web**, route path e api.

Il servizio di backend comunica con un database relazionale, SQL Server.

È stato scelto l'utilizzo di docker, in modo da ottenere una maggiore indipendenza nello sviluppo di frontend e backend. In questo modo i progettisti di frontend devono solamente cambiare qualche variabile d'ambiente, se necessario, e avviare il sistema.
Questo avvierà i seguenti servizi:
+ backend, avviabile sulla porta `443`
+ SQL server
+ SQL server configurator, servizio che permette di inizializzare il database creando il database, se non esiste, crea le tabelle e inserisce alcuni dati di esempio.


#### SQL Server

Il database ha in file di inizializzazione (vedere cartella init).
In questo file viene configurato il db, le tabelle, e inseriti dei valori.
Si vuole sottolineare che le password degli utenti vengono salvate già hashate, ed ovviamente, non sono in chiaro. 
Viene utilizzato [BCript](https://it.wikipedia.org/wiki/Bcrypt) per criptare e verificare le password.
BCrypt include automaticamente un salt all'interno dell'hash, quindi ogni volta che si esegue l'hash la stessa password otterrai un risultato diverso.
Quindi non serve salvare il salt separatamente: è incluso nell'hash.
La funzione Verify estrae il salt e confronta la password in modo sicuro.

##### BCrypt

```csharp
string hash = BCrypt.Net.BCrypt.HashPassword("password123", workFactor: 12);
```

+ Il `workFactor` (o **cost**) è un numero tra 4 e 31 e rende BCrypt "meno efficiente" (più lento).
+ Più è alto, più iterazioni interne fa BCrypt.
+ Esempio:  
  + `10` → default (2^10 = 1024 iterazioni)  
  + `12` → 4096 iterazioni  
  + `14` → 16.384 iterazioni  
  + `16` → 65.536 iterazioni (molto lento!)

Dipende dalla macchina, ma per dare un'idea:

| costo | t medio (s) |
| :-: | :-: |
| 10 | 0.1 |
| 12 | 0.3 |
| 14 | 1 |
| 16 | 3-5 |

### Frontend

Come framework Javascript abbiamo scelto l'accoppiata [Svelte](https://svelte.dev/docs/svelte/overview)+[SvelteKit](https://svelte.dev/docs/kit/introduction).

In fase di creazione del progetto ci siamo accorti che venivano messi a disposizione dei plugin, uno di questi era [mdsvex](https://mdsvex.pngwn.io/) che permette di scrivere pagine in Markdown anziché HTML. I file non avranno estensione _svelte_, ma _svx_.

Non ne abbiamo sfruttato tutte le potenzialità in quanto abbiamo preferito aggiungere una grafica più elaborata utilizzando [Bootstrap](https://getbootstrap.com/). Così facendo la gestione dei fogli di stile avviene con elementi già pronti all'uso.