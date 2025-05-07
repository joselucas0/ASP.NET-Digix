// wwwroot/js/login.js
document.addEventListener('DOMContentLoaded', () => {
    initializeLogin();
    checkAuthState();
});

function initializeLogin() {
    const loginForm = document.getElementById('loginForm');
    if (!loginForm) return;

    loginForm.addEventListener('submit', async (e) => {
        e.preventDefault();
        
        const username = document.getElementById('username').value.trim();
        const password = document.getElementById('password').value.trim();
        const submitBtn = loginForm.querySelector('button[type="submit"]');

        if (!username || !password) {
            showAlert('Preencha todos os campos!', 'danger');
            return;
        }

        try {
            submitBtn.disabled = true;
            submitBtn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Autenticando...';

            // Gerar hash SHA256 (igual ao backend)
            const passwordHash = await generateSHA256Hash(password);

            const response = await fetch('/api/login', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    Username: username,
                    Password: passwordHash // Envia o hash diretamente
                })
            });

            const result = await handleResponse(response);
            
            localStorage.setItem('authToken', result.token);
            window.location.href = '/index.html';

        } catch (error) {
            showAlert(error.message, 'danger');
            console.error('Login Error:', error);
        } finally {
            submitBtn.disabled = false;
            submitBtn.innerHTML = '<i class="fas fa-sign-in-alt me-2"></i>Entrar';
        }
    });
}

// Função para gerar hash SHA256 (igual ao backend)
async function generateSHA256Hash(password) {
    const encoder = new TextEncoder();
    const data = encoder.encode(password);
    const hashBuffer = await crypto.subtle.digest('SHA-256', data);
    const hashArray = Array.from(new Uint8Array(hashBuffer));
    return btoa(hashArray.map(b => String.fromCharCode(b)).join(''));
}

// Função para verificar autenticação
function checkAuthState() {
    const token = localStorage.getItem('authToken');
    const currentPage = window.location.pathname.split('/').pop();
    
    if (token && currentPage === 'login.html') {
        window.location.href = 'index.html';
    }
    
    const protectedPages = ['alunos.html', 'cursos.html'];
    if (!token && protectedPages.includes(currentPage)) {
        window.location.href = 'login.html';
    }
}

// Função para logout
window.handleLogout = () => {
    localStorage.removeItem('authToken');
    window.location.href = 'login.html';
}

// Funções auxiliares
async function handleResponse(response) {
    const text = await response.text();
    try {
        const data = JSON.parse(text);
        if (!response.ok) throw new Error(data || 'Erro na autenticação');
        return data;
    } catch {
        throw new Error(text || 'Erro desconhecido');
    }
}

function showAlert(message, type = 'success') {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type} alert-dismissible fade show mt-3`;
    alertDiv.innerHTML = `
        <i class="fas ${type === 'danger' ? 'fa-exclamation-triangle' : 'fa-check-circle'} me-2"></i>
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;

    const container = document.querySelector('.container');
    container.prepend(alertDiv);

    setTimeout(() => alertDiv.remove(), 5000);
}