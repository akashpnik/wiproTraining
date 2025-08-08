let people : [string, number  ] = ["Alice",30];
let coordinates: [number,number] =[10,30];

function displayTuple(tuple:[string,number]){
    console.log(`Name: ${tuple[0]}, Age: ${tuple[1]}`)
}
displayTuple(people);
console.log(`coordinates: (${coordinates[0]},${coordinates[1]})`);
