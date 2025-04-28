import { getCookie } from './cookies';

export async function getData(endpoint) {
    try {
        const response = await fetch(`${import.meta.env.VITE_API_URL}${endpoint}`);
        if (!response.ok)
            throw new Error(`Errore HTTP: ${response.status}`);
        return await response.json();
    } catch (error) {
        console.error("Errore durante la fetch:", error);
        throw error;
    }
}

export async function getDataAuth(endpoint) {
    try {
        const authToken = getCookie('authToken');
        const response = await fetch(`${import.meta.env.VITE_API_URL}${endpoint}`, {
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${authToken}`
            },
        });
        if (!response.ok)
            throw new Error(`Errore HTTP: ${response.status}`);
        return await response.json();
    } catch (error) {
        console.error("Errore durante la fetch con autenticazione:", error);
        throw error;
    }
}