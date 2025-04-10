<script>
    import "bootstrap/dist/css/bootstrap.min.css";
  
    let email = "";
    let password = "";
  
    const handleLogin = () => {
      console.log("Login con Email:", email, "Password:", password);
    };
  </script>
  
  <div class="container d-flex justify-content-center align-items-center vh-100">
    <div class="card p-4 shadow-lg text-center" style="width: 400px;">
      <div class="mb-3">
        <img src="/logo.png" alt="Logo" class="img-fluid" style="max-width: 150px;" />
      </div>
      <h2 class="text-primary">Benvenuto!</h2>
  
      <form on:submit|preventDefault={handleLogin}>
        <div class="mb-3 text-start">
          <label for="email" class="form-label">Email</label>
          <input type="email" class="form-control" bind:value={email} required />
        </div>
        <div class="mb-3 text-start">
          <label for="password" class="form-label">Password</label>
          <input type="password" class="form-control" bind:value={password} required />
        </div>
        <button type="submit" class="btn btn-primary w-100">Accedi</button>
      </form>
  
      <div class="text-center mt-3">
        <small>Non hai un account? 
          <a href="/Registrazione">Registrati</a>
        </small>
      </div>
    </div>
  </div>
  
  