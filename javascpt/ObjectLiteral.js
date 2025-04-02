function jogador(nome, idade) {
    this.nome = nome;
    this.idade = idade;
    PaisDeOrigem = {
        nome: "Brasil",
        cidade: "São Paulo",
        estado: "SP"
    }
    this.chutar = function(){
        console.log(`${this.nome} está chutando.`);
    };

}


function Time(nome, qnt){
    this.nome = nome;
    this.jogadores = [];
    this.qntJogadores = qnt;
    this.addJogadores = function(jogador){
        if(this.jogadores.length < this.qntJogadores){
            this.jogadores.push(jogador);
        }else{
            console.log("Time cheio");
        }
    }
}

//criar objetos
let jogador1 = new jogador("Lucas", 20);

let time1 = new Time("Verdomeiras", 5);

time1.addJogadores(jogador1.nome);
console.table(time1);

console.log()
