export async function getData(endpoint) {
    const response = await fetch(`http://localhost:8080/api/${endpoint}`);
    if (response.ok) {
        const json = await response.json();
        return json;
    } else {
        throw new Error('Errore nella risposta del server');
    }
}