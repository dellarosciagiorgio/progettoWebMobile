<script>
    let { children } = $props();
</script>

<nav>
    <a href="/">home</a>
    <a href="/getSagre">sagre</a>
    <a href="/getEventi">eventi</a>
    <a href="/getEventi/getEventiPassati">eventi passati</a>
    <a href="/getEventi/getEventiFuturi">eventi futuri</a>
    <a href="/login">login</a>
</nav>

{@render children()}

<style>
    :global(body) {
        background-color: #565;
    }
</style>