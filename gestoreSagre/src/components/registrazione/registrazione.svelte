<script>
    import "bootstrap/dist/css/bootstrap.min.css";
  
    let nome = "";
    let email = "";
    let password = "";
  
    const handleRegister = () => {
      console.log("Registrazione con Nome:", nome, "Email:", email, "Password:", password);
    };
  </script>
  
  <div class="container d-flex justify-content-center align-items-center vh-100">
    <div class="card p-4 shadow-lg text-center" style="width: 400px;">
      <div class="mb-3">
        <img src="/logo.png" alt="Logo" class="img-fluid" style="max-width: 150px;" />
      </div>
      <h2 class="text-success">Registrazione</h2>
  
      <form on:submit|preventDefault={handleRegister}>
        <div class="mb-3 text-start">
          <label class="form-label">Nome</label>
          <input type="text" class="form-control" bind:value={nome} required />
        </div>
        <div class="mb-3 text-start">
          <label class="form-label">Email</label>
          <input type="email" class="form-control" bind:value={email} required />
        </div>
        <div class="mb-3 text-start">
          <label class="form-label">Password</label>
          <input type="password" class="form-control" bind:value={password} required />
        </div>
        <button type="submit" class="btn btn-success w-100">Registrati</button>
      </form>
  
      <div class="text-center mt-3">
        <small>Hai già un account? 
          <a href="/login">Accedi</a>
        </small>
      </div>
    </div>
  </div>
  