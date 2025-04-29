<script>
    import 'bootstrap/dist/css/bootstrap.min.css';
    import './+layout.css';
    import { page } from '$app/stores';
    import { browser } from '$app/environment';
    import { onMount } from 'svelte';
    
    // Importa gli store dal file dedicato
    import { isAuthenticated, userEmail, role } from '$lib/stores';
    
    // Importa le funzioni per i cookie
    import { getCookie, setCookie, deleteCookie } from '$lib/cookies';
    
    // Variabile per memorizzare il ruolo dell'utente
    let userRole = '';
    
    // Gestione del dropdown eventi manuale
    let eventiDropdownOpen = false;
    
    function toggleEventiDropdown(e) {
        e.preventDefault();
        e.stopPropagation();
        eventiDropdownOpen = !eventiDropdownOpen;
    }
    
    function closeEventiDropdown() {
        eventiDropdownOpen = false;
    }
    
    // Funzione per controllare i cookie e verificare l'autenticazione all'avvio
    const checkAuthStatus = () => {
        if (browser) {
            const token = getCookie('authToken');
            const email = getCookie('userEmail');
            const savedRole = getCookie('userRole');
            
            if (token && email) {
                isAuthenticated.set(true);
                userEmail.set(email);
                if (savedRole) {
                    role.set(savedRole);
                    userRole = savedRole;
                }
                // Aggiorniamo anche localStorage per sincronizzazione tra tab
                localStorage.setItem('authStatus', 'true');
                localStorage.setItem('userEmail', email);
                if (savedRole) localStorage.setItem('userRole', savedRole);
                return true;
            } else {
                isAuthenticated.set(false);
                userEmail.set('');
                role.set('');
                userRole = '';
                return false;
            }
        }
        return false;
    };
    
    // Funzione per prolungare la durata dei cookie ad ogni accesso
    const refreshAuthCookies = () => {
        if (browser && $isAuthenticated) {
            const token = getCookie('authToken');
            const email = getCookie('userEmail');
            const savedRole = getCookie('userRole');
            
            if (token) setCookie('authToken', token);
            if (email) setCookie('userEmail', email);
            if (savedRole) setCookie('userRole', savedRole);
        }
    };
    
    // Funzione di logout
    const handleLogout = () => {
        deleteCookie('authToken');
        deleteCookie('userEmail');
        deleteCookie('userRole');
        isAuthenticated.set(false);
        userEmail.set('');
        role.set('');
        userRole = '';
        
        // Notifica altre schede del browser
        if (browser) {
            localStorage.removeItem('authStatus');
            localStorage.removeItem('userEmail');
            localStorage.removeItem('userRole');
            
            // Reindirizza alla home dopo il logout
            window.location.href = '/Home';
        }
    };
    
    // Gestione dropdown profilo
    let profileDropdownOpen = false;
    
    function toggleProfileDropdown(e) {
        e.stopPropagation();
        profileDropdownOpen = !profileDropdownOpen;
    }
    
    function closeProfileDropdown() {
        profileDropdownOpen = false;
    }
    
    // Per chiudere i dropdown quando si clicca altrove
    function handleClickOutside(e) {
        if (!e.target.closest('.eventi-dropdown') && !e.target.closest('.eventi-link')) {
            closeEventiDropdown();
        }
        
        if (!e.target.closest('.profile-dropdown') && !e.target.closest('.profile-icon')) {
            closeProfileDropdown();
        }
    }
    
    // Mantieni aggiornata la variabile userRole quando cambia lo store role
    role.subscribe(value => {
        userRole = value;
    });
    
    onMount(() => {
        if (browser) {
            // Aggiungi event listener per chiudere i dropdown quando si clicca fuori
            document.addEventListener('click', handleClickOutside);
            
            // Controlla lo stato di autenticazione all'avvio, Se autenticato, aggiorna i cookie per prolungare la durata
            if (checkAuthStatus()) {
                refreshAuthCookies();
            }
            
            // Un listener per il logout da altre schede
            window.addEventListener('storage', (event) => {
                if (event.key === 'authStatus' || event.key === 'userEmail' || event.key === 'userRole') {
                    if (event.key === 'authStatus' && event.newValue === 'true') {
                        isAuthenticated.set(true);
                        if (localStorage.getItem('userEmail')) {
                            userEmail.set(localStorage.getItem('userEmail'));
                        }
                        if (localStorage.getItem('userRole')) {
                            role.set(localStorage.getItem('userRole'));
                        }
                    } else if (event.key === 'authStatus' && !event.newValue) {
                        isAuthenticated.set(false);
                        userEmail.set('');
                        role.set('');
                    }
                }
            });
            
            // Controlla e rinnova i cookie ogni 5 minuti se l'utente è attivo
            const refreshInterval = setInterval(() => {
                if ($isAuthenticated) {
                    refreshAuthCookies();
                }
            }, 300000);
            
            return () => {
                document.removeEventListener('click', handleClickOutside);
                clearInterval(refreshInterval);
            };
        }
    });
    
    // listener per il cambio di pagina per ricontrollare lo stato di autenticazione
    $: {
        if ($page && browser) {
            checkAuthStatus();
            refreshAuthCookies();
        }
    }
</script>

<div class="navbar-wrapper">
    <nav class="navbar d-flex align-items-center">
        
        <a class="navbar-brand me-4" href="/Home">
            <img src="/logo.png" alt="Logo" width="28" height="28" class="me-2" />
            Village Festival
        </a>
        
        <ul class="navbar-nav d-flex flex-row nav-center">
            <!-- Home sempre visibile a tutti -->
            <li class="nav-item">
                <a class="nav-link" class:active={$page.url.pathname === '/Home'} href="/Home">Home</a>
            </li>
            
            {#if userRole !== 'Organizzatore'}
                <!-- Elementi visibili SOLO ai NON organizzatori (acquirenti) -->
                
                <!-- Dropdown Eventi - Implementazione completamente custom -->
                <li class="nav-item eventi-dropdown">
                    <a href="/Eventi" 
                       class="nav-link eventi-link" 
                       class:active={$page.url.pathname.includes('/Eventi')} 
                       on:click={toggleEventiDropdown}>
                        Eventi
                        <i class="bi bi-chevron-down ms-1"></i>
                    </a>
                    
                    {#if eventiDropdownOpen}
                        <div class="custom-dropdown">
                            <a href="/Eventi/EventiFuturi" class="dropdown-item">Eventi Futuri</a>
                            <a href="/Eventi/EventiPassati" class="dropdown-item">Eventi Passati</a>
                        </div>
                    {/if}
                </li>
                
                <li class="nav-item">
                    <a class="nav-link" class:active={$page.url.pathname === '/Sagre'} href="/Sagre">Sagre</a>
                </li>
                
                {#if $isAuthenticated}
                    <li class="nav-item">
                        <a class="nav-link" class:active={$page.url.pathname === '/Biglietto'} href="/Biglietto">I miei biglietti</a>
                    </li>
                {/if}
            {:else}
                <!-- Elementi visibili SOLO agli organizzatori -->
                <li class="nav-item">
                    <a class="nav-link" class:active={$page.url.pathname === '/LeMieSagre'} href="/LeMieSagre">Le mie sagre</a>
                </li>
            {/if}
        </ul>
        
        <div class="ms-auto d-flex align-items-center">
            {#if $isAuthenticated}
                <!-- Mostra l'icona del profilo con menu a tendina quando utente è loggato -->
                <div class="profile-dropdown">
                    <div class="profile-icon" on:click={toggleProfileDropdown}>
                        {#if $userEmail}
                            {$userEmail[0].toUpperCase()}
                        {:else}
                            U
                        {/if}
                    </div>
                    
                    {#if profileDropdownOpen}
                        <div class="custom-dropdown profile-menu">
                            <div class="user-email">{$userEmail}</div>
                            
                            <!-- Opzioni nel menu dropdown in base al ruolo -->
                            {#if userRole === 'Organizzatore'}
                                <a href="/LeMieSagre" class="dropdown-item">Le mie sagre</a>
                            {:else}
                                <a href="/Biglietto" class="dropdown-item">I miei biglietti</a>
                            {/if}
                            
                            <button class="dropdown-item logout-btn" on:click={handleLogout}>Logout</button>
                        </div>
                    {/if}
                </div>
            {:else}
                <!-- Mostra i pulsanti login/registrazione quando utente non è loggato -->
                <a href="/LogIn" class="btn btn-login me-2">Login</a>
                <a href="/Registrazione" class="btn btn-register">Registrazione</a>
            {/if}
        </div>
    </nav>
</div>

<slot />

