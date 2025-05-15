<script>
	import "bootstrap/dist/css/bootstrap.min.css";
	import "./registrazione.css";
	import { isAuthenticated, userEmail } from '$lib/stores';
	import { setCookie } from '$lib/cookies';
	import { onMount } from 'svelte';

	let nome = "";
	let cognome = "";
	let email = "";
	let password = "";
	let confermaPassword = "";
	let selectedRole = "acquirente";
	let accettoTermini = false;
	let isLoading = false;
	let registrationSuccess = false;
	let errorMessage = "";

	// Timer per il redirect
	let redirectTimer = 3;
	let redirectInterval;

	const handleRegistration = async () => {
		// Reset errori precedenti
		errorMessage = "";
		isLoading = true;
		
		// Controllo lunghezza della password (minimo 8 caratteri)
		if (password.length < 8) {
			errorMessage = "La password deve essere di almeno 8 caratteri!";
			isLoading = false;
			return;
		}
		
		if (password !== confermaPassword) {
			errorMessage = "Le password non coincidono!";
			isLoading = false;
			return;
		}
		
		const userData = {
			nome,
			cognome,
			email,
			password,
			selectedRole,
			accettoTermini
		};
		
		try {
			const endpoint = "acquirente";
			const response = await fetch(`${import.meta.env.VITE_API_URL}${endpoint}`, {
				method: "POST",
				headers: {
					"Content-Type": "application/json",
				},
				body: JSON.stringify(userData),
			});
			
			const responseJson = await response.json();
			
			if (response.ok) {
				// Registrazione avvenuta con successo!
				registrationSuccess = true;
				
				// Avvia il conteggio per il redirect
				redirectInterval = setInterval(() => {
					redirectTimer -= 1;
					if (redirectTimer <= 0) {
						clearInterval(redirectInterval);
						window.location.href = "/LogIn";
					}
				}, 1000);
			} else {
				errorMessage = responseJson.message || "Registrazione fallita. Riprova.";
			}
		} catch (error) {
			errorMessage = "Errore di connessione. Riprova più tardi.";
			console.error("Errore durante la registrazione:", error);
		} finally {
			isLoading = false;
		}
	};
	
	// Pulizia timer in caso di smontaggio del componente
	onMount(() => {
		return () => {
			if (redirectInterval) {
				clearInterval(redirectInterval);
			}
		};
	});
</script>

<div class="container-fluid min-vh-100 d-flex align-items-center justify-content-center registration-background">
	<div class="row w-100 justify-content-center">
		<div class="col-md-8 col-lg-7 col-xl-6">
			{#if !registrationSuccess}
				<div class="card border-0 shadow-lg rounded-4 overflow-hidden">
					<div class="card-header text-white p-0">
						<div class="text-center py-4 header-gradient">
							<h3 class="mb-0 fw-bold">Sagre Italiane</h3>
							<p class="mb-0">Scopri e organizza eventi gastronomici</p>
						</div>
					</div>
					<div class="card-body p-4 p-md-5">
						<div class="text-center mb-4">
							<div class="mb-3 position-relative d-inline-block">
								<img src="/logo.png" alt="Logo" class="img-fluid logo-image" />
								<div class="logo-background"></div>
							</div>
							<h4 class="fw-bold text-dark mb-1">Crea il tuo account</h4>
							<p class="text-muted mb-4">Inserisci i tuoi dati per registrarti</p>
						</div>
						
						{#if errorMessage}
							<div class="alert alert-danger mb-4" role="alert">
								{errorMessage}
							</div>
						{/if}
						
						<form on:submit|preventDefault={handleRegistration}>
							<div class="form-floating mb-4">
								<select class="form-select border-0 bg-light form-input-shadow" id="role" bind:value={selectedRole}>
									<option value="acquirente">Acquirente</option>
									<option value="organizzatore">Organizzatore</option>
								</select>
								<label for="role" class="text-muted">Registrati come</label>
							</div>
							<div class="row g-3 mb-3">
								<div class="col-md-6">
									<div class="form-floating">
										<input type="text" class="form-control border-0 bg-light form-input-shadow" id="nome" placeholder="Nome" bind:value={nome} required/>
										<label for="nome" class="text-muted">Nome</label>
									</div>
								</div>
								<div class="col-md-6">
									<div class="form-floating">
										<input type="text" class="form-control border-0 bg-light form-input-shadow" id="cognome" placeholder="Cognome" bind:value={cognome} required/>
										<label for="cognome" class="text-muted">Cognome</label>
									</div>
								</div>
							</div>
							<div class="form-floating mb-3">
								<input type="email" class="form-control border-0 bg-light form-input-shadow" id="email" placeholder="nome@esempio.com" bind:value={email} required/>
								<label for="email" class="text-muted">Email</label>
							</div>
							<div class="form-floating mb-3">
								<input type="password" class="form-control border-0 bg-light form-input-shadow" id="password" placeholder="Password" bind:value={password} required/>
								<label for="password" class="text-muted">Password (minimo 8 caratteri)</label>
							</div>
							<div class="form-floating mb-4">
								<input type="password" class="form-control border-0 bg-light form-input-shadow" id="confermaPassword" placeholder="Conferma Password" bind:value={confermaPassword} required/>
								<label for="confermaPassword" class="text-muted">Conferma Password</label>
							</div>
							<div class="form-check mb-4">
								<input class="form-check-input" type="checkbox" bind:checked={accettoTermini} id="accettoTermini" required/>
								<label class="form-check-label text-muted" for="accettoTermini">
									Accetto i
									<button class="btn btn-link text-decoration-none p-0" on:click={() => (window.location.href = "/Registrazione/TermsAndPolicy")}>Termini e Condizioni</button>
									e la
									<button class="btn btn-link text-decoration-none p-0" on:click={() => (window.location.href = "/Registrazione/TermsAndPolicy")}>Privacy Policy</button>
								</label>
							</div>
							<button type="submit" class="btn btn-lg w-100 text-white mb-4 py-3 register-button" disabled={isLoading}>
								{#if isLoading}
									<span class="spinner-border spinner-border-sm me-2" role="status" aria-hidden="true"></span>
									Registrazione in corso...
								{:else}
									Registrati
								{/if}
							</button>
						</form>
						<div class="d-flex align-items-center my-4">
							<div class="flex-grow-1 border-bottom"></div>
							<div class="px-3 text-muted small">oppure</div>
							<div class="flex-grow-1 border-bottom"></div>
						</div>
						<div class="text-center">
							<p class="mb-0">Hai già un account?</p>
							<button class="btn btn-link text-decoration-none p-0" on:click={() => (window.location.href = "/LogIn")}>Accedi ora</button>
						</div>
					</div>
					<div class="card-footer bg-white text-center p-3 border-0">
						<p class="text-muted small mb-0">© 2025 Sagre Italiane - Tutti i diritti riservati</p>
					</div>
				</div>
			{:else}
				<!-- Schermata di successo -->
				<div class="card border-0 shadow-lg rounded-4 overflow-hidden success-card">
					<div class="card-body p-5 text-center">
						<div class="success-icon-wrapper mb-4">
							<i class="bi bi-check-circle-fill success-icon"></i>
						</div>
						<h3 class="fw-bold mb-3">Registrazione completata!</h3>
						<p class="mb-4">Benvenuto, {nome}! Il tuo account è stato creato con successo.</p>
						<p class="text-muted mb-4">Verrai reindirizzato alla pagina di login tra <span class="fw-bold">{redirectTimer}</span> secondi...</p>
						<button class="btn btn-lg login-now-button" on:click={() => window.location.href = "/LogIn"}>
							Accedi ora
						</button>
					</div>
				</div>
			{/if}
		</div>
	</div>
</div>