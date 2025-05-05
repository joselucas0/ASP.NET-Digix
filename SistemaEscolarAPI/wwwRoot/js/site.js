window.addEventListener('DOMContentLoaded', () => {
    const token = localStorage.getItem('token');
  
    // Se estivermos na index ou pagina2 e não houver token, redireciona para login
    if ((location.pathname.endsWith('index.html') || location.pathname.endsWith('pagina2.html')) && !token) {
      location.href = 'login.html';
    }
  
    // Logout
    const logoutLink = document.getElementById('logout');
    if (logoutLink) {
      logoutLink.addEventListener('click', e => {
        e.preventDefault();
        localStorage.removeItem('token');
        location.href = 'login.html';
      });
    }
  
    // Login form
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
      loginForm.addEventListener('submit', async e => {
        e.preventDefault();
        const user = document.getElementById('username').value;
        const pass = document.getElementById('password').value;
        const errorMsg = document.getElementById('errorMsg');
  
        try {
          const resp = await fetch('/api/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ username: user, password: pass })
          });
          if (!resp.ok) throw new Error('Credenciais inválidas');
          const data = await resp.json();
          localStorage.setItem('token', data.token);
          location.href = 'index.html';
        } catch (err) {
          errorMsg.textContent = err.message;
        }
      });
    }
  
    // Botão de saudação na index
    const btnHello = document.getElementById('btnHello');
    if (btnHello) {
      btnHello.addEventListener('click', () => alert('Olá, bem‑vindo ao Sistema Escolar!'));
    }
  });