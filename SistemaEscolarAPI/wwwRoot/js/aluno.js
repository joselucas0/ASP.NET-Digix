document.addEventListener('DOMContentLoaded', () => {
    const alunosTable = document.getElementById('tabela-alunos');
    const novoAlunoBtn = document.getElementById('novo-aluno');

    const loadAlunos = async () => {
        const response = await fetch('/api/Aluno');
        const alunos = await response.json();
        
        alunosTable.innerHTML = alunos.map(aluno => `
            <tr>
                <td>${aluno.nome}</td>
                <td>${aluno.email}</td>
                <td>${new Date(aluno.dataNascimento).toLocaleDateString('pt-BR')}</td>
                <td>
                    <button class="btn btn-sm btn-primary" onclick="openEditAluno(${aluno.id})">
                        Editar
                    </button>
                    <button class="btn btn-sm btn-danger" onclick="deleteAluno(${aluno.id})">
                        Excluir
                    </button>
                </td>
            </tr>
        `).join('');
    };

    window.openEditAluno = async (id) => {
        const response = await fetch(`/api/Aluno/${id}`);
        const aluno = await response.json();
        
        const modal = new CustomModal();
        modal.show('Editar Aluno', `
            <form id="edit-aluno-form">
                <div class="mb-3">
                    <label class="form-label">Nome</label>
                    <input type="text" class="form-control" name="nome" value="${aluno.nome}" required>
                </div>
                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <input type="email" class="form-control" name="email" value="${aluno.email}" required>
                </div>
                <div class="mb-3">
                    <label class="form-label">Data Nascimento</label>
                    <input type="date" class="form-control" name="dataNascimento" 
                           value="${new Date(aluno.dataNascimento).toISOString().split('T')[0]}" required>
                </div>
            </form>
        `, [
            { 
                text: 'Salvar', 
                type: 'primary',
                submit: true,
                handler: async () => {
                    const formData = new FormData(document.getElementById('edit-aluno-form'));
                    const data = Object.fromEntries(formData.entries());
                    
                    await fetch(`/api/Aluno/${id}`, {
                        method: 'PUT',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(data)
                    });
                    
                    await loadAlunos();
                }
            }
        ], 'edit-aluno-form');
    };

    window.deleteAluno = (id) => {
        CustomModal.confirm('Tem certeza que deseja excluir este aluno?', async () => {
            await fetch(`/api/Aluno/${id}`, { method: 'DELETE' });
            await loadAlunos();
        });
    };

    novoAlunoBtn.addEventListener('click', () => {
        const modal = new CustomModal();
        modal.show('Novo Aluno', `
            <form id="new-aluno-form">
                <div class="mb-3">
                    <label class="form-label">Nome</label>
                    <input type="text" class="form-control" name="nome" required>
                </div>
                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <input type="email" class="form-control" name="email" required>
                </div>
                <div class="mb-3">
                    <label class="form-label">Data Nascimento</label>
                    <input type="date" class="form-control" name="dataNascimento" required>
                </div>
            </form>
        `, [
            { 
                text: 'Criar', 
                type: 'primary',
                submit: true,
                handler: async () => {
                    const formData = new FormData(document.getElementById('new-aluno-form'));
                    const data = Object.fromEntries(formData.entries());
                    
                    await fetch('/api/Aluno', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify(data)
                    });
                    
                    await loadAlunos();
                }
            }
        ], 'new-aluno-form');
    });

    loadAlunos();
});