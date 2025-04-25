<script>
	import { onMount } from "svelte";
	import { isAuthenticated, userEmail } from "$lib/stores";
	import "./login.css"; 

	let email = "";
	let password = "";
	let loginSuccess = false;
	let rememberMe = false;
	let loginError = "";
	let isLoading = false;

	// URL del backend
	const API_URL = "https://localhost:443/api";

	// Funzione per impostare un cookie
	const setCookie = (name, value, days) => {
		let expires = "";
		if (days) {
			const date = new Date();
			date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
			expires = `; expires=${date.toUTCString()}`;
		}
		// Importante: imposta il path a "/" per rendere il cookie disponibile in tutto il sito
		document.cookie = `${name}=${encodeURIComponent(value)}${expires}; path=/`;
	};

	// Funzione per ottenere un cookie
	const getCookie = (name) => {
		const cookies = document.cookie.split(';');
		for (let i = 0; i < cookies.length; i++) {
			const cookie = cookies[i].trim();
			if (cookie.startsWith(name + '=')) {
				return decodeURIComponent(cookie.substring(name.length + 1));
			}
		}
		return null;
	};

	// Funzione per eliminare un cookie
	const deleteCookie = (name) => {
		document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
	};

	// Funzione per effettuare chiamate API
	const apiCall = async (endpoint, method = "GET", body = null) => {
		const headers = {
			"Content-Type": "application/json"
		};
		
		// Aggiungi token di autorizzazione se disponibile
		const token = getCookie("authToken");
		if (token) {
			headers["Authorization"] = `Bearer ${token}`;
		}
		
		const options = {
			method,
			headers,
			credentials: "same-origin"
		};
		
		if (body) {
			options.body = JSON.stringify(body);
		}
		
		try {
			const response = await fetch(`${API_URL}${endpoint}`, options);
			
			// Per errori HTTP, lanciamo un'eccezione
			if (!response.ok) {
				const errorData = await response.json().catch(() => ({}));
				throw new Error(errorData.message || `Errore ${response.status}`);
			}
			
			// Per risposte senza contenuto
			if (response.status === 204) {
				return null;
			}
			
			// parsing della risposta come JSON
			return await response.json();
		} catch (error) {
			console.error(`Errore chiamata API ${endpoint}:`, error);
			throw error;
		}
	};

	// Controlla se l'utente è già loggato all'avvio del componente
	onMount(async () => {
		const savedToken = getCookie("authToken");
		const savedEmail = getCookie("userEmail");
		
		if (savedToken && savedEmail) {
			try {
				// Verifica la validità del token
				await apiCall("/verify");
				
				console.log("Sessione esistente verificata");
				email = savedEmail;
				loginSuccess = true;
				// Aggiorna gli store
				isAuthenticated.set(true);
				userEmail.set(savedEmail);
				
				// Notifica altre schede del browser che lo stato di autenticazione è cambiato
				localStorage.setItem('authStatus', 'true');
				localStorage.setItem('userEmail', savedEmail);
			} catch (error) {
				console.error("Errore durante la verifica del token:", error);
				// Token non valido, elimina i cookie
				deleteCookie("authToken");
				deleteCookie("userEmail");
				deleteCookie("userRole");
				
				// Aggiorna lo stato di autenticazione
				isAuthenticated.set(false);
				userEmail.set('');
				
				// Notifica altre schede del browser
				localStorage.removeItem('authStatus');
				localStorage.removeItem('userEmail');
			}
		}
	});

	const handleLogin = async () => {
		try {
			loginError = "";
			isLoading = true;
			
			// Chiamata API reale
			const data = await apiCall("/login", "POST", { email, password, rememberMe });
			
			console.log("Login avvenuto con successo:", data);
			
			// Salva i dati nei cookie (se il server non li ha già impostati)
			if (data.token) {
				if (rememberMe) {
					const expirationDays = 7; // Cookie valido per 7 giorni
					setCookie("authToken", data.token, expirationDays);
					setCookie("userEmail", email, expirationDays);
					if (data.role) setCookie("userRole", data.role, expirationDays);
				} else {
					// Cookie di sessione (scade alla chiusura del browser)
					setCookie("authToken", data.token);
					setCookie("userEmail", email);
					if (data.role) setCookie("userRole", data.role);
				}
			}
			
			// Aggiorna gli store
			isAuthenticated.set(true);
			userEmail.set(email);
			
			// Notifica altre schede del browser che lo stato di autenticazione è cambiato
			localStorage.setItem('authStatus', 'true');
			localStorage.setItem('userEmail', email);
			
			loginSuccess = true;
		} catch (error) {
			console.error("Errore durante il login:", error);
			
			// Gestione personalizzata degli errori
			if (error.message === "Invalid credentials") {
				loginError = "Email o password non validi";
			} else if (error.message === "Account not verified") {
				loginError = "Account non verificato. Controlla la tua email.";
			} else {
				loginError = "Errore durante il login. Riprova più tardi.";
			}
		} finally {
			isLoading = false;
		}
	};

	const goToHome = () => {
		window.location.href = "/Home";
	};

	const logout = async () => {
		try {
			// Chiamata al server per invalidare il token
			await apiCall("/logout", "POST");
		} catch (error) {
			console.error("Errore durante il logout:", error);
		} finally {
			// Rimuovi i cookie indipendentemente dall'esito della chiamata
			deleteCookie("authToken");
			deleteCookie("userEmail");
			deleteCookie("userRole");
			
			// Aggiorna gli store
			isAuthenticated.set(false);
			userEmail.set('');
			
			// Notifica altre schede del browser
			localStorage.removeItem('authStatus');
			localStorage.removeItem('userEmail');
			
			loginSuccess = false;
			email = "";
			password = "";
		}
	};

	// Recupero password
	const forgotPassword = () => {
		window.location.href = "/RecuperaPassword";
	};
</script>

{#if loginSuccess}
	<!-- Schermata di successo -->
	<div class="login-container">
		<div class="success-card">
			<div class="success-logo-container">
				<div class="success-logo-circle"></div>
				<img src="/logo.png" alt="Successo" class="success-logo-image" />
			</div>
			<h2>Login avvenuto con successo!</h2>
			<p class="subtitle">Benvenuto su Sagre Italiane 🎉</p>
			<div class="button-group">
				<button class="login-button success-button" on:click={goToHome}>
					Vai alla Home
				</button>
			</div>
		</div>
	</div>
{:else}
	<!-- Schermata di login -->
	<div class="login-container">
		<div class="login-card">
			<div class="login-header">
				<h1>Sagre Italiane</h1>
				<p>Scopri e organizza eventi gastronomici</p>
			</div>
			<div class="login-body">
				<div class="logo-container">
					<img src="/logo.png" alt="Logo" class="logo-image" />
				</div>
				<h2>Benvenuto!</h2>
				<p class="subtitle">Accedi per continuare</p>
				
				{#if loginError}
					<div class="alert-error">
						{loginError}
					</div>
				{/if}
				
				<form on:submit|preventDefault={handleLogin}>
					<div class="form-group">
						<input
							type="email"
							id="email"
							placeholder="Email"
							bind:value={email}
							required
							disabled={isLoading}
						/>
					</div>
					<div class="form-group">
						<input
							type="password"
							id="password"
							placeholder="Password"
							bind:value={password}
							required
							disabled={isLoading}
						/>
					</div>
					<div class="remember-me">
						<label class="checkbox-container">
							<input 
								type="checkbox" 
								bind:checked={rememberMe}
								disabled={isLoading}
							/>
							<span class="checkmark"></span>
							Ricordami per 7 giorni
						</label>
					</div>
					<button
						type="submit"
						class="login-button"
						disabled={isLoading}
					>
						{#if isLoading}
							<span class="spinner"></span>
							Accesso in corso...
						{:else}
							Accedi
						{/if}
					</button>
				</form>
				
				<div class="divider">
					<div class="divider-line"></div>
					<div class="divider-text">oppure</div>
					<div class="divider-line"></div>
				</div>
				
				<div class="register-section">
					<p>Non hai ancora un account?</p>
					<a href="/Registrazione" class="register-link">
						Registrati ora
					</a>
				</div>
			</div>
			<div class="login-footer">
				<p>© 2025 Sagre Italiane - Tutti i diritti riservati</p>
			</div>
		</div>
	</div>
{/if}