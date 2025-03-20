<script>
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
</div>
