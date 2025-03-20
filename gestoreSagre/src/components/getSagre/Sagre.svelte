<script>
	import jsonData from '../../../../responses/GetSagre.json';
</script>

<div>
    {#if jsonData && jsonData.data && Array.isArray(jsonData.data)}
        {#each jsonData.data as sagra}
      		<div>
        		<h3>ID: {sagra.idSagra}</h3>
        		<p>Nome: {sagra.nomeSagra}</p>
        		<p>Descrizione: {sagra.descrizione}</p>
        		<p>ID feedbacks: {sagra.idFeedbacks === null ? 'N/A' : sagra.idFeedbacks}</p>
        		<p>ID sagrai: {sagra.idEventi === null ? 'N/A' : sagra.idEventi}</p>
        		<p>ID organizzatore: {sagra.idOrganizzatore === null ? 'N/A' : sagra.idOrganizzatore}</p>
        		<hr/>
      		</div>
    	{/each}
    {:else}
        <p>Nessuna sagra presente.</p>
    {/if}
</div>