<script>
    import { onMount } from "svelte";
    import "bootstrap/dist/css/bootstrap.min.css";

    // Dati statici di esempio per sagre
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
<div class="container-fluid py-5" style="background: linear-gradient(135deg, #fcf8f3 0%, #fbe6d4 100%);">
    <div class="container py-5">
        <div class="row align-items-center">
            <div class="col-lg-6 mb-5 mb-lg-0">
                <h1 class="display-4 fw-bold mb-4" style="color: #d62828;">Benvenuto nel Mondo delle Sagre</h1>
                <p class="lead mb-4" style="color: #555;">Scopri le tradizioni culinarie e gli eventi culturali della nostra regione. Partecipa alle sagre locali e vivi un'esperienza unica.</p>
                <div class="d-flex gap-3">
                    <a href="/Sagre" class="btn btn-lg rounded-pill px-4" style="background-color: #f77f00; color: white;">Esplora Sagre</a>
                </div>
            </div>
            <div class="col-lg-6 text-center">
                <div class="position-relative">
                    <div class="position-absolute" style="width: 300px; height: 300px; background-color: rgba(247, 127, 0, 0.1); border-radius: 50%; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 0;"></div>
                    <img src="/logo.png" alt="Logo" class="img-fluid" style="max-width: 250px; z-index: 1; position: relative;" />
                </div>
            </div>
        </div>
    </div>
</div>

<!-- Main Content -->
<div class="container-fluid py-5" style="background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);">
    <div class="container">
        <!-- Sagre in Evidenza -->
        <div class="mb-5">
            <div class="row align-items-center mb-4">
                <div class="col">
                    <h2 class="fw-bold" style="color: #d62828;">Sagre in Evidenza</h2>
                    <p class="text-muted">Le sagre più popolari da non perdere</p>
                </div>
                <div class="col-auto">
                    <a href="/Sagre" class="btn rounded-pill px-4" style="background-color: #f77f00; color: white;">Tutte le Sagre</a>
                </div>
            </div>
            
            <div class="row g-4">
                {#each sagreInEvidenza as sagra}
                    <div class="col-sm-6 col-lg-4">
                        <div class="card border-0 h-100 shadow-sm rounded-4 overflow-hidden hover-scale" style="transition: all 0.3s ease;">
                            <div class="position-relative">
                                <img src="/imgs/sagra.jpg" class="card-img-top" alt="Immagine della sagra" style="height: 200px; object-fit: cover;">
                                <div class="position-absolute bottom-0 start-0 w-100 p-3" style="background: linear-gradient(to top, rgba(0,0,0,0.7), rgba(0,0,0,0));">
                                    <h5 class="card-title fw-bold text-white mb-0">{sagra.nomeSagra}</h5>
                                </div>
                            </div>
                            <div class="card-body p-4">
                                <p class="card-text text-muted mb-4">{sagra.descrizione}</p>
                                <div class="text-end">
                                    <a href='/Sagre/Sagra?id={sagra.idSagra}' class="btn rounded-pill px-4" style="background-color: #f77f00; color: white;">
                                        Dettagli
                                    </a>
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
<div class="container-fluid py-5" style="background-color: #d62828;">
    <div class="container">
        <div class="row justify-content-center">
            <div class="col-lg-8 text-center">
                <h2 class="fw-bold mb-3 text-white">Unisciti alla Comunità delle Sagre</h2>
                <p class="text-white opacity-75 mb-4">Scopri tutti gli eventi, condividi le tue esperienze e resta aggiornato sulle novità del mondo delle sagre.</p>
                <a href="/Registrazione" class="btn btn-lg rounded-pill px-5 py-3" style="background-color: white; color: #d62828;">Registrati Ora</a>
            </div>
        </div>
    </div>
</div>

<style>
    .hover-scale:hover {
        transform: translateY(-5px);
        box-shadow: 0 10px 20px rgba(0,0,0,0.1) !important;
    }
</style>