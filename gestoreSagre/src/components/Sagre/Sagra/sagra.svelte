<script>
    import Eventi from "../../Eventi/eventi.svelte";
    import { onMount } from 'svelte';
    import { getData } from "../../../lib/get.js";

    let id;
    let error = null;
    let valutazione = null;
    
    onMount(async () => {
        let data = await getData(`sagra/ratingbyid/${new URLSearchParams(window.location.search).get('id')}`);
        data ? valutazione = data.data : error = "Errore nel recupero dei dati.";
    });
</script>

Valutazione: {valutazione}

<Eventi idSagra={new URLSearchParams(window.location.search).get('id')} percorso={''}/>