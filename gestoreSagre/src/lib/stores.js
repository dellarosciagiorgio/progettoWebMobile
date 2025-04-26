// $lib/stores.js
import { writable } from 'svelte/store';

// Store per lo stato di autenticazione
export const isAuthenticated = writable(false);

// Store per l'email dell'utente
export const userEmail = writable('');

// Se necessario, aggiungi altri store (ad esempio per il ruolo dell'utente)
export const userRole = writable('');