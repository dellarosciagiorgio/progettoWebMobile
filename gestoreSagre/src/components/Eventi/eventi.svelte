<script>
    import { onMount } from "svelte";
    import { getData } from "../../lib/get.js";

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

{#if loading}
	 <div class="d-flex justify-content-center my-5">
		<div class="spinner-border text-primary" role="status" style="width: 3rem; height: 3rem;">
		  <span class="visually-hidden">Caricamento...</span>
		</div>
	  </div>
{:else if error}
    <p style="color: red;">{error}</p>
{:else}
    <div class="container mt-4">
        <div class="row">
            {#each eventi as evento}
                <div class="col-md-4 mb-4">
                    <div class="card" style="width: 100%;">
                        <img src="/imgs/evento.png" class="card-img-top" alt="Immagine dell'evento">
                        <div class="card-body">
                            <h5 class="card-title">{evento.dataEvento.split('T')[0]}</h5>
                            <p class="card-text">{evento.informazioniAggiuntive}</p>
                        </div>
                    </div>
                </div>
            {/each}
        </div>
    </div>
{/if}