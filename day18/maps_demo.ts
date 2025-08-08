//Declare a Map with string keys and Number values
let scores:Map<string,number> = new Map();

//Add values
scores.set("Alice",95);
scores.set("bob",82);
scores.set("Charlie",100);

//get a value by key
console.log(scores.get("Alice"));

//check if a key exists
console.log(scores.has("Bob"));
//Iterate over entries --for of loop
for(let [name,score] of scores){
    console.log(`4{name} scored ${score}`); //templete literal

}

//remove an entry
scores.delete("Bob");

//Map size 
console.log(scores.size); //output:2

//clear all entries
scores.clear();
console.log(scores.size);



