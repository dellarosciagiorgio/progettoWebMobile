<script>
    import { onMount } from 'svelte';
    let email = "";
    let password = "";
    let role = "";
    let expiration = null;
    let loginSuccess = false;
    let errorMessage = "";

    function setCookie(name, value, days) {
        let expires = "";
        if (days) {
            const date = new Date();
            // Calculate expiry date based on 'days' from now
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        // Construct the cookie string
        // Added Secure flag because your API is HTTPS
        // Added SameSite=Strict for better security (adjust to Lax if needed)
        // Added path=/ to make it available across the site
        document.cookie = name + "=" + (value || "") + expires + "; path=/; Secure; SameSite=Strict";
        console.log(`Cookie set: ${name}`);
        console.log(`Document cookie: ${document.cookie}`);
    }

    // Function to get a cookie (useful for checking if already logged in)
    function getCookie(name) {
        const nameEQ = name + "=";
        const ca = document.cookie.split(';');
        for(let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0)==' ')
				c = c.substring(1,c.length);
            if (c.indexOf(nameEQ) == 0)
				return c.substring(nameEQ.length,c.length);
        }
        return null;
    }

    // optional: check for token on component mount
    // onMount(() => {
    //     const existingtoken = getCookie("authtoken");
    //     existingtoken ? console.log("user already has a token:", existingtoken) : loginSuccess = true;
    // });


    const handleLogin = async () => {
        loginSuccess = false;
        errorMessage = "";
        try {
            const response = await fetch("https://localhost:443/api/login", {
                method: "POST",
                headers: { "Content-Type": "application/json", },
                body: JSON.stringify({ email, password }),
            });

            const data = await response.json();

            if (!response.ok)
				throw new Error(data.message || "Login fallito: Status " + response.status);

            console.log("Login avvenuto con successo:", data);
            if (data.success && data.data) {
                const jwtToken = data.data;
                console.log(jwtToken);
                // Set the cookie named 'authToken'
                // Set expiration (e.g., 7 days). Adjust as needed.
                // You might want to decode the JWT to get its actual expiry ('exp' claim)
                // and set the cookie expiry accordingly, but 7 days is a common practice.
                setCookie("authToken", jwtToken, 7);
                loginSuccess = true;
            } else
                 throw new Error("Login response received, but token data is missing.");
        } catch (error) {
            console.error("Errore durante il login:", error);
            errorMessage = error.message || "Credenziali non valide o errore di connessione";
        }
    };

    const handleLogout = () => {
        // logout impostando la data di scadenza passata
        setCookie("authToken", "", -1);
        loginSuccess = false;
        email = "";
        password = "";
        console.log("User logged out, cookie deleted.");
		window.location.href = "/Home";
    };
</script>

<div>
    {#if !loginSuccess}
        <form on:submit|preventDefault={handleLogin}>
            <div>
                <label for="email">Email:</label>
                <input type="email" id="email" bind:value={email} required/>
            </div>
            <div>
                <label for="password">Password:</label>
                <input type="password" id="password" bind:value={password} required/>
            </div>
            <button type="submit">Login</button>
        </form>
        {#if errorMessage}
            <p>Error: {errorMessage}</p>
        {/if}
    {:else}
        <h2>Logged in!</h2>
        <p>Token stored in cookie.</p>
        <button on:click={handleLogout}>Logout</button>
    {/if}
</div>