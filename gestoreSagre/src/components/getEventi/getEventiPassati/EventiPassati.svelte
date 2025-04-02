<!-- <script>
    import jsonData from "../../../../../responses/GetEventi.json";
    const today = new Date().toISOString().split("T")[0];
    let eventiFuturi = [];
    if (jsonData && jsonData.data && Array.isArray(jsonData.data)) {
        eventiFuturi = jsonData.data.filter(
            (evento) => evento.dataEvento.split("T")[0] >= today,
        );
    }
</script>

<div>
    {#if eventiFuturi.length > 0}
        {#each eventiFuturi as evento}
            <div>
                <h3>ID evento: {evento.idEvento}</h3>
                <p>ID sagra: {evento.idSagra}</p>
                <p>Data evento: {evento.dataEvento.split("T")[0]}</p>
                <p>Informazioni aggiuntive: {evento.informazioniAggiuntive}</p>
                <p>ID stocks biglietto: {evento.idStocksBiglietto === null ? "N/A" : evento.idStocksBiglietto}</p>
                <hr />
            </div>
        {/each}
    {:else}
        <p>Nessun evento futuro presente.</p>
    {/if}
</div> -->


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
                {#if new Date(evento.dataEvento) < new Date()}
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