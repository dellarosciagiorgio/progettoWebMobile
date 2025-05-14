<script>
    import { onMount } from "svelte";
    import { getData } from "../../lib/get.js";
    import "bootstrap/dist/css/bootstrap.min.css";
    import "./sagre.css";

    let sagre = [];
    let loading = true;
    let error = "";

    onMount(async () => {
        try {
            let data = await getData("sagre");
            if (data)
                console.log(sagre = data.data);
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

<div class="container-fluid py-5 page-background">
    <div class="container">
        <div class="row justify-content-center mb-5">
            <div class="col-lg-8 text-center">
                <h2 class="fw-bold mb-3 title-color">Le Nostre Sagre</h2>
                <p class="text-muted">Scopri tutte le sagre disponibili</p>
                <div class="d-inline-block position-relative mb-4">
                    <div class="logo-background"></div>
                    <img src="/logo.png" alt="Logo" class="img-fluid logo-image" />
                </div>
            </div>
        </div>

        {#if loading}
            <div class="d-flex justify-content-center my-5">
                <div class="spinner-border spinner-loading" role="status">
                    <span class="visually-hidden">Caricamento...</span>
                </div>
            </div>
        {:else if error}
            <div class="alert alert-danger text-center shadow-sm rounded-3" role="alert">
                <i class="bi bi-exclamation-triangle-fill me-2"></i> {error}
            </div>
        {:else if sagre.length === 0}
            <div class="text-center py-5">
                <div class="mb-4">
                    <i class="bi bi-calendar-x empty-icon"></i>
                </div>
                <h4 class="text-muted">Nessuna sagra disponibile</h4>
                <p class="text-muted">Non ci sono sagre registrate al momento.</p>
            </div>
        {:else}
            <div class="row g-4">
                {#each sagre as singolaSagra}
                    <div class="col-sm-6 col-lg-4">
                        <div class="card border-0 h-100 shadow-sm rounded-4 overflow-hidden hover-scale">
                            <div class="position-relative">
                                <img src="/imgs/sagra.jpg" class="card-img-top sagra-image" alt="Immagine della sagra">
                                <div class="image-overlay">
                                    <h5 class="card-title fw-bold text-white mb-0">{singolaSagra.sagra.nomeSagra}</h5>
                                </div>
                            </div>
                            <div class="card-body p-4">
                                <p class="card-text text-muted mb-4">{singolaSagra.sagra.descrizione}</p>
                                <div class="text-end">
                                    <a href='/Sagre/Sagra?id={singolaSagra.sagra.idSagra}' class="btn rounded-pill px-4 details-button">
                                        Dettagli
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                {/each}
            </div>
        {/if}
    </div>
</div>