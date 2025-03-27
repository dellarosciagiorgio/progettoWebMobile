<script>
	import jsonData from '../../../../responses/GetSagre.json';

    import { onMount } from "svelte";
    import { getData } from "../../lib/api.js";

    let sagre = [];
    let loading = true;
    let error = "";

    onMount(async () => {
        const data = await getData("sagre"); // Assumendo che il tuo backend abbia un endpoint /items
        if (data) {
            sagre = data.data;
			console.log(sagre);
        } else {
            error = "Errore nel recupero dei dati.";
        }
        loading = false;
    });
</script>

{#if loading}
     <!-- Loading spinner -->
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
            {#each sagre as sagra}
                <div class="col-md-4 mb-4">
                    <div class="card" style="width: 100%;">
                        <img src="https://www.ristobusiness.it/wp-content/uploads/2018/08/Sagra.jpg" class="card-img-top" alt="Immagine della sagra">
                        <div class="card-body">
                            <h5 class="card-title">{sagra.nomeSagra}</h5>
                            <p class="card-text">{sagra.descrizione}</p>
                            <a href="" class="btn btn-primary">Dettagli</a>
                        </div>
                    </div>
                </div>
            {/each}
        </div>
    </div>
{/if}