<script>
    import "bootstrap/dist/css/bootstrap.min.css";
    import { jwtDecode } from "jwt-decode";
    import "./login.css";
    import { onMount } from 'svelte';
    import { isAuthenticated, userEmail, role } from '$lib/stores';
    
    let emailInput = "";
    let password = "";
    let rememberMe = false;
    let loginSuccess = false;
    let errorMessage = "";
    let isLoading = false;
    
    function setCookie(name, value, days) {
        let expires = "";
        if (days) {
            const date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/; Secure; SameSite=Strict";
        console.log(`Cookie set: ${name}`);
        console.log(`Document cookie: ${document.cookie}`);
    }
    
    function getCookie(name) {
        const nameEQ = name + "=";
        const ca = document.cookie.split(';');
        for(let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0)==' ') c = c.substring(1,c.length);
            if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
        }
        return null;
    }
    
    const handleLogin = async () => {
        isLoading = true;
        loginSuccess = false;
        errorMessage = "";
        const endpoint = "login";
        
        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}${endpoint}`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email: emailInput, password }),
            });
            
            const data = await response.json();
            
            if (!response.ok) 
                throw new Error(data.message || "Login fallito: Status " + response.status);
                
            console.log("Login avvenuto con successo:", data);
            
            if (data.success && data.data) {
                const jwtToken = data.data;
                console.log(jwtToken);
                const decodedToken = jwtDecode(jwtToken);
                console.log(decodedToken);
                
                // Imposta il cookie per il token e l'email
                const cookieDuration = rememberMe ? 30 : 1;
                setCookie("authToken", jwtToken, cookieDuration);
                setCookie("userEmail", decodedToken.Email, cookieDuration);
                setCookie("userRole", decodedToken.Ruolo, cookieDuration);
                
                // Aggiorna gli store Svelte
                isAuthenticated.set(true);
                userEmail.set(emailInput);
                role.set(decodedToken.Ruolo); // Aggiungiamo lo store del ruolo
                
                // Aggiorna localStorage per sincronizzazione tra tab
                localStorage.setItem('authStatus', 'true');
                localStorage.setItem('userEmail', emailInput);
                localStorage.setItem('userRole', decodedToken.Ruolo); // Aggiungiamo il ruolo anche in localStorage
                
                loginSuccess = true;
                
                // Redirect automatico dopo 2 secondi
                setTimeout(() => {
                    window.location.href = "/Home";
                }, 2000);
            } else {
                throw new Error("Login response received, but token data is missing.");
            }
        } catch (error) {
            console.error("Errore durante il login:", error);
            errorMessage = error.message || "Credenziali non valide o errore di connessione";
        } finally {
            isLoading = false;
        }
    };
    
    const handleLogout = () => {
        setCookie("authToken", "", -1);
        setCookie("userEmail", "", -1);
        setCookie("userRole", "", -1); // Aggiungiamo la pulizia del cookie del ruolo
        loginSuccess = false;
        emailInput = "";
        password = "";
        
        // Aggiorna gli store
        isAuthenticated.set(false);
        userEmail.set('');
        role.set(''); // Aggiorniamo anche lo store del ruolo
        
        // Aggiorna localStorage
        localStorage.removeItem('authStatus');
        localStorage.removeItem('userEmail');
        localStorage.removeItem('userRole'); // Rimuoviamo anche il ruolo da localStorage
        
        console.log("User logged out, cookies deleted.");
        window.location.href = "/Home";
    };
    
    const goToHome = () => {
        window.location.href = "/Home";
    };
    
    // Check for token on component mount
    onMount(() => {
        const existingToken = getCookie("authToken");
        const savedEmail = getCookie("userEmail");
        const savedRole = getCookie("userRole");
        
        if (existingToken) {
            console.log("User already has a token");
            loginSuccess = true;
            
            // Aggiorna gli store se l'utente è già autenticato
            isAuthenticated.set(true);
            if (savedEmail) {
                userEmail.set(savedEmail);
                emailInput = savedEmail;
            }
            if (savedRole) {
                role.set(savedRole);
            }
        }
    });
</script>

<div class="login-container">
    {#if !loginSuccess}
        <div class="login-card">
            <div class="login-header">
                <h1>Sagre Online</h1>
                <p>Gestione Eventi e Sagre</p>
            </div>
            
            <div class="login-body">
                <div class="logo-container">
                    <img src="/logo.png" alt="Logo" class="img-fluid logo-image" />
                </div>
                
                <h2>Accedi</h2>
                <p class="subtitle">Inserisci le tue credenziali per accedere</p>
                
                {#if errorMessage}
                    <div class="alert-error">
                        {errorMessage}
                    </div>
                {/if}
                
                <form on:submit|preventDefault={handleLogin}>
                    <div class="form-group">
                        <input 
                            type="email" 
                            id="email" 
                            placeholder="Email" 
                            bind:value={emailInput} 
                            required
                        />
                    </div>
                    
                    <div class="form-group">
                        <input 
                            type="password" 
                            id="password" 
                            placeholder="Password" 
                            bind:value={password} 
                            required
                        />
                    </div>
                    
                    <div class="remember-me">
                        <label class="checkbox-container">
                            Ricordami
                            <input type="checkbox" bind:checked={rememberMe}>
                            <span class="checkmark"></span>
                        </label>
                    </div>
                    
                    <button type="submit" class="login-button" disabled={isLoading}>
                        {#if isLoading}
                            <span class="spinner"></span>
                        {/if}
                        {isLoading ? 'Accesso in corso...' : 'Accedi'}
                    </button>
                </form>
                
                <div class="divider">
                    <div class="divider-line"></div>
                    <span class="divider-text">oppure</span>
                    <div class="divider-line"></div>
                </div>
                
                <div class="register-section">
                    <p>Non hai un account?</p>
                    <a href="/Registrazione" class="register-link">Registrati</a>
                </div>
            </div>
            
            <div class="login-footer">
                <p>© 2024 Sagre Online - Tutti i diritti riservati</p>
            </div>
        </div>
    {:else}
        <div class="success-card">
            <div class="success-logo-container">
                <div class="success-logo-circle"></div>
                <img src="/logo.png" alt="Logo" class="img-fluid logo-image" />
            </div>
            
            <h2 class="success-title">Accesso Effettuato</h2>
            <p class="success-subtitle">Hai effettuato l'accesso con successo. Sarai reindirizzato automaticamente.</p>
        
        </div>
    {/if}
</div>