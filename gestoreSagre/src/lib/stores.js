import { writable } from 'svelte/store';

// Store per gestire lo stato di autenticazione
export const isAuthenticated = writable(false);
export const userEmail = writable('');
export const role = writable('');

// Funzione per inizializzare gli store dai cookie
export function initStoresFromCookies() {
    // Funzione per ottenere un cookie
    function getCookie(name) {
        if (typeof document === 'undefined') return null;
        
        const nameEQ = name + "=";
        const ca = document.cookie.split(';');
        for(let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0)==' ') c = c.substring(1,c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
        }
        return null;
    }
    
    // Recupera i valori dai cookie
    const token = getCookie("authToken");
    const email = getCookie("userEmail");
    const userRole = getCookie("userRole");
    
    // Imposta gli store in base ai cookie
    if (token) {
        isAuthenticated.set(true);
    }
    
    if (email) {
        userEmail.set(email);
    }
    
    if (userRole) {
        role.set(userRole);
    }
}

// Funzione per aggiornare gli store durante il login
export function updateStores(token, email, userRole) {
    // Aggiorna gli store
    isAuthenticated.set(true);
    userEmail.set(email);
    role.set(userRole);
}

// Funzione per resettare gli store durante il logout
export function clearStores() {
    // Reimposta gli store ai valori predefiniti
    isAuthenticated.set(false);
    userEmail.set('');
    role.set('');
}