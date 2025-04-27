import { writable } from 'svelte/store';

// Store per gestire lo stato di autenticazione
export const isAuthenticated = writable(false);
export const userEmail = writable('');