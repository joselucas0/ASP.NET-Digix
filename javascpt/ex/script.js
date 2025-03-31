document.addEventListener('DOMContentLoaded', () => {
    let tasks = JSON.parse(localStorage.getItem('tasks')) || [];
    let editingId = null;

    // Elementos DOM
    const form = document.getElementById('taskForm');
    const titleInput = document.getElementById('title');
    const descriptionInput = document.getElementById('description');
    const taskList = document.getElementById('taskList');
    const arrayDebug = document.getElementById('arrayDebug');

    // Eventos
    form.addEventListener('submit', handleSubmit);
    
    // Função principal
    function handleSubmit(e) {
        e.preventDefault();
        
        if (!titleInput.value.trim()) {
            showNotification('⚠️ Título é obrigatório!', 'error');
            return;
        }

        const task = {
            id: editingId || Date.now(),
            title: titleInput.value.trim(),
            description: descriptionInput.value.trim(),
            date: new Date().toLocaleString()
        };

        if (editingId) {
            const index = tasks.findIndex(t => t.id === editingId);
            if (index !== -1) {
                tasks[index] = task;
                showNotification('✅ Tarefa atualizada!', 'success');
            }
        } else {
            tasks.push(task);
            showNotification('✨ Nova tarefa criada!', 'success');
        }

        saveData();
        renderTasks();
        resetForm();
    }

    // Renderiza as tarefas
    function renderTasks() {
        taskList.innerHTML = '';
        
        tasks.forEach(task => {
            const taskElement = document.createElement('div');
            taskElement.className = 'task-card';
            taskElement.innerHTML = `
                <div class="task-content">
                    <h3>${task.title}</h3>
                    ${task.description ? `<p>${task.description}</p>` : ''}
                    <small>${task.date}</small>
                </div>
                <div class="task-actions">
                    <button class="btn-edit" onclick="editTask(${task.id})">✏️</button>
                    <button class="btn-delete" onclick="deleteTask(${task.id})">🗑️</button>
                </div>
            `;
            taskList.appendChild(taskElement);
        });

        updateArrayDebug();
    }

    // Funções globais
    window.editTask = (id) => {
        const task = tasks.find(t => t.id === id);
        if (task) {
            titleInput.value = task.title;
            descriptionInput.value = task.description;
            editingId = id;
            form.scrollIntoView({ behavior: 'smooth' });
            showNotification('📝 Editando tarefa...', 'info');
        }
    };

    window.deleteTask = (id) => {
        tasks = tasks.filter(t => t.id !== id);
        saveData();
        renderTasks();
        showNotification('🗑️ Tarefa excluída!', 'error');
    };

    // Funções auxiliares
    function saveData() {
        localStorage.setItem('tasks', JSON.stringify(tasks));
        console.log("Tarefas salvas:", JSON.stringify(tasks)); 
        updateArrayDebug();
    }

    function resetForm() {
        form.reset();
        editingId = null;
    }

    function updateArrayDebug() {
        arrayDebug.textContent = JSON.stringify(tasks, null, 2);
    }

    function showNotification(message, type) {
        const notification = document.createElement('div');
        notification.className = `notification ${type}`;
        notification.textContent = message;
        document.body.appendChild(notification);
        
        setTimeout(() => {
            notification.remove();
        }, 3000);
    }

    // Inicialização
    renderTasks();
});
