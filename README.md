# Progetto Web e Mobile

A.A. 2024/25

Giorgio Della Roscia, Daniela Di Lucchio e Mattia Di Spigno

## Backend

Per avviare il server eseguire i seguenti comandi:

```bash
cd gestoreSagre/SagradaFamilia
sudo docker compose up --build -d
```

Al termine, per chiudere il servizio occorre spegnere il container docker:

```bash
sudo docker compose down
```

## Frontend

L'applicazione va lanciata eseguendo i seguenti comandi:

```bash
cd gestoreSagre
npm run dev -- --open
```