<script>
    import 'bootstrap/dist/css/bootstrap.min.css';
    import './+layout.css';
    import { page } from '$app/stores';
    import { browser } from '$app/environment';
    import { onMount } from 'svelte';
    
    // Importa gli store dal file dedicato
    import { isAuthenticated, userEmail } from '$lib/stores';
    
    // Funzione per controllare i cookie e verificare l'autenticazione all'avvio
    const checkAuthStatus = () => {
        if (browser) {
            const token = getCookie('authToken');
            const email = getCookie('userEmail');
            
            if (token && email) {
                isAuthenticated.set(true);
                userEmail.set(email);
                return true;
            } else {
                isAuthenticated.set(false);
                userEmail.set('');
                return false;
            }
        }
        return false;
    };
    
    // Funzione per ottenere un cookie
    const getCookie = (name) => {
        if (browser) {
            const cookies = document.cookie.split(';');
            for (let i = 0; i < cookies.length; i++) {
                const cookie = cookies[i].trim();
                if (cookie.startsWith(name + '=')) {
                    return decodeURIComponent(cookie.substring(name.length + 1));
                }
            }
        }
        return null;
    };
    
    // Funzione per eliminare i cookie
    const deleteCookie = (name) => {
        if (browser) {
            document.cookie = `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
        }
    };
    
    // Funzione di logout
    const handleLogout = () => {
        deleteCookie('authToken');
        deleteCookie('userEmail');
        deleteCookie('userRole');
        isAuthenticated.set(false);
        userEmail.set('');
        
        // Notifica altre schede del browser
        if (browser) {
            localStorage.removeItem('authStatus');
            localStorage.removeItem('userEmail');
            
            // Reindirizza alla home dopo il logout
            window.location.href = '/Home';
        }
    };
    
    onMount(() => {
        if (browser) {
            import('bootstrap/dist/js/bootstrap.bundle.min.js');
            
            // Controlla lo stato di autenticazione all'avvio
            checkAuthStatus();
            
            // Aggiungiamo un listener per ricontrollare lo stato di autenticazione
            // quando cambia il valore dei cookie o localStorage (ad esempio quando l'utente fa login in un'altra tab)
            window.addEventListener('storage', (event) => {
                if (event.key === 'authStatus' || event.key === 'userEmail') {
                    if (event.key === 'authStatus' && event.newValue === 'true') {
                        isAuthenticated.set(true);
                        if (localStorage.getItem('userEmail')) {
                            userEmail.set(localStorage.getItem('userEmail'));
                        }
                    } else if (event.key === 'authStatus' && !event.newValue) {
                        isAuthenticated.set(false);
                        userEmail.set('');
                    }
                }
            });
        }
    });
    
    // Aggiungiamo un listener per il cambio di pagina per ricontrollare lo stato di autenticazione
    $: {
        if ($page && browser) {
            checkAuthStatus();
        }
    }
</script>

<div class="navbar-wrapper">
    <nav class="navbar d-flex align-items-center">
        
        
        <a class:active={$page.url.pathname === '/Biglietto'} href="/Biglietto">Biglietto</a>
        
        
        <a class="navbar-brand me-4" href="/Home">
            <img src="/logo.png" alt="Logo" width="28" height="28" class="me-2" />
            Village Festival
        </a>
        
        <ul class="navbar-nav d-flex flex-row nav-center">
            <li class="nav-item">
                <a class="nav-link" class:active={$page.url.pathname === '/Home'} href="/Home">Home</a>
            </li>
            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="/Eventi" id="eventiDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false" class:active={$page.url.pathname.includes('/Eventi')}>
                    Eventi
                </a>
                <ul class="dropdown-menu" aria-labelledby="eventiDropdown">
                    <li><a class="dropdown-item" href="/Eventi/EventiFuturi">Eventi Futuri</a></li>
                    <li><a class="dropdown-item" href="/Eventi/EventiPassati">Eventi Passati</a></li>
                </ul>
            </li>
            <li class="nav-item">
                <a class="nav-link" class:active={$page.url.pathname === '/Sagre'} href="/Sagre">Sagre</a>
            </li>
        </ul>
        
        <div class="ms-auto d-flex align-items-center">
            {#if $isAuthenticated}
                <!-- Mostra l'icona del profilo con menu a tendina quando utente è loggato -->
                <div class="dropdown">
                    <div class="profile-icon" id="profileDropdown" data-bs-toggle="dropdown" aria-expanded="false">
                        {#if $userEmail}
                            {$userEmail[0].toUpperCase()}
                        {:else}
                            U
                        {/if}
                    </div>
                    <ul class="dropdown-menu dropdown-menu-end profile-dropdown" aria-labelledby="profileDropdown">
                        <li class="user-email">{$userEmail}</li>
                        <li><button class="dropdown-item logout-btn" on:click={handleLogout}>Logout</button></li>
                    </ul>
                </div>
            {:else}
                <!-- Mostra i pulsanti login/registrazione quando utente non è loggato -->
                <a class="btn-signin" href="/LogIn">Login</a>
                <a class="btn-signup" href="/Registrazione">Registrazione</a>
            {/if}
        </div>
    </nav>
</div>

<slot />