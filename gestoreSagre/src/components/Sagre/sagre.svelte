<script>
    import { onMount } from "svelte";
    import { getData } from "../../lib/get.js";
    import "bootstrap/dist/css/bootstrap.min.css";

    let sagre = [];
    let loading = true;
    let error = "";

    onMount(async () => {
        try {
            let data = await getData("sagre");
            if (data) {
                console.log(sagre = data.data);
            } else {
                error = "Errore nel recupero dei dati.";
            }
        } catch (err) {
            console.error("Errore:", err);
            error = "Errore nel recupero dei dati.";
        } finally {
            loading = false;
        }
    });
</script>

<div class="container-fluid py-5" style="background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);">
    <div class="container">
        <div class="row justify-content-center mb-5">
            <div class="col-lg-8 text-center">
                <h2 class="fw-bold mb-3" style="color: #d62828;">Le Nostre Sagre</h2>
                <p class="text-muted">Scopri tutte le sagre disponibili</p>
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
        {:else if sagre.length === 0}
            <div class="text-center py-5">
                <div class="mb-4">
                    <i class="bi bi-calendar-x" style="font-size: 3rem; color: #6c757d;"></i>
                </div>
                <h4 class="text-muted">Nessuna sagra disponibile</h4>
                <p class="text-muted">Non ci sono sagre registrate al momento.</p>
            </div>
        {:else}
            <div class="row g-4">
                {#each sagre as sagra}
                    <div class="col-sm-6 col-lg-4">
                        <div class="card border-0 h-100 shadow-sm rounded-4 overflow-hidden hover-scale" style="transition: all 0.3s ease;">
                            <div class="position-relative">
                                <img src="/imgs/sagra.jpg" class="card-img-top" alt="Immagine della sagra" style="height: 200px; object-fit: cover;">
                                <div class="position-absolute bottom-0 start-0 w-100 p-3" style="background: linear-gradient(to top, rgba(0,0,0,0.7), rgba(0,0,0,0));">
                                    <h5 class="card-title fw-bold text-white mb-0">{sagra.nomeSagra}</h5>
                                </div>
                            </div>
                            <div class="card-body p-4">
                                <p class="card-text text-muted mb-4">{sagra.descrizione}</p>
                                <div class="text-end">
                                    <a href='/Sagre/Sagra?id={sagra.idSagra}' class="btn rounded-pill px-4" style="background-color: #f77f00; color: white;">
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

<style>
    .hover-scale:hover {
        transform: translateY(-5px);
        box-shadow: 0 10px 20px rgba(0,0,0,0.1) !important;
    }
</style>