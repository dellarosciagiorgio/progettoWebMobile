<script>
	import Eventi from './eventi.svelte';
    import { onMount } from "svelte";
    import { getData } from "../../lib/get.js";
    import "bootstrap/dist/css/bootstrap.min.css";

    export let idSagra = "";
    export let percorso = "bytime?checkByFuture=";
    export let eventiFuturi = "";

    let eventi = [];
    let loading = true;
    let error = "";

    onMount(async () => {
        let data = await getData(`eventi/${idSagra}${percorso}${eventiFuturi}`);
        data ? eventi = data.data : error = "Errore nel recupero dei dati.";
        loading = false;
    });
</script>

<div class="container-fluid py-5" style="background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);">
    <div class="container">
        <div class="row justify-content-center mb-5">
            <div class="col-lg-8 text-center">
                <h2 class="fw-bold mb-3" style="color: #d62828;">I miei eventi</h2>
                <div class="d-inline-block position-relative mb-4">
                    <div class="position-absolute" style="width: 100px; height: 100px; background-color: rgba(247, 127, 0, 0.1); border-radius: 50%; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 0;"></div>
                    <img src="/logo.png" alt="Logo" class="img-fluid" style="max-width: 80px; z-index: 1; position: relative;" />
                </div>
            </div>
        </div>

        {#if loading}
            <div class="d-flex justify-content-center my-5">
                <div class="spinner-border" role="status" style="width: 3rem; height: 3rem; color: #f77f00;">
                    <span class="visually-hidden">Caricamento...</span>
                </div>
            </div>
        {:else if error}
            <div class="alert alert-danger text-center shadow-sm rounded-3" role="alert">
                <i class="bi bi-exclamation-triangle-fill me-2"></i> {error}
            </div>
        {:else if eventi.length === 0}
            <div class="text-center py-5">
                <div class="mb-4">
                    <i class="bi bi-calendar-x" style="font-size: 3rem; color: #6c757d;"></i>
                </div>
                <h4 class="text-muted">Nessun evento disponibile</h4>
                <p class="text-muted">Non ci sono eventi programmati per questa sagra.</p>
            </div>
        {:else}
            <div class="row g-4">
                {#each eventi as evento}
                    <div class="col-sm-6 col-lg-4">
                        <div class="card border-0 h-100 shadow-sm rounded-4 overflow-hidden hover-scale" style="transition: all 0.3s ease;">
                            <div class="position-relative">
                                <img src="/imgs/evento.png" class="card-img-top" alt="Immagine dell'evento" style="height: 200px; object-fit: cover;">
                                <div class="position-absolute top-0 end-0 m-3 p-2 rounded-pill" style="background-color: rgba(255, 255, 255, 0.9);">
                                    <span class="fw-bold" style="color: #d62828;">{new Date(evento.dataEvento).toLocaleDateString('it-IT', {day: '2-digit', month: '2-digit'})}</span>
                                </div>
                            </div>
                            <div class="card-body p-4">
                                <h5 class="card-title fw-bold mb-3" style="color: #333;">{new Date(evento.dataEvento).toLocaleDateString('it-IT', {weekday: 'long', day: 'numeric', month: 'long', year: 'numeric'})}</h5>
                                <p class="card-text text-muted mb-4">{evento.informazioniAggiuntive}</p>
                                <div class="d-flex justify-content-between align-items-center">
                                    <span class="badge rounded-pill px-3 py-2" style="background-color: rgba(247, 127, 0, 0.1); color: #f77f00;">
                                        <i class="bi bi-clock me-1"></i>
                                        {new Date(evento.dataEvento).toLocaleTimeString('it-IT', {hour: '2-digit', minute: '2-digit'})}
                                    </span>
                                    <button class="btn btn-sm rounded-pill px-3" style="background-color: #f77f00; color: white;">Dettagli</button>
                                </div>
                            </div>
                        </div>
                    </div>
                {/each}
            </div>
        {/if}
    </div>
</div>

<style>
    .hover-scale:hover {
        transform: translateY(-5px);
        box-shadow: 0 10px 20px rgba(0,0,0,0.1) !important;
    }
</style>