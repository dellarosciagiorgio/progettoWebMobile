<script>
    import jsonData from '../../../../responses/GetEventi.json';
</script>

<div>
    {#if jsonData && jsonData.data && Array.isArray(jsonData.data)}
        {#each jsonData.data as evento}
        	<div>
          	    <h3>ID evento: {evento.idEvento}</h3>
          	    <p>ID sagra: {evento.idSagra}</p>
          	    <p>Data evento: {evento.dataEvento.split('T')[0]}</p>
          	    <p>Informazioni aggiuntive: {evento.informazioniAggiuntive}</p>
          	    <p>ID stocks biglietto: {evento.idStocksBiglietto === null ? 'N/A' : evento.idStocksBiglietto}</p>
          	    <hr/>
            </div>
        {/each}
    {:else}
        <p>Nessun evento presente.</p>
    {/if}
</div>