<script>
    import { onMount } from "svelte";
    import "bootstrap/dist/css/bootstrap.min.css";
    import "./home.style.css";

    const sagreInEvidenza = [
        {
            idSagra: 1,
            nomeSagra: "Sagra della Porchetta",
            descrizione: "La tradizionale sagra dedicata alla porchetta, con eventi gastronomici e musica dal vivo."
        },
        {
            idSagra: 2,
            nomeSagra: "Sagra del Vino",
            descrizione: "Un evento dedicato ai migliori vini locali con degustazioni e laboratori per tutti."
        },
        {
            idSagra: 3,
            nomeSagra: "Sagra del Tartufo",
            descrizione: "Scopri i sapori unici del tartufo in questa imperdibile sagra gastronomica."
        }
    ];
</script>

<!-- Hero Section -->
<div class="container-fluid py-5 bg-hero-gradient">
    <div class="container py-5">
        <div class="row align-items-center">
            <div class="col-lg-6 mb-5 mb-lg-0">
                <h1 class="display-4 fw-bold mb-4 text-main">Benvenuto nel Mondo delle Sagre</h1>
                <p class="lead mb-4 text-secondary-custom">
                    Scopri le tradizioni culinarie e gli eventi culturali della nostra regione. Partecipa alle sagre locali e vivi un'esperienza unica.
                </p>
                <div class="d-flex gap-3">
                    <a href="/Sagre" class="btn btn-lg rounded-pill px-4 btn-orange">Esplora Sagre</a>
                </div>
            </div>
            <div class="col-lg-6 text-center">
                <div class="position-relative hero-logo-container">
                    <div class="hero-logo-circle"></div>
                    <img src="/logo.png" alt="Logo" class="img-fluid hero-logo-img" />
                </div>
            </div>
        </div>
    </div>
</div>

<!-- Main Content -->
<div class="container-fluid py-5 bg-main-gradient">
    <div class="container">
        <div class="mb-5">
            <div class="row align-items-center mb-4">
                <div class="col">
                    <h2 class="fw-bold text-main">Sagre in Evidenza</h2>
                    <p class="text-muted">Le sagre più popolari da non perdere</p>
                </div>
                <div class="col-auto">
                    <a href="/Sagre" class="btn rounded-pill px-4 btn-orange">Tutte le Sagre</a>
                </div>
            </div>
            
            <div class="row g-4">
                {#each sagreInEvidenza as sagra}
                    <div class="col-sm-6 col-lg-4">
                        <div class="card border-0 h-100 shadow-sm rounded-4 overflow-hidden hover-scale card-transition">
                            <div class="position-relative">
                                <img src="/imgs/sagra.jpg" class="card-img-top sagra-img" alt="Immagine della sagra" />
                                <div class="position-absolute bottom-0 start-0 w-100 p-3 sagra-overlay">
                                    <h5 class="card-title fw-bold text-white mb-0">{sagra.nomeSagra}</h5>
                                </div>
                            </div>
                            <div class="card-body p-4">
                                <p class="card-text text-muted mb-4">{sagra.descrizione}</p>
                                <div class="text-end">
                                    <a href='/Sagre/Sagra?id={sagra.idSagra}' class="btn rounded-pill px-4 btn-orange">Dettagli</a>
                                </div>
                            </div>
                        </div>
                    </div>
                {/each}
            </div>
        </div>
    </div>
</div>

<!-- Call to Action -->
<div class="container-fluid py-5 cta-section">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-8 text-center">
                <h2 class="fw-bold mb-3 text-white">Unisciti alla Comunità delle Sagre</h2>
                <p class="text-white opacity-75 mb-4">
                    Scopri tutti gli eventi, condividi le tue esperienze e resta aggiornato sulle novità del mondo delle sagre.
                </p>
                <a href="/Registrazione" class="btn btn-lg rounded-pill px-5 py-3 btn-white-red">Registrati Ora</a>
            </div>
        </div>
    </div>
</div>
