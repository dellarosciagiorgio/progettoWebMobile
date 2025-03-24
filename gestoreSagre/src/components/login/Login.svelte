<script>
    let selectedOption = '';
    const options = ['acquirente', 'organizzatore', 'admin'];
    function handleChange(event) {
        selectedOption = event.target.value;
    }
    let username = '';
    let password = '';
    function handleSubmit(event) {
        event.preventDefault();
    }
    function linkHomepage() {
        window.location.href = '../';
    }
</script>

<select id='ruolo-select' value={selectedOption} on:change={handleChange}>
    <option value='' disabled selected hidden>ruolo</option>
    {#each options as option}
        <option value={option}>{option}</option>
    {/each}
</select>

{#if selectedOption}
    <form on:submit={handleSubmit}>
        <h2><u>{selectedOption}</u></h2>
        <div>
            <label for='username'>username:</label>
            <input type='text' id='username' bind:value={username} required/>
        </div>
        <div>
            <label for='password'>password:</label>
            <input type='password' id='password' bind:value={password} required/>
        </div>
        <button type='submit' on:click={linkHomepage}>accedi</button>
    </form>
{/if}