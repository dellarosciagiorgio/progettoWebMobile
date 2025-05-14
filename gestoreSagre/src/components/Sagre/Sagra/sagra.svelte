<script>
    import Eventi from "../../Eventi/eventi.svelte";
    import { onMount } from 'svelte';
    import { getData } from "../../../lib/get.js";

    let id;
    let error = null;
    let valutazione = null;
    

</script>

<Eventi idSagra={new URLSearchParams(window.location.search).get('id')} percorso={''}/>