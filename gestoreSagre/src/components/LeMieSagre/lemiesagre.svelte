<script>
    import { onMount } from "svelte";
    import { isAuthenticated, userEmail, role } from "$lib/stores";
    import { browser } from "$app/environment";
    import "bootstrap/dist/css/bootstrap.min.css";
    import "./lemiesagre.css";

    let sagre = [];
    let isLoading = true;
    let error = null;
    let showAddModal = false;
    let showEditModal = false;
    let showDeleteModal = false;
    let currentSagra = null;

    // Form data per nuova sagra o modifica
    let formData = {
        nome: "",
        descrizione: "",
        luogo: "",
        dataInizio: "",
        dataFine: "",
        immagine: ""
    };

    // Funzione per ottenere un cookie
    function getCookie(name) {
        if (!browser) return null;

        const nameEQ = name + "=";
        const ca = document.cookie.split(";");
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) == " ") c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) == 0)
                return c.substring(nameEQ.length, c.length);
        }
        return null;
    }

    // Funzione per formattare la data da ISO a DD/MM/YYYY
    function formatDate(isoDate) {
        if (!isoDate) return "";
        const date = new Date(isoDate);
        return `${date.getDate().toString().padStart(2, "0")}/${(date.getMonth() + 1).toString().padStart(2, "0")}/${date.getFullYear()}`;
    }

    // Funzione per formattare la data da DD/MM/YYYY a ISO (YYYY-MM-DD)
    function formatDateForInput(dateString) {
        if (!dateString) return "";
        if (dateString.includes("-")) return dateString; // Already in ISO format

        const parts = dateString.split("/");
        if (parts.length !== 3) return "";

        return `${parts[2]}-${parts[1].padStart(2, "0")}-${parts[0].padStart(2, "0")}`;
    }

    // Funzione per normalizzare i dati delle sagre (assicura consistenza nei nomi delle proprietà)
    function normalizzaDatiSagre(sagreData) {
        return sagreData.map(sagra => {
            // Log per debug
            console.log("Struttura sagra originale:", JSON.stringify(sagra));
            
            // Assicura che tutte le proprietà necessarie siano presenti
            const normalizedSagra = {
                id: sagra.id || sagra.idSagra || sagra.IdSagra || "",
                idSagra: sagra.idSagra || sagra.IdSagra || sagra.id || "",
                nome: sagra.nome || sagra.nomeSagra || sagra.NomeSagra || "",
                nomeSagra: sagra.nomeSagra || sagra.NomeSagra || sagra.nome || "",
                descrizione: sagra.descrizione || sagra.Descrizione || "",
                luogo: sagra.luogo || sagra.Luogo || "",
                dataInizio: sagra.dataInizio || sagra.DataInizio || "",
                dataFine: sagra.dataFine || sagra.DataFine || "",
                immagine: sagra.immagine || sagra.Immagine || ""
            };
            
            console.log("Sagra normalizzata:", JSON.stringify(normalizedSagra));
            return normalizedSagra;
        });
    }

    // Carica le sagre dell'organizzatore
    async function loadMySagre() {
        isLoading = true;
        error = null;

        try {
            const token = getCookie("authToken");
            if (!token) {
                throw new Error("Token di autenticazione non trovato");
            }

            // In base all'API di Postman, usiamo l'endpoint Get Sagre
            const response = await fetch(
                `${import.meta.env.VITE_API_URL}sagre/mySagre`,
                {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${token}`
                    }
                }
            );

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
                throw new Error(
                    `Errore nel parsing della risposta: ${responseText.substring(0, 100)}...`
                );
            }

            if (!response.ok) {
                const errorMsg =
                    data && data.message
                        ? data.message
                        : `Errore durante il recupero delle sagre: ${response.status}`;
                throw new Error(errorMsg);
            }

            // Log dei dati ricevuti per debug
            console.log("Struttura dati ricevuti:", data);
            
            // Estrai le sagre dai dati ricevuti
            let sagreData = [];
            if (data.success === true && Array.isArray(data.data)) {
                sagreData = data.data;
                console.log("Dati estratti da data.success.data");
            } else if (Array.isArray(data)) {
                sagreData = data;
                console.log("Dati estratti direttamente dall'array");
            } else if (typeof data === 'object' && data !== null) {
                // Se data è un oggetto singolo, probabilmente è una sagra
                sagreData = [data];
                console.log("Dati estratti da oggetto singolo");
            }
            
            // Normalizza i dati delle sagre
            sagre = normalizzaDatiSagre(sagreData);
            
            console.log("Sagre normalizzate:", sagre);
        } catch (err) {
            console.error("Errore nel caricamento delle sagre:", err);
            error = err.message;
            sagre = [];
        } finally {
            isLoading = false;
        }
    }

    async function addSagra() {
        try {
            const token = getCookie("authToken");
            if (!token)
                throw new Error("Token di autenticazione non trovato");

            console.log("Dati da inviare:", formData);

            let nomeSagra, descrizioneSagra;

            if (formData) {
                nomeSagra = formData.nome;
                descrizioneSagra = formData.descrizione;
            } else {
                console.warn("formData.success è false o formData non è definito. Utilizzo valori di default.");
                nomeSagra = "";
                descrizioneSagra = "";
            }
            // Usa l'endpoint "Add Sagra" come mostrato in Postman
            const response = await fetch(
                `${import.meta.env.VITE_API_URL}sagra/`,
                {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${token}`
                    },
                    body: JSON.stringify({ NomeSagra: nomeSagra, Descrizione: descrizioneSagra })
                }
            );

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
                throw new Error(
                    `Errore nel parsing della risposta: ${responseText.substring(0, 100)}...`
                );
            }

            if (!response.ok) {
                const errorMsg =
                    data && data.message
                        ? data.message
                        : `Errore durante l'aggiunta della sagra: ${response.status}`;
                throw new Error(errorMsg);
            }

            console.log("Sagra aggiunta con successo:", data);

            // Ricarica le sagre dopo l'aggiunta
            await loadMySagre();
            showAddModal = false;
            resetForm();
        } catch (err) {
            console.error("Errore nell'aggiunta della sagra:", err);
            error = err.message;
        }
    }

    // Modifica una sagra esistente
    async function updateSagra() {
        try {
            const token = getCookie("authToken");
            if (!token) {
                throw new Error("Token di autenticazione non trovato");
            }

            console.log("Dati da inviare per aggiornamento:", formData);

            // Usa l'endpoint "Edit Sagra" come mostrato in Postman
            const response = await fetch(
                `${import.meta.env.VITE_API_URL}sagra/`,
                {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${token}`
                    },
                    body: JSON.stringify({
                        id: currentSagra.id,
                        ...formData
                    })
                }
            );

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
                throw new Error(
                    `Errore nel parsing della risposta: ${responseText.substring(0, 100)}...`
                );
            }

            if (!response.ok) {
                const errorMsg =
                    data && data.message
                        ? data.message
                        : `Errore durante la modifica della sagra: ${response.status}`;
                throw new Error(errorMsg);
            }

            console.log("Sagra aggiornata con successo:", data);

            // Ricarica le sagre dopo la modifica
            await loadMySagre();
            showEditModal = false;
        } catch (err) {
            console.error("Errore nella modifica della sagra:", err);
            error = err.message;
        }
    }

    // Elimina una sagra
    async function deleteSagra() {
        try {
            const token = getCookie("authToken");
            if (!token) {
                throw new Error("Token di autenticazione non trovato");
            }

            // Usa l'endpoint "Delete Sagra" come mostrato in Postman
            const response = await fetch(
                `${import.meta.env.VITE_API_URL}sagra`,
                {
                    method: "DELETE",
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${token}`
                    },
                    body: JSON.stringify({ IdSagra: currentSagra.idSagra })
                }
            );

            // Controlla se la risposta è vuota
            const responseText = await response.text();
            if (!responseText) {
                // Per DELETE, una risposta vuota può essere normale
                if (response.ok) {
                    console.log(
                        "Sagra eliminata con successo (risposta vuota)"
                    );
                    // Ricarica le sagre dopo l'eliminazione
                    await loadMySagre();
                    showDeleteModal = false;
                    return;
                } else {
                    console.error(
                        "Risposta vuota dal server con stato di errore"
                    );
                    throw new Error(
                        `Errore durante l'eliminazione della sagra: ${response.status}`
                    );
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
                    console.log(
                        "Sagra eliminata con successo (risposta non-JSON)"
                    );
                    await loadMySagre();
                    showDeleteModal = false;
                    return;
                }
                throw new Error(
                    `Errore nel parsing della risposta: ${responseText.substring(0, 100)}...`
                );
            }

            if (!response.ok) {
                const errorMsg =
                    data && data.message
                        ? data.message
                        : `Errore durante l'eliminazione della sagra: ${response.status}`;
                throw new Error(errorMsg);
            }

            console.log("Sagra eliminata con successo:", data);

            // Ricarica le sagre dopo l'eliminazione
            await loadMySagre();
            showDeleteModal = false;
        } catch (err) {
            console.error("Errore nell'eliminazione della sagra:", err);
            error = err.message;
        }
    }

    // Prepara il form per la modifica
    function openEditModal(sagra) {
        currentSagra = sagra;
        formData = {
            nome: sagra.nome || "",
            descrizione: sagra.descrizione || "",
            luogo: sagra.luogo || "",
            dataInizio: formatDateForInput(sagra.dataInizio) || "",
            dataFine: formatDateForInput(sagra.dataFine) || "",
            immagine: sagra.immagine || ""
        };
        showEditModal = true;
    }

    // Prepara il modal per l'eliminazione
    function openDeleteModal(sagra) {
        currentSagra = sagra;
        showDeleteModal = true;
    }

    // Resetta il form
    function resetForm() {
        formData = {
            nome: "",
            descrizione: "",
            luogo: "",
            dataInizio: "",
            dataFine: "",
            immagine: ""
        };
    }

    // Naviga alla pagina degli eventi della sagra
    function viewEvents(sagraId) {
        window.location.href = `/Sagra/${sagraId}/Eventi`;
    }

    /* Correggi accesso alle proprietà sagra */
    onMount(() => {
        if (!browser) return;

        // Controlla se l'utente è autenticato come organizzatore
        const token = getCookie("authToken");
        const userRole = getCookie("userRole");

        if (!token || userRole !== "Organizzatore") {
            // Reindirizza alla home se non è un organizzatore autenticato
            window.location.href = "/Home";
            return;
        }

        // Carica le sagre dell'organizzatore
        loadMySagre();
    });
</script>

<svelte:head>
    <title>Le Mie Sagre | Village Festival</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css">
</svelte:head>

<div class="container mt-4">
    <div class="page-header d-flex justify-content-between align-items-center mb-4">
        <h1>Le Mie Sagre</h1>
        <button
            class="btn btn-add-sagra"
            on:click={() => {
                resetForm();
                showAddModal = true;
            }}
        >
            <span class="add-icon">+</span> Aggiungi Sagra
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
    {:else if sagre.length === 0}
        <div class="alert alert-info">
            Non hai ancora creato nessuna sagra. Clicca su "Aggiungi Sagra" per
            iniziare.
        </div>
    {:else}
        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            {#each sagre as sagra}
                <div class="col">
                    <div class="card h-100 event-card">
                        {#if sagra.immagine}
                            <img
                                src={sagra.immagine}
                                class="card-img-top"
                                alt={sagra.nome}
                            />
                        {:else}
                            <div class="card-img-placeholder">
                                <div class="no-image-text">Nessuna immagine</div>
                            </div>
                        {/if}
                        <div class="card-body">
                            <h5 class="card-title">{sagra.nome}</h5>
                            <p class="card-text description">
                                {#if sagra.descrizione}
                                    {sagra.descrizione}
                                {:else}
                                    <span class="no-description">Nessuna descrizione disponibile</span>
                                {/if}
                            </p>
                            <p class="card-text location">
                                <span class="location-prefix">Luogo:</span> 
                                {#if sagra.luogo} 
                                    {sagra.luogo}
                                {:else}
                                    <span class="missing-data">Non specificato</span>
                                {/if}
                            </p>
                            <p class="card-text date">
                                <span class="date-prefix">Date:</span> 
                                {#if sagra.dataInizio || sagra.dataFine}
                                    {formatDate(sagra.dataInizio)} - {formatDate(sagra.dataFine)}
                                {:else}
                                    <span class="missing-data">Non specificate</span>
                                {/if}
                            </p>
                        </div>
                        <div class="card-footer">
                            <div class="d-flex justify-content-between">
                                <button
                                    class="btn btn-events"
                                    on:click={() => viewEvents(sagra.id)}
                                >
                                    Eventi
                                </button>
                                <div class="action-buttons">
                                    <button
                                        class="btn btn-edit"
                                        on:click={() => openEditModal(sagra)}
                                        title="Modifica sagra"
                                    >
                                        <span class="edit-icon">✏️</span>
                                    </button>
                                    <button
                                        class="btn btn-delete"
                                        on:click={() => openDeleteModal(sagra)}
                                        title="Elimina sagra"
                                    >
                                        <span class="delete-icon">🗑️</span>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            {/each}
        </div>
    {/if}
</div>

<!-- Modal Aggiungi Sagra -->
{#if showAddModal}
    <div class="modal-overlay">
        <div class="modal-wrapper">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Aggiungi Nuova Sagra</h5>
                    <button
                        type="button"
                        class="btn-close"
                        aria-label="btn-addSagra"
                        on:click={() => (showAddModal = false)}
                    ></button>
                </div>
                <div class="modal-body">
                    <form on:submit|preventDefault={addSagra}>
                        <div class="mb-3">
                            <label for="nome" class="form-label">Nome*</label>
                            <input
                                type="text"
                                class="form-control"
                                id="nome"
                                bind:value={formData.nome}
                                required
                            />
                        </div>
                        <div class="mb-3">
                            <label for="descrizione" class="form-label">Descrizione*</label>
                            <textarea
                                class="form-control"
                                id="descrizione"
                                rows="3"
                                bind:value={formData.descrizione}
                                required
                            ></textarea>
                        </div>
                        <div class="mb-3">
                            <label for="luogo" class="form-label">Luogo*</label>
                            <input
                                type="text"
                                class="form-control"
                                id="luogo"
                                bind:value={formData.luogo}
                                required
                            />
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="dataInizio" class="form-label">Data Inizio*</label>
                                <input
                                    type="date"
                                    class="form-control"
                                    id="dataInizio"
                                    bind:value={formData.dataInizio}
                                    required
                                />
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="dataFine" class="form-label">Data Fine*</label>
                                <input
                                    type="date"
                                    class="form-control"
                                    id="dataFine"
                                    bind:value={formData.dataFine}
                                    required
                                />
                            </div>
                        </div>
                        <div class="mb-3">
                            <label for="immagine" class="form-label">URL Immagine</label>
                            <input
                                type="url"
                                class="form-control"
                                id="immagine"
                                bind:value={formData.immagine}
                            />
                            <div class="form-text">
                                Inserisci un URL valido per l'immagine della sagra
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button
                                type="button"
                                class="btn btn-secondary"
                                on:click={() => (showAddModal = false)}
                            >Annulla</button>
                            <button type="submit" class="btn btn-submit">
                                <span class="save-icon">💾</span> Salva
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
{/if}

<!-- Modal Modifica Sagra -->
{#if showEditModal && currentSagra}
    <div class="modal-overlay">
        <div class="modal-wrapper">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Modifica Sagra</h5>
                    <button
                        type="button"
                        class="btn-close"
                        aria-label="btn-editSagra"
                        on:click={() => (showEditModal = false)}
                    ></button>
                </div>
                <div class="modal-body">
                    <form on:submit|preventDefault={updateSagra}>
                        <div class="mb-3">
                            <label for="edit-nome" class="form-label">Nome*</label>
                            <input
                                type="text"
                                class="form-control"
                                id="edit-nome"
                                bind:value={formData.nome}
                                required
                            />
                        </div>
                        <div class="mb-3">
                            <label for="edit-descrizione" class="form-label">Descrizione*</label>
                            <textarea
                                class="form-control"
                                id="edit-descrizione"
                                rows="3"
                                bind:value={formData.descrizione}
                                required
                            ></textarea>
                        </div>
                        <div class="mb-3">
                            <label for="edit-luogo" class="form-label">Luogo*</label>
                            <input
                                type="text"
                                class="form-control"
                                id="edit-luogo"
                                bind:value={formData.luogo}
                                required
                            />
                        </div>
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <label for="edit-dataInizio" class="form-label">Data Inizio*</label>
                                <input
                                    type="date"
                                    class="form-control"
                                    id="edit-dataInizio"
                                    bind:value={formData.dataInizio}
                                    required
                                />
                            </div>
                            <div class="col-md-6 mb-3">
                                <label for="edit-dataFine" class="form-label">Data Fine*</label>
                                <input
                                    type="date"
                                    class="form-control"
                                    id="edit-dataFine"
                                    bind:value={formData.dataFine}
                                    required
                                />
                            </div>
                        </div>
                        <div class="mb-3">
                            <label for="edit-immagine" class="form-label">URL Immagine</label>
                            <input
                                type="url"
                                class="form-control"
                                id="edit-immagine"
                                bind:value={formData.immagine}
                            />
                            <div class="form-text">
                                Inserisci un URL valido per l'immagine della sagra
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button
                                type="button"
                                class="btn btn-secondary"
                                on:click={() => (showEditModal = false)}
                            >Annulla</button>
                            <button type="submit" class="btn btn-submit">
                                <span class="save-icon">💾</span> Aggiorna
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
{/if}

<!-- Modal Elimina Sagra -->
{#if showDeleteModal && currentSagra}
    <div class="modal-overlay">
        <div class="modal-wrapper delete-modal">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Conferma Eliminazione</h5>
                    <button
                        type="button"
                        class="btn-close"
                        aria-label="btn-deleteSagra"
                        on:click={() => (showDeleteModal = false)}
                    ></button>
                </div>
                <div class="modal-body delete-modal-body">
                    <div class="delete-warning-icon">
                        <span class="warning-icon">⚠️</span>
                    </div>
                    <h4 class="delete-title">
                        {#if currentSagra.nomeSagra || currentSagra.nome}
                            Stai per eliminare la sagra "{currentSagra.nomeSagra || currentSagra.nome}"
                        {:else}
                            Stai per eliminare questa sagra
                        {/if}
                    </h4>
                    <div class="delete-warning">
                        <p>
                            Questa azione è <strong>irreversibile</strong> e comporterà anche
                            l'eliminazione di tutti gli eventi associati.
                        </p>
                        <p>
                            Sei sicuro di voler procedere?
                        </p>
                    </div>
                </div>
                <div class="modal-footer delete-modal-footer">
                    <button
                        type="button"
                        class="btn btn-secondary"
                        on:click={() => (showDeleteModal = false)}
                    >Annulla</button>
                    <button
                        type="button"
                        class="btn btn-delete-confirm"
                        on:click={deleteSagra}
                    >
                        <span class="trash-icon">🗑️</span> Elimina Sagra
                    </button>
                </div>
            </div>
        </div>
    </div>
{/if}