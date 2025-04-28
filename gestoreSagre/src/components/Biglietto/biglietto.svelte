
<script>
    import "./biglietto.css";
    import { onMount } from "svelte";
    import { getDataAuth } from "../../lib/get.js";
    
    let biglietti = [];
    let loading = true;
    let error = "";

    onMount(async () => {
    try {
        let data = await getDataAuth("biglietto");
        if (data)
            console.log(biglietti = data.data); // <-- correggi qui
        else
            error = "Errore nel recupero dei dati.";
    } catch (err) {
        console.error("Errore:", err);
        error = "Errore nel recupero dei dati.";
    } finally {
        loading = false;
    }
});

</script>

<div class="container biglietti-container my-5">
    <h1 class="text-center mb-4">I Miei Biglietti</h1>
    
    {#if loading}
        <div class="loading-container">
            <div class="spinner-border text-warning" role="status">
                <span class="visually-hidden">Caricamento...</span>
            </div>
            <p class="mt-3">Caricamento biglietti in corso...</p>
        </div>
    {:else if error}
        <div class="alert alert-danger" role="alert">
            <i class="bi bi-exclamation-triangle-fill me-2"></i> {error}
        </div>
    {:else if biglietti.length === 0}
        <div class="no-tickets-container">
            <div class="empty-ticket">
                <i class="bi bi-ticket-perforated"></i>
                <p>Non hai ancora biglietti acquistati</p>
                <a href="/Eventi/EventiFuturi" class="btn btn-primary mt-3">Esplora gli eventi</a>
            </div>
        </div>
    {:else}
        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            {#each biglietti as biglietto}
                <div class="col">
                    <div class="ticket-card">
                        <div class="ticket-header">
                            <div class="ticket-badge">#{biglietto.idBiglietto}</div>
                            <h3 class="ticket-title">{biglietto.nomeEvento || 'Evento'}</h3>
                            <p class="ticket-date">{new Date(biglietto.dataEvento || Date.now()).toLocaleDateString('it-IT', { 
                                day: 'numeric', 
                                month: 'long', 
                                year: 'numeric',
                                hour: '2-digit',
                                minute: '2-digit'
                            })}</p>
                        </div>
                        <div class="ticket-body">
                            <div class="ticket-info">
                                <div class="info-item">
                                    <span class="info-label">Tipo:</span>
                                    <span class="info-value">{biglietto.tipoBiglietto || 'Standard'}</span>
                                </div>
                                <div class="info-item">
                                    <span class="info-label">Posto:</span>
                                    <span class="info-value">{biglietto.posto || 'Non assegnato'}</span>
                                </div>
                                <div class="info-item">
                                    <span class="info-label">Prezzo:</span>
                                    <span class="info-value">€{biglietto.prezzo || '0.00'}</span>
                                </div>
                            </div>
                            <div class="ticket-qr">
                                <img src={`https://api.qrserver.com/v1/create-qr-code/?size=100x100&data=TICKET-${biglietto.idBiglietto}`} alt="QR Code" />
                            </div>
                        </div>
                        <div class="ticket-footer">
                            <div class="ticket-location">
                                <i class="bi bi-geo-alt-fill"></i>
                                <span>{biglietto.luogo || 'Luogo non specificato'}</span>
                            </div>
                        </div>
                    </div>
                </div>
            {/each}
        </div>
    {/if}
</div>