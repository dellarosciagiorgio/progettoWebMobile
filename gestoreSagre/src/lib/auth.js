// $lib/auth.js
import { isAuthenticated, userEmail } from './stores';
import { setCookie, getCookie, deleteCookie } from './cookie';

// URL del backend
const API_URL = "https://localhost:443/api";

/**
 * Funzione per effettuare chiamate API con gestione del token.
 * Viene impostato credentials: "include" per assicurare l'invio dei cookie anche in contesti CORS.
 */
export const apiCall = async (endpoint, method = "GET", body = null) => {
  const headers = {
    "Content-Type": "application/json"
  };

  // Aggiungi token di autorizzazione se disponibile
  const token = getCookie("authToken");
  if (token) {
    headers["Authorization"] = `Bearer ${token}`;
  }

  const options = {
    method,
    headers,
    credentials: "include" // Usa "include" per inviare i cookie (compresi quelli cross-origin)
  };

  if (body) {
    options.body = JSON.stringify(body);
  }

  try {
    const response = await fetch(`${API_URL}${endpoint}`, options);
    
    // Se l'API risponde con errore, prova a recuperare il messaggio di errore
    if (!response.ok) {
      const errorData = await response.json().catch(() => ({}));
      throw new Error(errorData.message || `Errore ${response.status}`);
    }
    
    // Risposte senza contenuto (204 No Content)
    if (response.status === 204) {
      return null;
    }
    
    // Parsing della risposta come JSON
    return await response.json();
  } catch (error) {
    console.error(`Errore chiamata API ${endpoint}:`, error);
    throw error;
  }
};

/**
 * Funzione per la verifica dell'autenticazione.
 * Controlla se sono presenti i cookie del token e dell'email, li valida con il backend e aggiorna gli store.
 */
export const verifyAuth = async () => {
  try {
    const token = getCookie("authToken");
    const email = getCookie("userEmail");
    
    console.log("Verifica autenticazione:", { token: !!token, email });
    
    if (!token || !email) {
      return false;
    }
    
    // Verifica la validità del token con il backend
    await apiCall("/verify");
    console.log("Token verificato con successo");
    
    // Aggiorna gli store
    isAuthenticated.set(true);
    userEmail.set(email);
    
    // Notifica altre schede del browser
    localStorage.setItem('authStatus', 'true');
    localStorage.setItem('userEmail', email);
    
    return true;
  } catch (error) {
    console.error("Errore durante la verifica del token:", error);
    // Se il token non è valido, esegui il logout per pulire lo stato
    await logout();
    return false;
  }
};

/**
 * Funzione per effettuare il login.
 * A seconda del flag rememberMe imposta cookie validi per 7 giorni o di sessione.
 */
export const login = async (email, password, rememberMe) => {
  const data = await apiCall("/login", "POST", { email, password, rememberMe });
  
  if (data.token) {
    if (rememberMe) {
      const expirationDays = 7; // Cookie valido per 7 giorni
      setCookie("authToken", data.token, expirationDays);
      setCookie("userEmail", email, expirationDays);
      if (data.role) setCookie("userRole", data.role, expirationDays);
    } else {
      // Cookie di sessione (scadono alla chiusura del browser)
      setCookie("authToken", data.token);
      setCookie("userEmail", email);
      if (data.role) setCookie("userRole", data.role);
    }
  }
  
  // Aggiorna gli store
  isAuthenticated.set(true);
  userEmail.set(email);
  
  // Notifica altre schede del browser
  localStorage.setItem('authStatus', 'true');
  localStorage.setItem('userEmail', email);
  
  return data;
};

/**
 * Funzione per il logout.
 * Chiama l'endpoint di logout sul server (se necessario) e pulisce i cookie e gli store.
 */
export const logout = async () => {
  try {
    const token = getCookie("authToken");
    if (token) {
      // Effettua la chiamata al server per invalidare il token
      await apiCall("/logout", "POST").catch(() => {});
    }
  } catch (error) {
    console.error("Errore durante il logout:", error);
  } finally {
    // Rimuove i cookie indipendentemente dall'esito della chiamata
    deleteCookie("authToken");
    deleteCookie("userEmail");
    deleteCookie("userRole");
    
    // Aggiorna gli store
    isAuthenticated.set(false);
    userEmail.set('');
    
    // Pulisce le notifiche di autenticazione in localStorage (utile per altre schede)
    localStorage.removeItem('authStatus');
    localStorage.removeItem('userEmail');
  }
};
