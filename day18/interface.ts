interface User {
    employeeId: any;
    name: string;
    age: number;
}

const alice: User = {
    name: 'Alice',
    age: 30
};

console.log(`User: ${alice.name}, Age: ${alice.age}`); // Output: User: Alice, Age: 30