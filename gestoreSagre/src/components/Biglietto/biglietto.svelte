<script>
    import { onMount } from "svelte";
    import { getDataAuth } from "../../lib/get.js";

    let biglietto = [];
    let loading = true;
    let error = "";

    onMount(async () => {
        try {
            let data = await getDataAuth("biglietto");
            if (data)
                console.log(biglietto = data.data);
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

{#if loading}
    <p>caricamento...</p>
{:else if error}
    <p>{error}</p>
{:else if biglietto.length === 0}
    <p>nessun biglietto disponibile</p>
{:else}
    {#each biglietto as biglietto}
        <p>{biglietto.idBiglietto}</p>
        <!-- <script>console.log("success: ", biglietto)</script> -->
    {/each}
{/if}