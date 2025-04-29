<script>
	import "bootstrap/dist/css/bootstrap.min.css";
	import "./registrazione.css";
	import { isAuthenticated, userEmail } from '$lib/stores';
	import { setCookie } from '$lib/cookies';

	let nome = "";
	let cognome = "";
	let email = "";
	let password = "";
	let confermaPassword = "";
	let selectedRole = "acquirente";
	let accettoTermini = false;

	const handleRegistration = async () => {
		if (password !== confermaPassword) {
			alert("Le password non coincidono!");
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
			if (response.ok)
				alert("Registrazione avvenuta con successo!");
				//todo auto-login
			else
				alert(`Errore: ${responseJson.message || "Registrazione fallita"}`);
		} catch (error) {
			alert("Errore di connessione. Riprova più tardi.");
		}
	};
</script>

<div class="container-fluid min-vh-100 d-flex align-items-center justify-content-center registration-background">
	<div class="row w-100 justify-content-center">
		<div class="col-md-8 col-lg-7 col-xl-6">
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
							<img src="/logo.png" alt="Logo" class="img-fluid logo-image"/>
							<div class="logo-background"></div>
						</div>
						<h4 class="fw-bold text-dark mb-1">Crea il tuo account</h4>
						<p class="text-muted mb-4">Inserisci i tuoi dati per registrarti</p>
					</div>
					<form on:submit|preventDefault={handleRegistration}>
						<div class="form-floating mb-4">
							<select class="form-select border-0 bg-light form-input-shadow" id="role" bind:value={selectedRole}>
								<option value="acquirente">Acquirente</option>
								<option value="organizzatore">Organizzatore</option
								>
								<option value="amministratore">Amministratore</option>
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
							<label for="password" class="text-muted">Password</label>
						</div>
						<div class="form-floating mb-4">
							<input type="password" class="form-control border-0 bg-light form-input-shadow" id="confermaPassword" placeholder="Conferma Password" bind:value={confermaPassword} required/>
							<label for="confermaPassword" class="text-muted">Conferma Password</label>
						</div>
						<div class="form-check mb-4">
							<input class="form-check-input" type="checkbox" bind:checked={accettoTermini} id="accettoTermini" required/>
							<label class="form-check-label text-muted" for="accettoTermini">
								Accetto i
								<button class="btn btn-link text-decoration-none p-0" on:click={() => (window.location.href =
											"/Registrazione/TermsAndPolicy")}>Termini e Condizioni</button>
								e la
								<button class="btn btn-link text-decoration-none p-0" on:click={() => (window.location.href = "/Registrazione/TermsAndPolicy")}>Privacy Policy</button>
							</label>
						</div>
						<button type="submit" class="btn btn-lg w-100 text-white mb-4 py-3 register-button">Registrati</button>
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
		</div>
	</div>
</div>