import { Animal } from './Animal.js';
import { Cobra } from './Cobra.js';

function criarAnimais() {
    // Dados diretamente no código
    return {
        animais: [
            {
                tipo: "Cobra",
                dados: {
                    nome: "Naja",
                    raca: "Naja kaouthia",
                    peso: 2.5,
                    idade: 3,
                    cor: "preta e branca"
                }
            },
            {
                tipo: "Animal",
                dados: {
                    nome: "Leão",
                    raca: "Panthera leo",
                    peso: 190,
                    idade: 5
                }
            }
        ]
    };
}

function main() {
    const animaisData = criarAnimais();
    const animais = [];

    for (const item of animaisData.animais) {
        let animal;
        
        if (item.tipo === "Cobra") {
            animal = new Cobra(
                item.dados.nome,
                item.dados.raca,
                item.dados.peso,
                item.dados.idade,
                item.dados.cor
            );
        } else {
            animal = new Animal(
                item.dados.nome,
                item.dados.raca,
                item.dados.peso,
                item.dados.idade
            );
        }
        
        animais.push(animal);
    }

    console.log("=== ANIMAIS CARREGADOS ===");
    animais.forEach(animal => {
        console.log(animal);
        animal.comer();
        
        if (animal instanceof Cobra) {
            animal.procriar();
            animal.trocarPele();
            console.log(`É venenosa? ${Cobra.venenosa}`);
        }
        
        console.log('-----------------------');
    });
}

main();