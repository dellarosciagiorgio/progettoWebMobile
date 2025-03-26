<script>
    import 'bootstrap/dist/css/bootstrap.min.css';
    import { page } from '$app/stores';
    import { browser } from '$app/environment';
    import { onMount } from 'svelte';

    // Carica Bootstrap solo nel browser
    onMount(() => {
        if (browser) {
            import('bootstrap/dist/js/bootstrap.bundle.min.js');
        }
    });
</script>

<nav class="navbar navbar-expand-lg navbar-light bg-light">
    <div class="container-fluid">
        <a class="navbar-brand" href="#">
            <img src="/logo.png" alt="Logo" width="30" height="24" class="d-inline-block align-text-top">
            Village Festival
        </a>
        <a
            class="nav-link me-3"
            class:active={$page.url.pathname === '/login'}
            href="/login"
        >
            <i class="bi bi-person-circle"></i> Login
        </a>

        <button
            class="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNav"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
        >
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                <li class="nav-item">
                    <a
                        class="nav-link"
                        class:active={$page.url.pathname === '/'}
                        href="/"
                    >
                        Home
                    </a>
                </li>
                <li class="nav-item dropdown">
                    <a
                        class="nav-link dropdown-toggle"
                        href="/getEventi"
                        id="navbarEventiDropdown"
                        role="button"
                        data-bs-toggle="dropdown"
                        aria-expanded="false"
                    >
                        I miei eventi
                    </a>
                    <ul class="dropdown-menu" aria-labelledby="navbarEventiDropdown">
                        <li>
                            <a
                                class="dropdown-item"
                                class:active={$page.url.pathname === '/getEventi/getEventiPassati'}
                                href="/getEventi/getEventiPassati"
                            >
                                Eventi passati
                            </a>
                        </li>
                        <li>
                            <a
                                class="dropdown-item"
                                class:active={$page.url.pathname === '/getEventi/getEventiFuturi'}
                                href="/getEventi/getEventiFuturi"
                            >
                                Eventi futuri
                            </a>
                        </li>
                    </ul>
                </li>
                <li class="nav-item">
                    <a
                        class="nav-link"
                        class:active={$page.url.pathname === '/getSagre'}
                        href="/getSagre"
                    >
                        Sagre
                    </a>
                </li>
            </ul>
        </div>
    </div>
</nav>

<!-- Passa i children -->
<slot />