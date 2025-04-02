<script>
    import { onMount } from "svelte";
    import { getData } from "../../../lib/get.js";

    let eventi = [];
    let loading = true;
    let error = "";

    onMount(async () => {
        const data = await getData("eventi");
        if (data) {
            eventi = data.data;
			console.log(eventi);
        } else {
            error = "Errore nel recupero dei dati.";
        }
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
                {#if new Date(evento.dataEvento) >= new Date()}
                    <div class="col-md-4 mb-4">
                        <div class="card" style="width: 100%;">
                            <img src="/imgs/evento.jpg" class="card-img-top" alt="Immagine dell'evento">
                            <div class="card-body">
                                <h5 class="card-title">{evento.dataEvento.split('T')[0]}</h5>
                                <p class="card-text">{evento.informazioniAggiuntive}</p>
                            </div>
                        </div>
                    </div>
                {/if}
            {/each}
        </div>
    </div>
{/if}