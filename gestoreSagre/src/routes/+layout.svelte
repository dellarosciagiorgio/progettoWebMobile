<script>
	import Registrazione from './../components/Registrazione/registrazione.svelte';
	import Login from './../components/LogIn/login.svelte';
    import 'bootstrap/dist/css/bootstrap.min.css';
    import { page } from '$app/stores';
    import { browser } from '$app/environment';
    import { onMount } from 'svelte';
    
    onMount(() => {
        if (browser)
            import('bootstrap/dist/js/bootstrap.bundle.min.js');
    });
</script>

<style>
    :root {
        --cottage-orange: #e67300;
        --cottage-light: #fff8f0;
        --cottage-dark: #592c0d;
        --cottage-yellow: #f1e480;
    }
    
    .navbar-wrapper {
        background-color:var(--cottage-light);
        padding: 1rem;
    }
    
    .navbar {
        background-color: var(--cottage-light);
        border-radius: 2rem;
        padding: 0.75rem 2rem;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
        font-family: Georgia, serif;
    }
    
    .navbar-brand {
        font-weight: bold;
        color: var(--cottage-orange) !important;
        font-size: 1.5rem;
        display: flex;
        align-items: center;
    }
    
    .nav-link {
        color: var(--cottage-dark) !important;
        font-size: 1rem;
        padding: 0.5rem 1rem;
        border-radius: 1rem;
        transition: background-color 0.2s;
    }
    
    .nav-link:hover,
    .nav-link.active {
        background-color: var(--cottage-orange);
        color: white !important;
    }
    
    .btn-signin {
        background: none;
        border: none;
        color: var(--cottage-dark);
        font-weight: 500;
        margin-right: 0.5rem;
    }
    
    .btn-signup {
        background-color: var(--cottage-yellow);
        border: none;
        padding: 0.4rem 1rem;
        border-radius: 1rem;
        font-weight: 600;
        color: var(--cottage-dark);
    }
    
    .nav-center {
        flex: 1;
        justify-content: center;
    }
    
    .dropdown-menu {
        background-color: var(--cottage-light);
        border-radius: 1rem;
        border: none;
        box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
    }
    
    .dropdown-item {
        color: var(--cottage-dark);
        font-size: 0.95rem;
        padding: 0.5rem 1rem;
        transition: background-color 0.2s;
    }
    
    .dropdown-item:hover {
        background-color: var(--cottage-yellow);
        color: var(--cottage-dark);
        border-radius: 0.5rem;
    }
</style>

<div class="navbar-wrapper">
    <nav class="navbar d-flex align-items-center">
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
            <a class="btn-signin" href="/LogIn">Login</a>
            <a class="btn-signup" href="/Registrazione">Registrazione</a>
        </div>
    </nav>
</div>

<slot />