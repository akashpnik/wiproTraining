var people = ["Alice", 30];
var coordinates = [10, 30];
function displayTuple(tuple) {
    console.log("Name: ".concat(tuple[0], ", Age: ").concat(tuple[1]));
}
displayTuple(people);
console.log("coordinates: (".concat(coordinates[0], ",").concat(coordinates[1], ")"));
