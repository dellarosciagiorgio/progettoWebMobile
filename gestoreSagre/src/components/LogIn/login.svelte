<script>
  import "bootstrap/dist/css/bootstrap.min.css";
  
  let email = "";
  let password = "";
  let selectedRole = "acquirente";
  
  const handleLogin = () => {
    console.log("Login con Email:", email, "Password:", password, "Ruolo:", selectedRole);
  };
</script>

<div class="container-fluid min-vh-100 d-flex align-items-center justify-content-center" style="background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);">
  <div class="row w-100 justify-content-center">
    <div class="col-md-8 col-lg-6 col-xl-5">
      <div class="card border-0 shadow-lg rounded-4 overflow-hidden">
        <div class="card-header text-white p-0">
          <div class="text-center py-4" style="background: linear-gradient(to right, #d62828, #f77f00);">
            <h3 class="mb-0 fw-bold">Sagre Italiane</h3>
            <p class="mb-0">Scopri e organizza eventi gastronomici</p>
          </div>
        </div>
        
        <div class="card-body p-4 p-md-5">
          <div class="text-center mb-4">
            <div class="mb-3 position-relative d-inline-block">
              <img src="/logo.png" alt="Logo" class="img-fluid" style="max-width: 150px; z-index: 1; position: relative;" />
              <div class="position-absolute" style="width: 160px; height: 160px; background-color: rgba(247, 127, 0, 0.1); border-radius: 50%; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 0;"></div>
            </div>
            <h4 class="fw-bold text-dark mb-1">Benvenuto!</h4>
            <p class="text-muted mb-4">Accedi per continuare</p>
          </div>
          
          <form on:submit|preventDefault={handleLogin}>
            <div class="form-floating mb-4">
              <select class="form-select border-0 bg-light" style="box-shadow: 0 2px 5px rgba(0,0,0,0.08);" id="role" bind:value={selectedRole}>
                <option value="acquirente">Acquirente</option>
                <option value="organizzatore">Organizzatore</option>
                <option value="amministratore">Amministratore</option>
              </select>
              <label for="role" class="text-muted">Accedi come</label>
            </div>
            
            <div class="form-floating mb-3">
              <input 
                type="email" 
                class="form-control border-0 bg-light" 
                style="box-shadow: 0 2px 5px rgba(0,0,0,0.08);"
                id="email" 
                placeholder="nome@esempio.com" 
                bind:value={email} 
                required
              />
              <label for="email" class="text-muted">Email</label>
            </div>
            
            <div class="form-floating mb-3">
              <input 
                type="password" 
                class="form-control border-0 bg-light" 
                style="box-shadow: 0 2px 5px rgba(0,0,0,0.08);"
                id="password" 
                placeholder="Password" 
                bind:value={password} 
                required
              />
              <label for="password" class="text-muted">Password</label>
            </div>
            
            <button type="submit" class="btn btn-lg w-100 text-white mb-4 py-3" style="background-color: #f77f00; border: none; box-shadow: 0 4px 12px rgba(247, 127, 0, 0.25);">
              Accedi
            </button>
            
            <div class="d-flex align-items-center my-4">
              <div class="flex-grow-1 border-bottom"></div>
              <div class="px-3 text-muted small">oppure</div>
              <div class="flex-grow-1 border-bottom"></div>
            </div>
            
            <div class="text-center">
              <p class="mb-0">Non hai ancora un account?</p>
              <a href="/Registrazione" class="fw-bold text-decoration-none" style="color: #d62828;">
                Registrati ora
              </a>
            </div>
          </form>
        </div>
        
        <div class="card-footer bg-white text-center p-3 border-0">
          <p class="text-muted small mb-0">© 2025 Sagre Italiane - Tutti i diritti riservati</p>
        </div>
      </div>
    </div>
  </div>
</div>