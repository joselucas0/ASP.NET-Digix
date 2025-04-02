export class Animal {
    constructor(nome, raca, peso, idade) {
        this.nome = nome;
        this.raca = raca;
        this.peso = peso;
        this.idade = idade;
    }

    comer() {
        console.log(`${this.nome} está comendo.`);
    }

    dormir() {
        console.log(`${this.nome} está dormindo.`);
    }

    fazerBarulho() {
        console.log(`${this.nome} está fazendo barulho.`);
    }
}