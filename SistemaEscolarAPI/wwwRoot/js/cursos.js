// Estrutura similar ao alunos.js, adaptando campos
document.addEventListener('DOMContentLoaded', () => {
    const cursosContainer = document.getElementById('lista-cursos');
    
    const loadCursos = async () => {
        const response = await fetch('/api/Curso');
        const cursos = await response.json();
        
        cursosContainer.innerHTML = cursos.map(curso => `
            <div class="col-md-4 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">${curso.nome}</h5>
                        <p class="card-text">${curso.descricao}</p>
                        <p>Duração: ${curso.duracao} meses</p>
                        <button class="btn btn-primary" onclick="openEditCurso(${curso.id})">
                            Editar
                        </button>
                        <button class="btn btn-danger" onclick="deleteCurso(${curso.id})">
                            Excluir
                        </button>
                    </div>
                </div>
            </div>
        `).join('');
    };

    // Implementar openEditCurso e deleteCurso similar ao alunos.js
});