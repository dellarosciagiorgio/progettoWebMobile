export async function getData(endpoint) {
    const response = await fetch(`${import.meta.env.VITE_API_URL}${endpoint}`);
    if (response.ok)
        return await response.json();
    else
        throw new Error('Errore nella risposta del server');
}