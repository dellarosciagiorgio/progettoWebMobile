
## Comandi Docker 
Lista di comandi utili. Probabilmente vi servono solo i primi 10. La lista vuole essere esaustiva.
```bash
#Build del Dockercompose
docker compose build

#Up del Dockercompose in Detached Mode
docker compose up -d

#Build Up del Dockercompose in Detached Mode
docker compose up --build -d

#Ferma il container
docker compose down

#Ferma il container e ne elimina i volumi
docker compose down -v

#logs container
docker logs <cont_name>

# Mostra tutti i container (attivi e non)
docker ps -a

# Mostra solo i container attivi
docker ps

# Rimuove un'immagine Docker
docker rmi <nome_immagine>

# Mostra tutte le immagini Docker
docker images

# Rimuove tutte le immagini inutilizzate
docker image prune

#Build multi-platform and push
docker buildx build --platform linux/amd64,linux/arm64 -t mattiadispigno/sagradafamilia-server:latest --push .

#Tag Image:
docker tag sagradafamilia-server:latest mattiadispigno/sagradafamilia-server:latest

#Push Image on Docker Hub
docker push mattiadispigno/sagradafamilia-server:latest

# Entra in un container in esecuzione
docker exec -it <nome_container> bash

# Costruisce un'immagine Docker da un Dockerfile
docker build -t nome_immagine .

# Esegue un container in modalità interattiva
docker run -it nome_immagine

# Esegue un container in background
docker run -d --name nome_container nome_immagine

# Esegue un container con una porta esposta
docker run -p 8080:80 nome_immagine

# Pulisce tutto ciò che non è utilizzato (container, immagini, volumi, network)
docker system prune -a
```

## Evitare problemi di Cors
Per evitare problemi legati al cors bisogna aggiungere le pagine statiche di svelte a .net.
* Bisogna modificare il svelte.config.js
* eseguire il comando 
```bash 
    npm run build 
```
* Verrà prodotto un file build, copiarlo e incollarlo dentro SagradaFamilia/wwwroot
* Decommentare la riga nel compose.yml
```yml
    - ./wwwroot/build:/app/wwwroot:ro
```
### Modiciare il svelte.config.js
Assicurarsi che il svelte.config.js di svelte sia configurato come segue:
```js
import { mdsvex } from "mdsvex";
import adapter from '@sveltejs/adapter-static';

/** @type {import('@sveltejs/kit').Config} */
const config = {
    kit: {
		// adapter-auto only supports some environments, see https://svelte.dev/docs/kit/adapter-auto for a list.
		// If your environment is not supported, or you settled on a specific environment, switch out the adapter.
		// See https://svelte.dev/docs/kit/adapters for more information about adapters.
		adapter: adapter({
			pages: 'build',
			assets: 'build',
			fallback: 'index.html'
		  }),
	},
    prerender: {
		default: true
	  },
    preprocess: [mdsvex()],
    extensions: [".svelte", ".svx"]
};

export default config;

```

Se il pacchetto @sveltejs/adapter-static non è installato:
```bash
npm install -D @sveltejs/adapter-static
```

## HTTPS
Per buildare il certificato:
- creare una cartella in SagradaFamilia (cd gestoreSagre/SagradaFamilia) chiamata server_certificate
- aggiungere il certificato alla cartella
- aggiungere al file .env un variabile CERT_PASSWORD=laTuaChiaveSegretaMoltoLunga123456789
- docker compose up --build -d
- le api con https rispondono all'url: https://localhost:443/api
- le api con http rispondono all'url http://localhost:80/api


## File .env di docker
Descrizione variabili:
* DB_CONN_STRING stringa di connessione al db tra ""
* SA_PASSWORD password dell'utente sa del database
* SQL_CONT_NAME nome del container di sql server
* MY_SECRET secret utilizzata nel jwt.
* MY_PLATFORM piattaforma utilizzata, linux/amd64 di default o linux/arm64.

Esempio di file .env
```.env
DB_CONN_STRING="Data Source=sqlserver,1433;Initial Catalog=GestoreSagre;User ID=sa;Password=sagre_admin!1234;Trust Server Certificate=True;"
SA_PASSWORD=sagre_admin!1234
SQL_CONT_NAME=sqlserver
MY_SECRET=laTuaChiaveSegretaMoltoLunga123456789
MY_PLATFORM=linux/amd64
```


## BCript
In .NET puoi usare BCrypt per criptare (più correttamente: "hashare") le password in modo sicuro. BCrypt è un algoritmo di hashing pensato per proteggere le password, resistente agli attacchi di forza bruta e rainbow table.
Per prima cosa, ti serve una libreria compatibile. La più usata è BCrypt.Net-Next.
BCrypt include automaticamente un salt all'interno dell'hash, quindi ogni volta che hashai la stessa password otterrai un risultato diverso.
Non serve salvare il salt separatamente: è incluso nell'hash.
La funzione Verify estrae il salt e confronta la password in modo sicuro.

### Come si rende BCrypt “meno efficiente” (più lento)

```csharp
string hash = BCrypt.Net.BCrypt.HashPassword("password123", workFactor: 12);
```

- Il `workFactor` (o **cost**) è un numero tra 4 e 31.
- Più è alto, più iterazioni interne fa BCrypt.
- Esempio:  
  - `10` → default (2^10 = 1024 iterazioni)  
  - `12` → 4096 iterazioni  
  - `14` → 16.384 iterazioni  
  - `16` → 65.536 iterazioni (molto lento!)

---

###  Quanto rallenta?

Dipende dalla macchina, ma per dare un'idea:

| Cost | Tempo medio (approx) |
|------|-----------------------|
| 10   | ~100 ms               |
| 12   | ~300 ms               |
| 14   | ~1 sec                |
| 16   | ~3-5 sec              |

