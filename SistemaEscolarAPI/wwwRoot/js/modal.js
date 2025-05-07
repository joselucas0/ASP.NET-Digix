class CustomModal {
    constructor() {
        this.modal = this.createModal();
        document.body.appendChild(this.modal);
        this.setupCloseHandlers();
    }

    createModal() {
        const modal = document.createElement('div');
        modal.className = 'modal-overlay';
        modal.innerHTML = `
            <div class="modal-content">
                <div class="modal-header">
                    <h3 class="modal-title"></h3>
                    <button class="modal-close">&times;</button>
                </div>
                <div class="modal-body"></div>
                <div class="modal-footer"></div>
            </div>
        `;
        return modal;
    }

    show(title, content, buttons = []) {
        this.setTitle(title);
        this.setContent(content);
        this.addButtons(buttons);
        this.open();
    }

    setTitle(title) {
        this.modal.querySelector('.modal-title').textContent = title;
    }

    setContent(content) {
        this.modal.querySelector('.modal-body').innerHTML = content;
    }

    addButtons(buttons) {
        const footer = this.modal.querySelector('.modal-footer');
        footer.innerHTML = '';
        buttons.forEach(btnConfig => {
            const btn = document.createElement('button');
            btn.className = `btn btn-${btnConfig.type || 'secondary'}`;
            btn.textContent = btnConfig.text;
            if(btnConfig.handler) {
                btn.addEventListener('click', () => {
                    btnConfig.handler();
                    if(btnConfig.close !== false) this.close();
                });
            }
            footer.appendChild(btn);
        });
    }

    open() {
        this.modal.classList.add('active');
        document.body.style.overflow = 'hidden';
    }

    close() {
        this.modal.classList.remove('active');
        document.body.style.overflow = '';
    }

    setupCloseHandlers() {
        this.modal.querySelector('.modal-close').addEventListener('click', () => this.close());
        this.modal.addEventListener('click', (e) => {
            if(e.target === this.modal) this.close();
        });
    }
}