const fruits = ['apple', 'banana','carrot'];
for (const fruit of fruits) {
    console.log(fruit);
}
const scores1 = new Map([
    ['Alice', 95],
    ['Bob', 87],
    ['Eve', 78],
]);
for (const [name, score] of scores1) {
    console.log(`${name}: ${score}`);
}
const uniqueNumbers = new Set([1, 2, 3, 4, 5]);
for (const number of uniqueNumbers) {
    console.log(number);
}
