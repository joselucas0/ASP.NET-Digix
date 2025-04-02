let pessoa = {
    nome : 'Lucas'
    
}

console.log(Object.getOwnPropertyDescriptor(pessoa));


Object.assign(pessoa, {idade: 20});

let config = {
    ip: '123.123.123',
    port: 8080,
    block: false,
}

let { ip, port, block } = config;
console.log(ip, port, block);

let lista = ['Lucas', 'Maria', 'João'];
let [nome1, nome2, nome3] = lista;

console.log(nome1, nome2, nome3);

let lista2 = ['Lucas', 'Maria', 'João', 'José'];
let [nome4, ...resto] = lista2;

console.log(nome4);
console.log(resto);

console.log(lista2)