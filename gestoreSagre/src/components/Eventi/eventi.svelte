<script>
	import Eventi from './eventi.svelte';
	import { onMount } from "svelte";
	import { getData } from "../../lib/get.js";
	import "bootstrap/dist/css/bootstrap.min.css";
	import "./eventi.style.css";

	export let idSagra = "";
	export let percorso = "bytime?checkByFuture=";
	export let eventiFuturi = "";

	let eventi = [];
	let loading = true;
	let error = "";

	onMount(async () => {
		let data = await getData(`eventi/${idSagra}${percorso}${eventiFuturi}`);
		data ? eventi = data.data : error = "Errore nel recupero dei dati.";
		loading = false;
	});
</script>

<div class="container-fluid py-5 bg-gradient-custom">
	<div class="container">
		<div class="row justify-content-center mb-5">
			<div class="col-lg-8 text-center">
				<h2 class="fw-bold mb-3 text-title-custom">Eventi</h2>
				<div class="d-inline-block position-relative mb-4">
					<div class="logo-background-circle"></div>
					<img src="/logo.png" alt="Logo" class="img-fluid logo-img" />
				</div>
			</div>
		</div>

		{#if loading}
			<div class="d-flex justify-content-center my-5">
				<div class="spinner-border spinner-custom" role="status">
					<span class="visually-hidden">Caricamento...</span>
				</div>
			</div>
		{:else if error}
			<div class="alert alert-danger text-center shadow-sm rounded-3" role="alert">
				<i class="bi bi-exclamation-triangle-fill me-2"></i> {error}
			</div>
		{:else if eventi.length === 0}
			<div class="text-center py-5">
				<div class="mb-4">
					<i class="bi bi-calendar-x icon-empty"></i>
				</div>
				<h4 class="text-muted">Nessun evento disponibile</h4>
				<p class="text-muted">Non ci sono eventi programmati per questa sagra.</p>
			</div>
		{:else}
			<div class="row g-4">
				{#each eventi as evento}
					<div class="col-sm-6 col-lg-4">
						<div class="card border-0 h-100 shadow-sm rounded-4 overflow-hidden hover-scale card-transition">
							<div class="position-relative">
								<img src="/imgs/evento.png" class="card-img-top event-img" alt="Immagine dell'evento" />
								<div class="position-absolute top-0 end-0 m-3 p-2 rounded-pill event-date-box">
									<span class="fw-bold event-date-text">
										{new Date(evento.dataEvento).toLocaleDateString('it-IT', {day: '2-digit', month: '2-digit'})}
									</span>
								</div>
							</div>
							<div class="card-body p-4">
								<h5 class="card-title fw-bold mb-3 text-dark">
									{new Date(evento.dataEvento).toLocaleDateString('it-IT', {weekday: 'long', day: 'numeric', month: 'long', year: 'numeric'})}
								</h5>
								<p class="card-text text-muted mb-4">{evento.informazioniAggiuntive}</p>
								<div class="d-flex justify-content-between align-items-center">
									<span class="badge rounded-pill px-3 py-2 badge-custom">
										<i class="bi bi-clock me-1"></i>
										{new Date(evento.dataEvento).toLocaleTimeString('it-IT', {hour: '2-digit', minute: '2-digit'})}
									</span>
									<button class="btn btn-sm rounded-pill px-3 btn-orange">Dettagli</button>
								</div>
							</div>
						</div>
					</div>
				{/each}
			</div>
		{/if}
	</div>
</div>
