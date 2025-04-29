<script>
    import { onMount } from 'svelte';
    import { isAuthenticated, userEmail, role } from '$lib/stores';
    import { page } from '$app/stores';
    import { browser } from '$app/environment';
    import "bootstrap/dist/css/bootstrap.min.css";
    import "bootstrap-icons/font/bootstrap-icons.css";
    
    // Ottiene l'ID della sagra dai parametri della route
    let sagraId;
    $: sagraId = $page.params.id;
    
    let sagra = null;
    let eventi = [];
    let isLoading = true;
    let error = null;
    let showAddModal = false;
    let showEditModal = false;
    let showDeleteModal = false;
    let currentEvento = null;
    
    // Form data per nuovo evento o modifica
    let formData = {
        nome: '',
        descrizione: '',
        data: '',
        ora: '',
        durata: '',
        maxPartecipanti: 0,
        prezzoIngresso: 0
    };
    
    // Funzione per ottenere un cookie
    function getCookie(name) {
        if (!browser) return null;
        
        const nameEQ = name + "=";
        const ca = document.cookie.split(';');
        for(let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0)==' ') c = c.substring(1,c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
        }
        return null;
    }
    
    // Funzione per formattare la data da ISO a DD/MM/YYYY
    function formatDate(isoDate) {
        if (!isoDate) return '';
        const date = new Date(isoDate);
        return `${date.getDate().toString().padStart(2, '0')}/${(date.getMonth() + 1).toString().padStart(2, '0')}/${date.getFullYear()}`;
    }
    
    // Funzione per formattare la data da DD/MM/YYYY a ISO (YYYY-MM-DD)
    function formatDateForInput(dateString) {
        if (!dateString) return '';
        if (dateString.includes('-')) return dateString; // Already in ISO format
        
        const parts = dateString.split('/');
        if (parts.length !== 3) return '';
        
        return `${parts[2]}-${parts[1].padStart(2, '0')}-${parts[0].padStart(2, '0')}`;
    }
    
    // Carica i dettagli della sagra
    async function loadSagraDetails() {
        try {
            const token = getCookie("authToken");
            if (!token) {
                throw new Error("Token di autenticazione non trovato");
            }
            
            // Utilizzo dell'endpoint "Get Sagre By Id" dall'immagine Postman
            const response = await fetch(`${import.meta.env.VITE_API_URL}Get Sagre By Id?id=${sagraId}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                }
            });
            
            // Controlla se la risposta è vuota
            const responseText = await response.text();
            if (!responseText) {
                console.error("Risposta vuota dal server");
                throw new Error("Il server ha restituito una risposta vuota");
            }
            
            console.log("Risposta dettagli sagra dal server (raw):", responseText);
            
            // Prova a parsare la risposta come JSON
            let data;
            try {
                data = JSON.parse(responseText);
            } catch (jsonError) {
                console.error("Errore nel parsing JSON:", jsonError);
                throw new Error(`Errore nel parsing della risposta: ${responseText.substring(0, 100)}...`);
            }
            
            if (!response.ok) {
                const errorMsg = data && data.message ? data.message : `Errore durante il recupero della sagra: ${response.status}`;
                throw new Error(errorMsg);
            }
            
            if (data.success && data.data) {
                sagra = data.data;
            } else if (Array.isArray(data) && data.length > 0) {
                // Se l'API restituisce un array con un singolo item
                sagra = data[0];
            } else if (typeof data === 'object' && data !== null) {
                // Se l'API restituisce direttamente l'oggetto sagra
                sagra = data;
            } else {
                throw new Error("Dati della sagra non disponibili");
            }
            
            console.log("Dettagli sagra caricati:", sagra);
        } catch (err) {
            console.error("Errore nel caricamento della sagra:", err);
            error = err.message;
        }
    }
    
    // Carica gli eventi della sagra
    async function loadSagraEvents() {
        try {
            const token = getCookie("authToken");
            if (!token) {
                throw new Error("Token di autenticazione non trovato");
            }
            
            // Utilizzo dell'endpoint "GetEventi ByIdSagra" dall'immagine Postman
            const response = await fetch(`${import.meta.env.VITE_API_URL}GetEventi ByIdSagra?id=${sagraId}`, {
                method: "GET",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                }
            });
            
            // Controlla se la risposta è vuota
            const responseText = await response.text();
            if (!responseText) {
                console.error("Risposta vuota dal server");
                throw new Error("Il server ha restituito una risposta vuota");
            }
            
            console.log("Risposta eventi dal server (raw):", responseText);
            
            // Prova a parsare la risposta come JSON
            let data;
            try {
                data = JSON.parse(responseText);
            } catch (jsonError) {
                console.error("Errore nel parsing JSON:", jsonError);
                throw new Error(`Errore nel parsing della risposta: ${responseText.substring(0, 100)}...`);
            }
            
            if (!response.ok) {
                const errorMsg = data && data.message ? data.message : `Errore durante il recupero degli eventi: ${response.status}`;
                throw new Error(errorMsg);
            }
            
            if (data.success && data.data) {
                eventi = data.data;
            } else if (Array.isArray(data)) {
                // Se l'API restituisce direttamente un array di eventi
                eventi = data;
            } else {
                eventi = [];
            }
            
            console.log("Eventi caricati:", eventi);
        } catch (err) {
            console.error("Errore nel caricamento degli eventi:", err);
            error = err.message;
            eventi = [];
        } finally {
            isLoading = false;
        }
    }
    
    // Aggiungi un nuovo evento
    async function addEvent() {
        try {
            const token = getCookie("authToken");
            if (!token) {
                throw new Error("Token di autenticazione non trovato");
            }
            
            // Assicurati che l'ID della sagra sia incluso nei dati
            const eventData = { 
                ...formData, 
                sagraId: parseInt(sagraId)  // Assicurati che l'ID sia un numero se necessario
            };
            
            console.log("Dati evento da inviare:", eventData);
            
            // Utilizzo dell'endpoint "AddEvento" dall'immagine Postman
            const response = await fetch(`${import.meta.env.VITE_API_URL}AddEvento`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify(eventData)
            });
            
            // Controlla se la risposta è vuota
            const responseText = await response.text();
            if (!responseText) {
                console.error("Risposta vuota dal server");
                throw new Error("Il server ha restituito una risposta vuota");
            }
            
            console.log("Risposta dal server (raw):", responseText);
            
            // Prova a parsare la risposta come JSON
            let data;
            try {
                data = JSON.parse(responseText);
            } catch (jsonError) {
                console.error("Errore nel parsing JSON:", jsonError);
                throw new Error(`Errore nel parsing della risposta: ${responseText.substring(0, 100)}...`);
            }
            
            if (!response.ok) {
                const errorMsg = data && data.message ? data.message : `Errore durante l'aggiunta dell'evento: ${response.status}`;
                throw new Error(errorMsg);
            }
            
            console.log("Evento aggiunto con successo:", data);
            
            // Ricarica gli eventi dopo l'aggiunta
            await loadSagraEvents();
            showAddModal = false;
            resetForm();
            
        } catch (err) {
            console.error("Errore nell'aggiunta dell'evento:", err);
            error = err.message;
        }
    }
    
    // Modifica un evento esistente
    async function updateEvent() {
        try {
            const token = getCookie("authToken");
            if (!token) {
                throw new Error("Token di autenticazione non trovato");
            }
            
            // Prepara i dati per l'aggiornamento includendo l'ID dell'evento
            const updateData = {
                id: currentEvento.id,
                ...formData,
                sagraId: parseInt(sagraId)  // Assicurati che l'ID sia un numero se necessario
            };
            
            console.log("Dati evento da aggiornare:", updateData);
            
            // Utilizzo dell'endpoint "Modifica Evento" dall'immagine Postman
            const response = await fetch(`${import.meta.env.VITE_API_URL}Modifica Evento`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify(updateData)
            });
            
            // Controlla se la risposta è vuota
            const responseText = await response.text();
            if (!responseText) {
                console.error("Risposta vuota dal server");
                throw new Error("Il server ha restituito una risposta vuota");
            }
            
            console.log("Risposta dal server (raw):", responseText);
            
            // Prova a parsare la risposta come JSON
            let data;
            try {
                data = JSON.parse(responseText);
            } catch (jsonError) {
                console.error("Errore nel parsing JSON:", jsonError);
                throw new Error(`Errore nel parsing della risposta: ${responseText.substring(0, 100)}...`);
            }
            
            if (!response.ok) {
                const errorMsg = data && data.message ? data.message : `Errore durante la modifica dell'evento: ${response.status}`;
                throw new Error(errorMsg);
            }
            
            console.log("Evento aggiornato con successo:", data);
            
            // Ricarica gli eventi dopo la modifica
            await loadSagraEvents();
            showEditModal = false;
            
        } catch (err) {
            console.error("Errore nella modifica dell'evento:", err);
            error = err.message;
        }
    }
    
    // Elimina un evento
    async function deleteEvent() {
        try {
            const token = getCookie("authToken");
            if (!token) {
                throw new Error("Token di autenticazione non trovato");
            }
            
            console.log("Eliminazione evento con ID:", currentEvento.id);
            
            // Utilizzo dell'endpoint "DeleteEvento" dall'immagine Postman
            const response = await fetch(`${import.meta.env.VITE_API_URL}DeleteEvento`, {
                method: "DELETE",
                headers: {
                    "Content-Type": "application/json",
                    "Authorization": `Bearer ${token}`
                },
                body: JSON.stringify({ id: currentEvento.id })
            });
            
            // Controlla se la risposta è vuota
            const responseText = await response.text();
            if (!responseText) {
                // Per DELETE, una risposta vuota può essere normale
                if (response.ok) {
                    console.log("Evento eliminato con successo (risposta vuota)");
                    // Ricarica gli eventi dopo l'eliminazione
                    await loadSagraEvents();
                    showDeleteModal = false;
                    return;
                } else {
                    console.error("Risposta vuota dal server con stato di errore");
                    throw new Error(`Errore durante l'eliminazione dell'evento: ${response.status}`);
                }
            }
            
            console.log("Risposta dal server (raw):", responseText);
            
            // Prova a parsare la risposta come JSON
            let data;
            try {
                data = JSON.parse(responseText);
            } catch (jsonError) {
                console.error("Errore nel parsing JSON:", jsonError);
                // Se il response.ok è true, considera comunque l'operazione come riuscita
                if (response.ok) {
                    console.log("Evento eliminato con successo (risposta non-JSON)");
                    await loadSagraEvents();
                    showDeleteModal = false;
                    return;
                }
                throw new Error(`Errore nel parsing della risposta: ${responseText.substring(0, 100)}...`);
            }
            
            if (!response.ok) {
                const errorMsg = data && data.message ? data.message : `Errore durante l'eliminazione dell'evento: ${response.status}`;
                throw new Error(errorMsg);
            }
            
            console.log("Evento eliminato con successo:", data);
            
            // Ricarica gli eventi dopo l'eliminazione
            await loadSagraEvents();
            showDeleteModal = false;
            
        } catch (err) {
            console.error("Errore nell'eliminazione dell'evento:", err);
            error = err.message;
        }
    }
    
    // Prepara il form per la modifica
    function openEditModal(evento) {
        currentEvento = evento;
        formData = {
            nome: evento.nome,
            descrizione: evento.descrizione,
            data: formatDateForInput(evento.data),
            ora: evento.ora,
            durata: evento.durata,
            maxPartecipanti: evento.maxPartecipanti,
            prezzoIngresso: evento.prezzoIngresso
        };
        showEditModal = true;
    }
    
    // Prepara il modal per l'eliminazione
    function openDeleteModal(evento) {
        currentEvento = evento;
        showDeleteModal = true;
    }
    
    // Resetta il form
    function resetForm() {
        formData = {
            nome: '',
            descrizione: '',
            data: '',
            ora: '',
            durata: '',
            maxPartecipanti: 0,
            prezzoIngresso: 0
        };
    }
    
    // Formatta il prezzo come valuta euro
    function formatCurrency(value) {
        return new Intl.NumberFormat('it-IT', { 
            style: 'currency', 
            currency: 'EUR',
            minimumFractionDigits: 2
        }).format(value);
    }
    
    // Torna alla pagina delle mie sagre
    function goBack() {
        window.location.href = "/LeMieSagre";
    }
    
    onMount(async () => {
        if (!browser) return;
        
        // Controlla se l'utente è autenticato come organizzatore
        const token = getCookie("authToken");
        const userRole = getCookie("userRole");
        
        if (!token || userRole !== 'Organizzatore') {
            // Reindirizza alla home se non è un organizzatore autenticato
            window.location.href = "/Home";
            return;
        }
        
        // Carica i dettagli della sagra e i suoi eventi
        await loadSagraDetails();
        await loadSagraEvents();
    });
</script>

<svelte:head>
    <title>{sagra ? `Eventi - ${sagra.nome}` : 'Eventi della Sagra'} | Village Festival</title>
</svelte:head>

<div class="container mt-4">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <div>
            <button class="btn btn-outline-secondary mb-2" on:click={goBack}>
                <i class="bi bi-arrow-left me-2"></i>Torna alle Mie Sagre
            </button>
            {#if sagra}
                <h1 class="mt-2">{sagra.nome} - Eventi</h1>
                <p class="text-muted">
                    <i class="bi bi-geo-alt me-1"></i>{sagra.luogo}
                    <span class="mx-2">•</span>
                    <i class="bi bi-calendar-event me-1"></i>
                    {formatDate(sagra.dataInizio)} - {formatDate(sagra.dataFine)}
                </p>
            {/if}
        </div>
        <button class="btn btn-primary" on:click={() => {resetForm(); showAddModal = true;}}>
            <i class="bi bi-plus-circle me-2"></i>Aggiungi Evento
        </button>
    </div>
    
    {#if error}
        <div class="alert alert-danger">
            {error}
        </div>
    {/if}
    
    {#if isLoading}
        <div class="d-flex justify-content-center mt-5">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Caricamento...</span>
            </div>
        </div>
    {:else if eventi.length === 0}
        <div class="alert alert-info">
            Non ci sono eventi per questa sagra. Clicca su "Aggiungi Evento" per iniziare.
        </div>
    {:else}
        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            {#each eventi as evento (evento.id)}
                <div class="col">
                    <div class="card h-100 event-card">
                        <div class="card-header">
                            <div class="d-flex justify-content-between align-items-center">
                                <span>
                                    <i class="bi bi-calendar-date me-2"></i> 
                                    {formatDate(evento.data)}
                                </span>
                                <span>
                                    <i class="bi bi-clock me-2"></i> 
                                    {evento.ora}
                                </span>
                            </div>
                        </div>
                        <div class="card-body">
                            <h5 class="card-title">{evento.nome}</h5>
                            <p class="card-text">{evento.descrizione}</p>
                            <p class="card-text">
                                <small class="text-muted">
                                    <i class="bi bi-hourglass-split me-1"></i> Durata: {evento.durata}
                                </small>
                            </p>
                            <div class="d-flex justify-content-between mt-3">
                                <span class="badge bg-info">
                                    <i class="bi bi-people me-1"></i> 
                                    Max: {evento.maxPartecipanti} persone
                                </span>
                                <span class="badge bg-success">
                                    {formatCurrency(evento.prezzoIngresso)}
                                </span>
                            </div>
                        </div>
                        <div class="card-footer bg-white">
                            <div class="d-flex justify-content-end">
                                <button class="btn btn-outline-secondary me-2" on:click={() => openEditModal(evento)}>
                                    <i class="bi bi-pencil"></i>
                                </button>
                                <button class="btn btn-outline-danger" on:click={() => openDeleteModal(evento)}>
                                    <i class="bi bi-trash"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            {/each}
        </div>
    {/if}
</div>

<!-- Modal Aggiungi Evento -->
{#if showAddModal}
    <div class="modal fade show" style="display: block;" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Aggiungi Nuovo Evento</h5>
                    <button type="button" class="btn-close" on:click={() => showAddModal = false}></button>
                </div>
                <div class="modal-body">
                    <form on:submit|preventDefault={addEvent}>
                        <div class="mb-3">
                            <label for="nome" class="form-label">Nome*</label>
                            <input type="text" class="form-control" id="nome" bind:value={formData.nome} required>
                        </div>
                        <div class="mb-3">
                            <label for="descrizione" class="form-label">Descrizione*</label>
                            <textarea class="form-control" id="descrizione" rows="3" bind:value={formData.descrizione} required></textarea>
                        </div>
                        <div class="row">
                            <div class="col-md-4 mb-3">
                                <label for="data" class="form-label">Data*</label>
                                <input type="date" class="form-control" id="data" bind:value={formData.data} required>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label for="ora" class="form-label">Ora*</label>
                                <input type="time" class="form-control" id="ora" bind:value={formData.ora} required>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label for="durata" class="form-label">Durata*</label>
                                <input type="text" class="form-control" id="durata" placeholder="es. 2 ore" bind:value={formData.durata} required>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="maxPartecipanti" class="form-label">Max Partecipanti*</label>
                                <input type="number" class="form-control" id="maxPartecipanti" min="0" bind:value={formData.maxPartecipanti} required>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="prezzoIngresso" class="form-label">Prezzo Ingresso (€)*</label>
                                <input type="number" class="form-control" id="prezzoIngresso" min="0" step="0.01" bind:value={formData.prezzoIngresso} required>
                            </div>
                        </div>
                        <div class="text-end">
                            <button type="button" class="btn btn-secondary me-2" on:click={() => showAddModal = false}>Annulla</button>
                            <button type="submit" class="btn btn-primary">Salva</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div class="modal-backdrop fade show"></div>
{/if}

<!-- Modal Modifica Evento -->
{#if showEditModal && currentEvento}
    <div class="modal fade show" style="display: block;" tabindex="-1">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Modifica Evento</h5>
                    <button type="button" class="btn-close" on:click={() => showEditModal = false}></button>
                </div>
                <div class="modal-body">
                    <form on:submit|preventDefault={updateEvent}>
                        <div class="mb-3">
                            <label for="edit-nome" class="form-label">Nome*</label>
                            <input type="text" class="form-control" id="edit-nome" bind:value={formData.nome} required>
                        </div>
                        <div class="mb-3">
                            <label for="edit-descrizione" class="form-label">Descrizione*</label>
                            <textarea class="form-control" id="edit-descrizione" rows="3" bind:value={formData.descrizione} required></textarea>
                        </div>
                        <div class="row">
                            <div class="col-md-4 mb-3">
                                <label for="edit-data" class="form-label">Data*</label>
                                <input type="date" class="form-control" id="edit-data" bind:value={formData.data} required>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label for="edit-ora" class="form-label">Ora*</label>
                                <input type="time" class="form-control" id="edit-ora" bind:value={formData.ora} required>
                            </div>
                            <div class="col-md-4 mb-3">
                                <label for="edit-durata" class="form-label">Durata*</label>
                                <input type="text" class="form-control" id="edit-durata" placeholder="es. 2 ore" bind:value={formData.durata} required>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="edit-maxPartecipanti" class="form-label">Max Partecipanti*</label>
                                <input type="number" class="form-control" id="edit-maxPartecipanti" min="0" bind:value={formData.maxPartecipanti} required>
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="edit-prezzoIngresso" class="form-label">Prezzo Ingresso (€)*</label>
                                <input type="number" class="form-control" id="edit-prezzoIngresso" min="0" step="0.01" bind:value={formData.prezzoIngresso} required>
                            </div>
                        </div>
                        <div class="text-end">
                            <button type="button" class="btn btn-secondary me-2" on:click={() => showEditModal = false}>Annulla</button>
                            <button type="submit" class="btn btn-primary">Aggiorna</button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div class="modal-backdrop fade show"></div>
{/if}

<!-- Modal Elimina Evento -->
{#if showDeleteModal && currentEvento}
    <div class="modal fade show" style="display: block;" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Conferma Eliminazione</h5>
                    <button type="button" class="btn-close" on:click={() => showDeleteModal = false}></button>
                </div>
                <div class="modal-body">
                    <p>Sei sicuro di voler eliminare l'evento "{currentEvento.nome}"?</p>
                    <p class="text-danger">Questa azione è irreversibile.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" on:click={() => showDeleteModal = false}>Annulla</button>
                    <button type="button" class="btn btn-danger" on:click={deleteEvent}>Elimina</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal-backdrop fade show"></div>
{/if}

<style>
    .card-header {
        background-color: #f8f9fa;
        border-bottom: 1px solid #eee;
    }
    
    .event-card {
        transition: transform 0.3s ease, box-shadow 0.3s ease;
        border-radius: 10px;
        overflow: hidden;
        border: none;
        box-shadow: 0 4px 10px rgba(0,0,0,0.1);
    }
    
    .event-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 10px 20px rgba(0,0,0,0.15);
    }
    
    .card-title {
        font-weight: bold;
        color: #333;
    }
    
    .card-footer {
        background: none;
        border-top: 1px solid #eee;
        padding-top: 1rem;
    }
    
    .card-text {
        color: #555;
    }
    
    .badge {
        font-size: 0.8rem;
        padding: 0.5rem 0.7rem;
    }
    
    .modal-content {
        border-radius: 12px;
        border: none;
        box-shadow: 0 5px 20px rgba(0,0,0,0.2);
    }
    
    .modal-header {
        border-bottom: 1px solid #eee;
    }
    
    .modal-footer {
        border-top: 1px solid #eee;
    }
</style>