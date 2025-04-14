<script>
    import Eventi from "../../Eventi/eventi.svelte";
    import { onMount } from 'svelte';

    export let id;

    onMount(() => {
        id = new URLSearchParams(window.location.search).get('id');
    });
</script>

id sagra: {id}

<Eventi idSagra={new URLSearchParams(window.location.search).get('id')} percorso={''}/>